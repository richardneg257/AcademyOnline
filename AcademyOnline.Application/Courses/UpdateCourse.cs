using AcademyOnline.Persistence;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class UpdateCourse
    {
        public class UpdateCourseQuery : IRequest
        {
            public int CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? PublicationDate { get; set; }
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
                    throw new Exception("El curso no existe");

                course.Title = request.Title ?? course.Title;
                course.Description = request.Description ?? course.Description;
                course.PublicationDate = request.PublicationDate ?? course.PublicationDate;

                var status = await context.SaveChangesAsync();
                if (status > 0)
                    return Unit.Value;

                throw new Exception("No se guardaron los cambios en el curso");
            }
        }
    }
}
