using AcademyOnline.Application.Courses;
using AcademyOnline.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyOnline.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>()
                .ForMember(x => x.Instructors, y => y.MapFrom(z => z.InstructorsLink.Select(a => a.Instructor).ToList()))
                .ForMember(x => x.Comments, y => y.MapFrom(z => z.Comments))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price));
            CreateMap<CourseInstructor, CourseInstructorDto>();
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Price, PriceDto>();
        }
    }
}
