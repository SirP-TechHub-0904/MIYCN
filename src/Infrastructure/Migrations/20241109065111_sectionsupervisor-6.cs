using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sectionsupervisor6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupervisorTrainingForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TrainingId = table.Column<long>(type: "bigint", nullable: true),
                    SupervisorSectionQuestionId = table.Column<long>(type: "bigint", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnFour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorTrainingForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupervisorTrainingForms_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupervisorTrainingForms_SupervisorSectionQuestions_SupervisorSectionQuestionId",
                        column: x => x.SupervisorSectionQuestionId,
                        principalTable: "SupervisorSectionQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupervisorTrainingForms_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorTrainingForms_SupervisorSectionQuestionId",
                table: "SupervisorTrainingForms",
                column: "SupervisorSectionQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorTrainingForms_TrainingId",
                table: "SupervisorTrainingForms",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorTrainingForms_UserId",
                table: "SupervisorTrainingForms",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupervisorTrainingForms");
        }
    }
}
