using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class certup344 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CertificateAttendanceTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CourseTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "TrainingTitle",
                table: "Settings",
                newName: "CertificateTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CertificateTitle",
                table: "Settings",
                newName: "TrainingTitle");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateAttendanceTitle",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseTitle",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
