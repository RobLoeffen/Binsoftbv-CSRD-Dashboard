using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Binsoft.Ecoparts.Infrastructure.Migrations
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    MaterialId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ShapeType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ShapeHeight = table.Column<double>(type: "REAL", nullable: false),
                    ShapeLength = table.Column<double>(type: "REAL", nullable: true),
                    ShapeWidth = table.Column<double>(type: "REAL", nullable: true),
                    ShapeRadius = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecoparts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Density = table.Column<double>(type: "REAL", nullable: false),
                    EmissionFactor = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ecoparts");

            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}
