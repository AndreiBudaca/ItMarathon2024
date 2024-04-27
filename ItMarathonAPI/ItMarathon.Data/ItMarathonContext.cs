using ItMarathon.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItMarathon.Data
{
    public class ItMarathonContext : DbContext
    {
        public ItMarathonContext(DbContextOptions<ItMarathonContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<StudentGrade> StudentGrades { get; set; }
        
        public DbSet<StudentOptionalPreference> StudentOptionalPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Grades)
                .WithOne(sg => sg.Student)
                .HasForeignKey(sg => sg.StudentId)
                .IsRequired();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Grades)
                .WithOne(sg => sg.Course)
                .HasForeignKey(sg => sg.CourseId)
                .IsRequired();

            modelBuilder.Entity<User>()
               .HasMany(u => u.StudentOptionalPreferences)
               .WithOne(sg => sg.Student)
               .HasForeignKey(sg => sg.StudentId)
               .IsRequired();

            modelBuilder.Entity<Course>()
               .HasMany(c => c.StudentOptionalPreferences)
               .WithOne(sg => sg.Optional)
               .HasForeignKey(sg => sg.OptionalId)
               .IsRequired();
        }
    }
}
