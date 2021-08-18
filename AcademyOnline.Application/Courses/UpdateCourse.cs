using AcademyOnline.Application.Handlers;
using AcademyOnline.Domain;
using AcademyOnline.Persistence;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class UpdateCourse
    {
        public class UpdateCourseQuery : IRequest
        {
            public Guid CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? PublicationDate { get; set; }
            public List<Guid> InstructorsLink { get; set; }
        }

        public class UpdateCourseQueryValidation : AbstractValidator<UpdateCourseQuery>
        {
            public UpdateCourseQueryValidation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.PublicationDate).NotEmpty();
            }
        }

        public class UpdateCourseHandler : IRequestHandler<UpdateCourseQuery>
        {
            private readonly AcademyOnlineContext context;

            public UpdateCourseHandler(AcademyOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(UpdateCourseQuery request, CancellationToken cancellationToken)
            {
                var course = await context.Courses.FindAsync(request.CourseId);
                if (course == null)
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new { message = "ExceptionHandler: No se encontró el curso" });

                course.Title = request.Title ?? course.Title;
                course.Description = request.Description ?? course.Description;
                course.PublicationDate = request.PublicationDate ?? course.PublicationDate;

                if (request.InstructorsLink != null && request.InstructorsLink.Count > 0)
                {
                    var instructorsDb = context.CourseInstuctor.Where(x => x.CourseId == request.CourseId).ToList();
                    foreach(var courseInstructor in instructorsDb)
                    {
                        context.CourseInstuctor.Remove(courseInstructor);
                    }
                    foreach(var instructorId in request.InstructorsLink)
                    {
                        var newCourseInstructor = new CourseInstructor
                        {
                            CourseId = request.CourseId,
                            InstructorId = instructorId
                        };
                        context.CourseInstuctor.Add(newCourseInstructor);
                    }
                }

                var status = await context.SaveChangesAsync();
                if (status > 0)
                    return Unit.Value;

                throw new Exception("No se guardaron los cambios en el curso");
            }
        }
    }
}
