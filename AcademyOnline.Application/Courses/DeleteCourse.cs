using AcademyOnline.Application.Handlers;
using AcademyOnline.Persistence;
using MediatR;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class DeleteCourse
    {
        public class DeleteCourseQuery : IRequest
        {
            public Guid CourseId { get; set; }
        }

        public class DeleteCourseHandler : IRequestHandler<DeleteCourseQuery>
        {
            private readonly AcademyOnlineContext context;

            public DeleteCourseHandler(AcademyOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(DeleteCourseQuery request, CancellationToken cancellationToken)
            {
                var instructorsDb = context.CourseInstuctor.Where(x => x.CourseId == request.CourseId).ToList();
                foreach(var instructor in instructorsDb)
                {
                    context.CourseInstuctor.Remove(instructor);
                }

                var comments = context.Comments.Where(x => x.CourseId == request.CourseId).ToList();
                foreach(var comment in comments)
                {
                    context.Comments.Remove(comment);
                }

                var price = context.Prices.FirstOrDefault(x => x.CourseId == request.CourseId);
                if (price != null)
                {
                    context.Prices.Remove(price);
                }

                var course = await context.Courses.FindAsync(request.CourseId);
                if (course == null)
                {
                    //throw new Exception("No se encontró el curso");
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new { message = "ExceptionHandler: No se encontró el curso" });
                }

                context.Remove(course);
                var state = await context.SaveChangesAsync();
                if (state > 0)
                    return Unit.Value;

                throw new Exception("No se pudo eliminar el curso");
            }
        }
    }
}
