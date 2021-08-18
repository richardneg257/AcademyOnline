using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class InstructorDto
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Degree { get; set; }
        public byte[] ProfilePhoto { get; set; }
    }
}
