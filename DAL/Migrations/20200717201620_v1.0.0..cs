using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class v100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appointmentTypes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointmentTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    birthDate = table.Column<DateTime>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    insuranceCompany = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appointmentDate = table.Column<DateTime>(nullable: false),
                    patientId = table.Column<int>(nullable: false),
                    appointmentTypeId = table.Column<int>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.id);
                    table.ForeignKey(
                        name: "FK_appointments_appointmentTypes_appointmentTypeId",
                        column: x => x.appointmentTypeId,
                        principalTable: "appointmentTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointments_patients_patientId",
                        column: x => x.patientId,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_appointmentTypeId",
                table: "appointments",
                column: "appointmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_patientId",
                table: "appointments",
                column: "patientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "appointmentTypes");

            migrationBuilder.DropTable(
                name: "patients");
        }
    }
}
