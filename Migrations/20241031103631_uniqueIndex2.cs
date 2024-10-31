using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentAPI.Migrations
{
    /// <inheritdoc />
    public partial class uniqueIndex2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_Street",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumn: "Id",
                keyValue: 1,
                column: "EnrollmentDate",
                value: new DateTime(2024, 10, 31, 11, 36, 31, 260, DateTimeKind.Local).AddTicks(1077));

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumn: "Id",
                keyValue: 2,
                column: "EnrollmentDate",
                value: new DateTime(2024, 10, 31, 11, 36, 31, 260, DateTimeKind.Local).AddTicks(1126));

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumn: "Id",
                keyValue: 3,
                column: "EnrollmentDate",
                value: new DateTime(2024, 10, 31, 11, 36, 31, 260, DateTimeKind.Local).AddTicks(1129));

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumn: "Id",
                keyValue: 4,
                column: "EnrollmentDate",
                value: new DateTime(2024, 10, 31, 11, 36, 31, 260, DateTimeKind.Local).AddTicks(1130));

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Street_City",
                table: "Addresses",
                columns: new[] { "Street", "City" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_Street_City",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumn: "Id",
                keyValue: 1,
                column: "EnrollmentDate",
                value: new DateTime(2024, 10, 31, 11, 32, 42, 17, DateTimeKind.Local).AddTicks(3465));

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumn: "Id",
                keyValue: 2,
                column: "EnrollmentDate",
                value: new DateTime(2024, 10, 31, 11, 32, 42, 17, DateTimeKind.Local).AddTicks(3519));

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumn: "Id",
                keyValue: 3,
                column: "EnrollmentDate",
                value: new DateTime(2024, 10, 31, 11, 32, 42, 17, DateTimeKind.Local).AddTicks(3522));

            migrationBuilder.UpdateData(
                table: "StudentCourses",
                keyColumn: "Id",
                keyValue: 4,
                column: "EnrollmentDate",
                value: new DateTime(2024, 10, 31, 11, 32, 42, 17, DateTimeKind.Local).AddTicks(3523));

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Street",
                table: "Addresses",
                column: "Street",
                unique: true);
        }
    }
}
