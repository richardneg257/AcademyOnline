﻿namespace AcademyOnline.Domain
{
    public class CourseInstructor
    {
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
    }
}
