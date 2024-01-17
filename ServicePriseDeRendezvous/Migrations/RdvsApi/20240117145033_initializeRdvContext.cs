using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicePriseDeRendezvous.Migrations.RdvsApi
{
    public partial class initializeRdvContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rdvs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomDuPatient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjetConsultation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JourRendezVous = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rdvs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rdvs");
        }
    }
}
