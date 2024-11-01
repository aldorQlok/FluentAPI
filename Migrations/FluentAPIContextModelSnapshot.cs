﻿// <auto-generated />
using System;
using FluentAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FluentAPI.Migrations
{
    [DbContext(typeof(FluentAPIContext))]
    partial class FluentAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FluentAPI.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Street", "City")
                        .IsUnique();

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Springfield",
                            Country = "USA",
                            Street = "123 Elm Street"
                        },
                        new
                        {
                            Id = 2,
                            City = "Shelbyville",
                            Country = "USA",
                            Street = "456 Oak Avenue"
                        });
                });

            modelBuilder.Entity("FluentAPI.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Math 101"
                        },
                        new
                        {
                            Id = 2,
                            Title = "History 201"
                        });
                });

            modelBuilder.Entity("FluentAPI.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<int?>("Age")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("First_Name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("NET23_Students", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressId = 1,
                            Age = 20,
                            FirstName = "Alice",
                            LastName = "Johnson"
                        },
                        new
                        {
                            Id = 2,
                            AddressId = 1,
                            Age = 22,
                            FirstName = "Bob",
                            LastName = "Smith"
                        });
                });

            modelBuilder.Entity("FluentAPI.Models.StudentCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseId = 1,
                            EnrollmentDate = new DateTime(2024, 10, 31, 11, 36, 31, 260, DateTimeKind.Local).AddTicks(1077),
                            StudentId = 1
                        },
                        new
                        {
                            Id = 2,
                            CourseId = 2,
                            EnrollmentDate = new DateTime(2024, 10, 31, 11, 36, 31, 260, DateTimeKind.Local).AddTicks(1126),
                            StudentId = 1
                        },
                        new
                        {
                            Id = 3,
                            CourseId = 1,
                            EnrollmentDate = new DateTime(2024, 10, 31, 11, 36, 31, 260, DateTimeKind.Local).AddTicks(1129),
                            StudentId = 2
                        },
                        new
                        {
                            Id = 4,
                            CourseId = 2,
                            EnrollmentDate = new DateTime(2024, 10, 31, 11, 36, 31, 260, DateTimeKind.Local).AddTicks(1130),
                            StudentId = 2
                        });
                });

            modelBuilder.Entity("FluentAPI.Models.Student", b =>
                {
                    b.HasOne("FluentAPI.Models.Address", "Address")
                        .WithMany("Students")
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("FluentAPI.Models.StudentCourse", b =>
                {
                    b.HasOne("FluentAPI.Models.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FluentAPI.Models.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("FluentAPI.Models.Address", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("FluentAPI.Models.Course", b =>
                {
                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("FluentAPI.Models.Student", b =>
                {
                    b.Navigation("StudentCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
