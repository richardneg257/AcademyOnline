using System;
using System.Collections.Generic;

namespace AcademyOnline.Domain
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public Price Price { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CourseInstructor> InstructorsLink { get; set; }
    }
}
