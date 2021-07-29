namespace AcademyOnline.Domain
{
    public class Price
    {
        public int PrecioId { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal PromotionPrice { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
