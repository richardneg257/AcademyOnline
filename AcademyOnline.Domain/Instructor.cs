using System.Collections.Generic;

namespace AcademyOnline.Domain
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Degree { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public ICollection<CourseInstructor> CoursesLink { get; set; }
    }
}
