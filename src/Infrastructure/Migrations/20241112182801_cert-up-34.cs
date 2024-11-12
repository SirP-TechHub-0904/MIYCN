using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class certup34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Trainings_TrainingId",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Settings_TrainingId",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "RightTitleName",
                table: "Settings",
                newName: "CertificateRightSideSignatureUrl");

            migrationBuilder.RenameColumn(
                name: "RightSignatureUrl",
                table: "Settings",
                newName: "CertificateRightSideSignatureKey");

            migrationBuilder.RenameColumn(
                name: "RightSignatureKey",
                table: "Settings",
                newName: "CertificateRightSideOfficeTitle");

            migrationBuilder.RenameColumn(
                name: "RightPosition",
                table: "Settings",
                newName: "CertificateRightSideOfficePosition");

            migrationBuilder.RenameColumn(
                name: "RightOffSignature",
                table: "Settings",
                newName: "CertificateUseRightSidePhysicalSignature");

            migrationBuilder.RenameColumn(
                name: "RightOccupation",
                table: "Settings",
                newName: "CertificateRightSideName");

            migrationBuilder.RenameColumn(
                name: "LeftTitleName",
                table: "Settings",
                newName: "CertificateLeftSideSignatureUrl");

            migrationBuilder.RenameColumn(
                name: "LeftSignatureUrl",
                table: "Settings",
                newName: "CertificateLeftSideSignatureKey");

            migrationBuilder.RenameColumn(
                name: "LeftSignatureKey",
                table: "Settings",
                newName: "CertificateLeftSideOfficeTitle");

            migrationBuilder.RenameColumn(
                name: "LeftPosition",
                table: "Settings",
                newName: "CertificateLeftSideOfficePosition");

            migrationBuilder.RenameColumn(
                name: "LeftOffSignature",
                table: "Settings",
                newName: "CertificateUseLeftSidePhysicalSignature");

            migrationBuilder.RenameColumn(
                name: "LeftOccupation",
                table: "Settings",
                newName: "CertificateLeftSideName");

            migrationBuilder.AddColumn<bool>(
                name: "UseStateCertificateInformation",
                table: "Trainings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseStateCertificateInformation",
                table: "Trainings");

            migrationBuilder.RenameColumn(
                name: "CertificateUseRightSidePhysicalSignature",
                table: "Settings",
                newName: "RightOffSignature");

            migrationBuilder.RenameColumn(
                name: "CertificateUseLeftSidePhysicalSignature",
                table: "Settings",
                newName: "LeftOffSignature");

            migrationBuilder.RenameColumn(
                name: "CertificateRightSideSignatureUrl",
                table: "Settings",
                newName: "RightTitleName");

            migrationBuilder.RenameColumn(
                name: "CertificateRightSideSignatureKey",
                table: "Settings",
                newName: "RightSignatureUrl");

            migrationBuilder.RenameColumn(
                name: "CertificateRightSideOfficeTitle",
                table: "Settings",
                newName: "RightSignatureKey");

            migrationBuilder.RenameColumn(
                name: "CertificateRightSideOfficePosition",
                table: "Settings",
                newName: "RightPosition");

            migrationBuilder.RenameColumn(
                name: "CertificateRightSideName",
                table: "Settings",
                newName: "RightOccupation");

            migrationBuilder.RenameColumn(
                name: "CertificateLeftSideSignatureUrl",
                table: "Settings",
                newName: "LeftTitleName");

            migrationBuilder.RenameColumn(
                name: "CertificateLeftSideSignatureKey",
                table: "Settings",
                newName: "LeftSignatureUrl");

            migrationBuilder.RenameColumn(
                name: "CertificateLeftSideOfficeTitle",
                table: "Settings",
                newName: "LeftSignatureKey");

            migrationBuilder.RenameColumn(
                name: "CertificateLeftSideOfficePosition",
                table: "Settings",
                newName: "LeftPosition");

            migrationBuilder.RenameColumn(
                name: "CertificateLeftSideName",
                table: "Settings",
                newName: "LeftOccupation");

            migrationBuilder.AddColumn<long>(
                name: "TrainingId",
                table: "Settings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Settings_TrainingId",
                table: "Settings",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_Trainings_TrainingId",
                table: "Settings",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
