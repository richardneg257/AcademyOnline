using AcademyOnline.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcademyOnline.Persistence
{
    public class AcademyOnlineContext : IdentityDbContext<User>
    {
        public AcademyOnlineContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseInstructor>().HasKey(ci => new { ci.CourseId, ci.InstructorId });
            //modelBuilder.Entity<Price>().Property(p => p.CurrentPrice).HasColumnType("decimal(18,2)");
            //modelBuilder.Entity<Price>().Property(p => p.PromotionPrice).HasColumnType("decimal(18,2)");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CourseInstructor> CourseInstuctor { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Price> Prices { get; set; }
    }
}
