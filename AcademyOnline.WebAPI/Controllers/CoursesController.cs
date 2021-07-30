using AcademyOnline.Application.Courses;
using AcademyOnline.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademyOnline.WebAPI.Controllers
{
    [Route("api/Courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CoursesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> Get()
        {
            return await mediator.Send(new GetCourses.GetCoursesQuery());
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<Course>> GetById(int courseId)
        {
            return await mediator.Send(new GetCourseById.GetCourseByIdQuery() { CourseId = courseId });
        }
    }
}
