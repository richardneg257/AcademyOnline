using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyOnline.Application.Courses
{
    public class PriceDto
    {
        public Guid PriceId { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal PromotionPrice { get; set; }
        public Guid CourseId { get; set; }
    }
}
