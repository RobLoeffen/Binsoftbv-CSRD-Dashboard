using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Binsoft.CRSDdashboard.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSeederValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Length", "Width" },
                values: new object[] { 3.0, 2.0 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Length", "Width" },
                values: new object[] { 1.0, 0.5 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Length", "Width" },
                values: new object[] { 4.0, 6.0 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Length", "Width" },
                values: new object[] { 0.10000000000000001, 0.5 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Length", "Width" },
                values: new object[] { 3.0, 2.0 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Length", "Width" },
                values: new object[] { 10.0, 20.0 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Length", "Width" },
                values: new object[] { 1.0, 1.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Length", "Width" },
                values: new object[] { 300.0, 200.0 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Length", "Width" },
                values: new object[] { 300.0, 200.0 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Length", "Width" },
                values: new object[] { 300.0, 200.0 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Length", "Width" },
                values: new object[] { 300.0, 200.0 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Length", "Width" },
                values: new object[] { 300.0, 200.0 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Length", "Width" },
                values: new object[] { 300.0, 200.0 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Length", "Width" },
                values: new object[] { 300.0, 200.0 });
        }
    }
}
