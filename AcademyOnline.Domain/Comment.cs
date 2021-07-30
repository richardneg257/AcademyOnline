using System;

namespace AcademyOnline.Domain
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string Student { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
