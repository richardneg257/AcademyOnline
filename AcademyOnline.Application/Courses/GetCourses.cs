using AcademyOnline.Domain;
using AcademyOnline.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class GetCourses
    {
        public class GetCoursesQuery : IRequest<List<Course>>
        {

        }

        public class GetCoursesHandler : IRequestHandler<GetCoursesQuery, List<Course>>
        {
            private readonly AcademyOnlineContext context;

            public GetCoursesHandler(AcademyOnlineContext context)
            {
                this.context = context;
            }

            public async Task<List<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
            {
                return await context.Courses.ToListAsync();
            }
        }
    }
}
