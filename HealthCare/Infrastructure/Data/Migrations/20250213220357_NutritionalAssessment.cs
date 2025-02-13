using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCare.Migrations
{
    /// <inheritdoc />
    public partial class NutritionalAssessment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NutritionalAssessment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cb = table.Column<string>(type: "text", nullable: false),
                    Aj = table.Column<string>(type: "text", nullable: false),
                    Cp = table.Column<string>(type: "text", nullable: false),
                    EstimatedWeight = table.Column<float>(type: "real", nullable: false),
                    EstimatedStature = table.Column<float>(type: "real", nullable: false),
                    Imc = table.Column<float>(type: "real", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionalAssessment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionalAssessment_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NutritionalAssessment_PatientId",
                table: "NutritionalAssessment",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionalAssessment");
        }
    }
}
