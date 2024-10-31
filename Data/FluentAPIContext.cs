using FluentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FluentAPI.Data
{
    public class FluentAPIContext : DbContext
    {
        public FluentAPIContext(DbContextOptions<FluentAPIContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Alice", LastName = "Johnson", Age = 20, AddressId = 1 },
                new Student { Id = 2, FirstName = "Bob", LastName = "Smith", Age = 22, AddressId = 1 }
            );

            modelBuilder.Entity<Address>().HasData(
                new Address { Id = 1, Street = "123 Elm Street", City = "Springfield", Country = "USA" },
                new Address { Id = 2, Street = "456 Oak Avenue", City = "Shelbyville", Country = "USA" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Math 101" },
                new Course { Id = 2, Title = "History 201" }
            );

            modelBuilder.Entity<StudentCourse>().HasData(
                new StudentCourse { Id = 1, StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.Now },
                new StudentCourse { Id = 2, StudentId = 1, CourseId = 2, EnrollmentDate = DateTime.Now },
                new StudentCourse { Id = 3, StudentId = 2, CourseId = 1, EnrollmentDate = DateTime.Now },
                new StudentCourse { Id = 4, StudentId = 2, CourseId = 2, EnrollmentDate = DateTime.Now }
            );
        }
    }
}
