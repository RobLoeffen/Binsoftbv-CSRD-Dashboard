using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Binsoft.CRSDdashboard.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddShapeTypeToEcopart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Ecoparts");

            migrationBuilder.AlterColumn<double>(
                name: "Width",
                table: "Ecoparts",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<double>(
                name: "Height",
                table: "Ecoparts",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<double>(
                name: "Radius",
                table: "Ecoparts",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShapeType",
                table: "Ecoparts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Radius", "ShapeType" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Radius", "ShapeType" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Radius", "ShapeType" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Radius", "ShapeType" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Radius", "ShapeType" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Radius", "ShapeType" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Radius", "ShapeType" },
                values: new object[] { null, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Radius",
                table: "Ecoparts");

            migrationBuilder.DropColumn(
                name: "ShapeType",
                table: "Ecoparts");

            migrationBuilder.AlterColumn<double>(
                name: "Width",
                table: "Ecoparts",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Height",
                table: "Ecoparts",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Ecoparts",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Weight",
                value: 1.2);

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Weight",
                value: 1.5);

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Weight",
                value: 1.3999999999999999);

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Weight",
                value: 1.3);

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 5,
                column: "Weight",
                value: 1.6000000000000001);

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 6,
                column: "Weight",
                value: 2.1000000000000001);

            migrationBuilder.UpdateData(
                table: "Ecoparts",
                keyColumn: "Id",
                keyValue: 7,
                column: "Weight",
                value: 1.8);
        }
    }
}
