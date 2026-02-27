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
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Co2PerKg = table.Column<double>(type: "REAL", nullable: false),
                    Density = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                });

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
                    MaterialId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecoparts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ecoparts_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "Id", "Co2PerKg", "Density", "Name" },
                values: new object[,]
                {
                    { 1, 2.25, 1.27, "PET" },
                    { 2, 3.0899999999999999, 0.95999999999999996, "HDPE" },
                    { 3, 3.0899999999999999, 0.93999999999999995, "LDPE" },
                    { 4, 3.0899999999999999, 0.91000000000000003, "PP" },
                    { 5, 3.3300000000000001, 1.0600000000000001, "PS" },
                    { 6, 1.3899999999999999, 1.45, "PVC" },
                    { 7, 0.010999999999999999, 1.24, "PLA" }
                });

            migrationBuilder.InsertData(
                table: "Ecoparts",
                columns: new[] { "Id", "CreatedAt", "Height", "Length", "MaterialId", "Name", "Weight", "Width" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, 1, "PET Plate", 1.2, 200.0 },
                    { 2, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, 2, "HDPE Plate", 1.5, 200.0 },
                    { 3, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, 3, "LDPE Plate", 1.3999999999999999, 200.0 },
                    { 4, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, 4, "PP Plate", 1.3, 200.0 },
                    { 5, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, 5, "PS Plate", 1.6000000000000001, 200.0 },
                    { 6, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, 6, "PVC Plate", 2.1000000000000001, 200.0 },
                    { 7, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 300.0, 7, "PLA Plate", 1.8, 200.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ecoparts_MaterialId",
                table: "Ecoparts",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_Name",
                table: "Material",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ecoparts");

            migrationBuilder.DropTable(
                name: "Material");
        }
    }
}
