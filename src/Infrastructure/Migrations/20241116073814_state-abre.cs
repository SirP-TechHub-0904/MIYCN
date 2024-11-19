using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
namespace Infrastructure.Migrations
{
    public partial class stateabre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatesAndAbbreviations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatesAndAbbreviations", x => x.Id);
                });

            // Seed data for Nigerian states and 3-letter abbreviations
            migrationBuilder.InsertData(
                table: "StatesAndAbbreviations",
                columns: new[] { "State", "Abbreviation" },
                values: new object[,]
                {
                    { "Abia", "ABI" },
                    { "Adamawa", "ADA" },
                    { "AkwaIbom", "AKS" },
                    { "Anambra", "ANA" },
                    { "Bauchi", "BAU" },
                    { "Bayelsa", "BAY" },
                    { "Benue", "BEN" },
                    { "Borno", "BOR" },
                    { "Cross River", "CRO" },
                    { "Delta", "DEL" },
                    { "Ebonyi", "EBO" },
                    { "Edo", "EDO" },
                    { "Ekiti", "EKI" },
                    { "Enugu", "ENU" },
                    { "FCT", "FCT" },
                    { "Gombe", "GOM" },
                    { "Imo", "IMO" },
                    { "Jigawa", "JIG" },
                    { "Kaduna", "KAD" },
                    { "Kano", "KAN" },
                    { "Katsina", "KAT" },
                    { "Kebbi", "KEB" },
                    { "Kogi", "KOG" },
                    { "Kwara", "KWA" },
                    { "Lagos", "LAG" },
                    { "Nasarawa", "NAS" },
                    { "Niger", "NIG" },
                    { "Ogun", "OGU" },
                    { "Ondo", "OND" },
                    { "Osun", "OSU" },
                    { "Oyo", "OYO" },
                    { "Plateau", "PLA" },
                    { "Rivers", "RIV" },
                    { "Sokoto", "SOK" },
                    { "Taraba", "TAR" },
                    { "Yobe", "YOB" },
                    { "Zamfara", "ZAM" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatesAndAbbreviations");
        }
    }
}
