using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class v1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "appointmentTypes",
                columns: new[] { "id", "active", "name" },
                values: new object[,]
                {
                    { -1, true, "Consulta General" },
                    { -2, true, "Odontologia" },
                    { -3, true, "Pediatria" },
                    { -4, true, "Neurologia" },
                    { -5, true, "Ortopedia" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "appointmentTypes",
                keyColumn: "id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "appointmentTypes",
                keyColumn: "id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "appointmentTypes",
                keyColumn: "id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "appointmentTypes",
                keyColumn: "id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "appointmentTypes",
                keyColumn: "id",
                keyValue: -1);
        }
    }
}
