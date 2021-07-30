using AcademyOnline.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class DeleteCourse
    {
        public class DeleteCourseQuery : IRequest
        {
            public int CourseId { get; set; }
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
                var course = await context.Courses.FindAsync(request.CourseId);
                if (course == null)
                    throw new Exception("No se encontró el curso");

                context.Remove(course);
                var state = await context.SaveChangesAsync();
                if (state > 0)
                    return Unit.Value;

                throw new Exception("No se pudo eliminar el curso");
            }
        }
    }
}
