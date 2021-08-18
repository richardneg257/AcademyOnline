using AcademyOnline.Domain;
using AcademyOnline.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class GetCourses
    {
        public class GetCoursesQuery : IRequest<List<CourseDto>>
        {

        }

        public class GetCoursesHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>
        {
            private readonly AcademyOnlineContext context;
            private readonly IMapper mapper;

            public GetCoursesHandler(AcademyOnlineContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
            {
                var courses = await context.Courses.Include(x => x.InstructorsLink).ThenInclude(x => x.Instructor).ToListAsync();
                var coursesDto = mapper.Map<List<Course>, List<CourseDto>>(courses);
                return coursesDto;
            }
        }
    }
}
