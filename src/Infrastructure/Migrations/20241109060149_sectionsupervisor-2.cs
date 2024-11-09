using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sectionsupervisor2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerType",
                table: "SupervisorSectionQuestions");

            migrationBuilder.DropColumn(
                name: "AnswerTypeTitle",
                table: "SupervisorSectionQuestions");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "SupervisorSectionQuestions");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "SupervisorSectionQuestions");

            migrationBuilder.DropColumn(
                name: "EnableNames",
                table: "SupervisorSectionQuestions");

            migrationBuilder.DropColumn(
                name: "EnableRemark",
                table: "SupervisorSectionQuestions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SupervisorSectionQuestions");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "SupervisorSectionQuestions");

            migrationBuilder.DropColumn(
                name: "RemarkTitle",
                table: "SupervisorSectionQuestions");

            migrationBuilder.AddColumn<int>(
                name: "AnswerType",
                table: "SupervisorSections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AnswerTypeTitle",
                table: "SupervisorSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "SupervisorSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "SupervisorSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnableNames",
                table: "SupervisorSections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableRemark",
                table: "SupervisorSections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SupervisorSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "SupervisorSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkTitle",
                table: "SupervisorSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "SupervisorSections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerType",
                table: "SupervisorSections");

            migrationBuilder.DropColumn(
                name: "AnswerTypeTitle",
                table: "SupervisorSections");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "SupervisorSections");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "SupervisorSections");

            migrationBuilder.DropColumn(
                name: "EnableNames",
                table: "SupervisorSections");

            migrationBuilder.DropColumn(
                name: "EnableRemark",
                table: "SupervisorSections");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SupervisorSections");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "SupervisorSections");

            migrationBuilder.DropColumn(
                name: "RemarkTitle",
                table: "SupervisorSections");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "SupervisorSections");

            migrationBuilder.AddColumn<int>(
                name: "AnswerType",
                table: "SupervisorSectionQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AnswerTypeTitle",
                table: "SupervisorSectionQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "SupervisorSectionQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "SupervisorSectionQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnableNames",
                table: "SupervisorSectionQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableRemark",
                table: "SupervisorSectionQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SupervisorSectionQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "SupervisorSectionQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkTitle",
                table: "SupervisorSectionQuestions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
