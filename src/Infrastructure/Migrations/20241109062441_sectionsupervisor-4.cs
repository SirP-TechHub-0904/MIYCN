using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sectionsupervisor4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "SupervisorSections",
                newName: "ColumnTwo");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SupervisorSections",
                newName: "ColumnThree");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "SupervisorSections",
                newName: "ColumnOne");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "SupervisorSections",
                newName: "ColumnFour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ColumnTwo",
                table: "SupervisorSections",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "ColumnThree",
                table: "SupervisorSections",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ColumnOne",
                table: "SupervisorSections",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ColumnFour",
                table: "SupervisorSections",
                newName: "Designation");
        }
    }
}
