using AcademyOnline.Domain;
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
    public class CreateCourse
    {
        public class CreateCourseQuery : IRequest
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? PublicationDate { get; set; }
        }

        public class CreateCourseQueryValidation : AbstractValidator<CreateCourseQuery>
        {
            public CreateCourseQueryValidation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.PublicationDate).NotEmpty();
            }
        }

        public class CreateCourseHandler : IRequestHandler<CreateCourseQuery>
        {
            private readonly AcademyOnlineContext context;

            public CreateCourseHandler(AcademyOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(CreateCourseQuery request, CancellationToken cancellationToken)
            {
                var course = new Course()
                {
                    Title = request.Title,
                    Description = request.Description,
                    PublicationDate = request.PublicationDate
                };

                context.Courses.Add(course);
                var state = await context.SaveChangesAsync();
                if (state > 0)
                    return Unit.Value;

                throw new Exception("No se pudo insertar el curso");
            }
        }
    }
}
