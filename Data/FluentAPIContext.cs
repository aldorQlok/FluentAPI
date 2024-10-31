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


            // Byter namn på "Student" tabellen i databasen.
            modelBuilder.Entity<Student>()
                .ToTable("NET23_Students");

            // Byter namn på "FirstName" kolumnen i Student tabellen i databasen.
            modelBuilder.Entity<Student>()
                .Property(s => s.FirstName)
                .HasColumnName("First_Name");

            // Byter/beskriver datatypen för kolumnen "FirstName" i Student tabellen i databasen.
            modelBuilder.Entity<Student>()
                .Property(s => s.FirstName)
                .HasColumnType("nvarchar(100)");

            // Här kör vi s.k "Fluent Chaining" eller "Method Chaining" för att kombinera
            // konfigurationerna vi gjorde ovan till en och samma entity.
            // Detta är möjligt då vi jobbar mot samma tabell och samma property.
            modelBuilder.Entity<Student>()
                .Property(s => s.FirstName)
                .HasColumnName("First_Name")
                .HasColumnType("nvarchar(100)");

            // Här sätter vi en "Age" till "NOT NULL" genom att använda "IsRequired".
            // Detta är vanligt när vi har satt "?"-tecken på propertyn i modellen men vi vill ändå
            // att det ska krävas i databasen.
            modelBuilder.Entity<Student>()
                .Property(s => s.Age)
                .IsRequired();

            // Den här kommer "Ignorera" Country-propertyn. Det innebär att den här kolumnen inte kommer skapas i tabellen Address.
            modelBuilder.Entity<Address>()
                .Ignore(a => a.Country);

            // Den här använd för att ätta ett primärtnykel.
            // Vanligtvi sköts detta automatiskt av EF men när Id har en icke-konventionell namn så kan det vara
            // bra att definiera den på det här sättet.
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => sc.Id);

            // Så här definieras en Många-Till-Många relation (både den här raden och den under den skapar en-
            // Många-Till-Många relation mellan Student och Course där StudentCourse är relationstabellen.
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                // Konfigurera beteendet när vi tar bort en rad i "Student" och hur referensen ska hanteras i StudentCourse.
                // I det här fallet sätter vi den till "Null" istället för att ta bort hela raden som är default beteendet.
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId)
                // Här sätter vi beteendet till "Strict" vilket innebär att den kommer förhindra oss från att ta bort en rad i "Coure"
                // så länge den har en referens till en rad i StudentCourse
                .OnDelete(DeleteBehavior.Restrict);

            // Här skapar vi ett index av "Street" och gör att den blir "Unique".
            // Det innebär att det blir snabbare att utföra SELECT, sortering och filtrering förfrågningar mot den
            // Samt att den inte får upprepas och måste vara unik.
            modelBuilder.Entity<Address>()
                .HasIndex(a => a.Street)
                .IsUnique();

            // Här gör vi samma sak som ovan fast istället för att det ska vara ett enda property så blir det en kombination av properties.
            modelBuilder.Entity<Address>()
                .HasIndex(a => new { a.Street, a.City })
                .IsUnique();

            // Kod för att seeda databasen med data.
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
