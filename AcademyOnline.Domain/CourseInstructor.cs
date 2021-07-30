﻿using System;

namespace AcademyOnline.Domain
{
    public class CourseInstructor
    {
        public Guid CourseId { get; set; }
        public Guid InstructorId { get; set; }
        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
    }
}
