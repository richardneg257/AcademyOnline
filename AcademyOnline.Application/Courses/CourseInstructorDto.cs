using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class CourseInstructorDto
    {
        public Guid CourseId { get; set; }
        public Guid InstructorId { get; set; }
    }
}
