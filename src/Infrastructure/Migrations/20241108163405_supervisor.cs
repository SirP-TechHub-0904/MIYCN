using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class supervisor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupervisorSections",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupervisorSectionQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerType = table.Column<int>(type: "int", nullable: false),
                    AnswerTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableRemark = table.Column<bool>(type: "bit", nullable: false),
                    RemarkTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableNames = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    SupervisorSectionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorSectionQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupervisorSectionQuestions_SupervisorSections_SupervisorSectionId",
                        column: x => x.SupervisorSectionId,
                        principalTable: "SupervisorSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorSectionQuestions_SupervisorSectionId",
                table: "SupervisorSectionQuestions",
                column: "SupervisorSectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupervisorSectionQuestions");

            migrationBuilder.DropTable(
                name: "SupervisorSections");
        }
    }
}
