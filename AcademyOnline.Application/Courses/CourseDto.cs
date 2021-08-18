using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class CourseDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public ICollection<InstructorDto> Instructors { get; set; }
    }
}
