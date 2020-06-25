using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Weddeverhogingdocent1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 1,
                columns: new[] { "Familienaam", "Maandwedde" },
                values: new object[] { "Abbeloos", 1500m });

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 2,
                column: "Familienaam",
                value: "Abelshausen");

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 3,
                column: "Familienaam",
                value: "Achten");

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 4,
                column: "Familienaam",
                value: "Adam");

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 5,
                column: "Familienaam",
                value: "Adriaensens");

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 6,
                column: "Familienaam",
                value: "Adriaensens");

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 7,
                column: "Familienaam",
                value: "Aerenhouts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 1,
                columns: new[] { "Familienaam", "Maandwedde" },
                values: new object[] { "Abbeloo s", 1400m });

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 2,
                column: "Familienaam",
                value: "Abelsh ausen");

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 3,
                column: "Familienaam",
                value: "Achten ");

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 4,
                column: "Familienaam",
                value: "Adam ");

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 5,
                column: "Familienaam",
                value: "Adriaense ns");

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 6,
                column: "Familienaam",
                value: "Adriaens ens");

            migrationBuilder.UpdateData(
                table: "Docenten",
                keyColumn: "DocentId",
                keyValue: 7,
                column: "Familienaam",
                value: "Aerenho uts");
        }
    }
}
