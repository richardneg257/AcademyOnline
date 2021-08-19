using AcademyOnline.Application.Handlers;
using AcademyOnline.Domain;
using AcademyOnline.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class GetCourseById
    {
        public class GetCourseByIdQuery : IRequest<CourseDto>
        {
            public Guid CourseId { get; set; }
        }

        public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, CourseDto>
        {
            private readonly AcademyOnlineContext context;
            private readonly IMapper mapper;

            public GetCourseByIdHandler(AcademyOnlineContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
            {
                var course = await context.Courses
                    .Include(x => x.Price)
                    .Include(x => x.Comments)
                    .Include(x => x.InstructorsLink)
                    .ThenInclude(x => x.Instructor).FirstOrDefaultAsync(x => x.CourseId == request.CourseId);
                
                if (course == null)
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new { message = "ExceptionHandler: No se encontró el curso" });

                var courseDto = mapper.Map<Course, CourseDto>(course);
                return courseDto;
            }
        }
    }
}
