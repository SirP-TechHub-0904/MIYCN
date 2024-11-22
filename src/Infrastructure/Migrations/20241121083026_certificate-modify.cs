using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class certificatemodify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BatchId",
                table: "Trainings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProviderId",
                table: "Trainings",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_BatchId",
                table: "Trainings",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_ProviderId",
                table: "Trainings",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Batches_BatchId",
                table: "Trainings",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Providers_ProviderId",
                table: "Trainings",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Batches_BatchId",
                table: "Trainings");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Providers_ProviderId",
                table: "Trainings");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_BatchId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_ProviderId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Trainings");
        }
    }
}
