using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Binsoft.CRSDdashboard.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ecoparts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Length = table.Column<double>(type: "REAL", nullable: false),
                    Width = table.Column<double>(type: "REAL", nullable: false),
                    Height = table.Column<double>(type: "REAL", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Material = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecoparts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmissionFactors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Material = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Co2PerKg = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmissionFactors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Ecoparts",
                columns: new[] { "Id", "CreatedAt", "Height", "Length", "Material", "Name", "Weight", "Width" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, "PET", "PET Plate", 1.2, 200.0 },
                    { 2, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, "HDPE", "HDPE Plate", 1.5, 200.0 },
                    { 3, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, "LDPE", "LDPE Plate", 1.3999999999999999, 200.0 },
                    { 4, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, "PP", "PP Plate", 1.3, 200.0 },
                    { 5, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, "PS", "PS Plate", 1.6000000000000001, 200.0 },
                    { 6, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, "PVC", "PVC Plate", 2.1000000000000001, 200.0 },
                    { 7, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, "PLA", "PLA Plate", 1.8, 200.0 }
                });

            migrationBuilder.InsertData(
                table: "EmissionFactors",
                columns: new[] { "Id", "Co2PerKg", "Material" },
                values: new object[,]
                {
                    { 1, 2.25, "PET" },
                    { 2, 3.0899999999999999, "HDPE" },
                    { 3, 3.0899999999999999, "LDPE" },
                    { 4, 3.0899999999999999, "PP" },
                    { 5, 3.3300000000000001, "PS" },
                    { 6, 1.3899999999999999, "PVC" },
                    { 7, 0.010999999999999999, "PLA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmissionFactors_Material",
                table: "EmissionFactors",
                column: "Material",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ecoparts");

            migrationBuilder.DropTable(
                name: "EmissionFactors");
        }
    }
}
