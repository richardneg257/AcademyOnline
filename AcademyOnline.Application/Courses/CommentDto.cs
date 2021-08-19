using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class CommentDto
    {
        public Guid CommentId { get; set; }
        public string Student { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public Guid CourseId { get; set; }
    }
}
