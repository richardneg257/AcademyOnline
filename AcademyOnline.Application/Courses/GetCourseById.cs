using AcademyOnline.Application.Handlers;
using AcademyOnline.Domain;
using AcademyOnline.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class GetCourseById
    {
        public class GetCourseByIdQuery : IRequest<Course>
        {
            public int CourseId { get; set; }
        }

        public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, Course>
        {
            private readonly AcademyOnlineContext context;

            public GetCourseByIdHandler(AcademyOnlineContext context)
            {
                this.context = context;
            }

            public async Task<Course> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
            {
                var course = await context.Courses.FindAsync(request.CourseId);
                if (course == null)
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new { message = "ExceptionHandler: No se encontró el curso" });
                return course;
            }
        }
    }
}
