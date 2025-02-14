using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class Pattern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthSituation_Patients_PatientId",
                table: "HealthSituation");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalAssessment_Patients_PatientId",
                table: "NutritionalAssessment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutritionalAssessment",
                table: "NutritionalAssessment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthSituation",
                table: "HealthSituation");

            migrationBuilder.RenameTable(
                name: "NutritionalAssessment",
                newName: "NutritionalAssessments");

            migrationBuilder.RenameTable(
                name: "HealthSituation",
                newName: "HealthSituations");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionalAssessment_PatientId",
                table: "NutritionalAssessments",
                newName: "IX_NutritionalAssessments_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthSituation_PatientId",
                table: "HealthSituations",
                newName: "IX_HealthSituations_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutritionalAssessments",
                table: "NutritionalAssessments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthSituations",
                table: "HealthSituations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthSituations_Patients_PatientId",
                table: "HealthSituations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalAssessments_Patients_PatientId",
                table: "NutritionalAssessments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthSituations_Patients_PatientId",
                table: "HealthSituations");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalAssessments_Patients_PatientId",
                table: "NutritionalAssessments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutritionalAssessments",
                table: "NutritionalAssessments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthSituations",
                table: "HealthSituations");

            migrationBuilder.RenameTable(
                name: "NutritionalAssessments",
                newName: "NutritionalAssessment");

            migrationBuilder.RenameTable(
                name: "HealthSituations",
                newName: "HealthSituation");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionalAssessments_PatientId",
                table: "NutritionalAssessment",
                newName: "IX_NutritionalAssessment_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthSituations_PatientId",
                table: "HealthSituation",
                newName: "IX_HealthSituation_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutritionalAssessment",
                table: "NutritionalAssessment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthSituation",
                table: "HealthSituation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthSituation_Patients_PatientId",
                table: "HealthSituation",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalAssessment_Patients_PatientId",
                table: "NutritionalAssessment",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
