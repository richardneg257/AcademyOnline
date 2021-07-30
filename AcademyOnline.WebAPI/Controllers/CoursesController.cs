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

        [HttpPost]
        public async Task<ActionResult<Unit>> Create([FromBody] CreateCourse.CreateCourseQuery course)
        {
            return await mediator.Send(course);
        }

        [HttpPut("{courseId}")]
        public async Task<ActionResult<Unit>> Update(int courseId, [FromBody] UpdateCourse.UpdateCourseQuery course)
        {
            course.CourseId = courseId;
            return await mediator.Send(course);
        }

        [HttpDelete("{courseId}")]
        public async Task<ActionResult<Unit>> Delete(int courseId)
        {
            return await mediator.Send(new DeleteCourse.DeleteCourseQuery() { CourseId = courseId });
        }
    }
}
