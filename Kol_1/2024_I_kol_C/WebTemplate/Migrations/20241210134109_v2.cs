using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rezervoari",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sifra = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Zapremina = table.Column<double>(type: "float", nullable: false),
                    Temperatura = table.Column<double>(type: "float", nullable: false),
                    DatumPoslednjegCiscenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrekvencijaCiscenja = table.Column<int>(type: "int", nullable: true),
                    Kapacitet = table.Column<int>(type: "int", nullable: false),
                    BrojTrenutnihRiba = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervoari", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ribe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivVrste = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Masa = table.Column<double>(type: "float", nullable: true),
                    GodineStarosti = table.Column<int>(type: "int", nullable: true),
                    GdeSeNalaziID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ribe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ribe_Rezervoari_GdeSeNalaziID",
                        column: x => x.GdeSeNalaziID,
                        principalTable: "Rezervoari",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Akvarijumi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumDodavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojJedinki = table.Column<int>(type: "int", nullable: false),
                    P_RezervoarID = table.Column<int>(type: "int", nullable: true),
                    P_RibaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Akvarijumi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Akvarijumi_Rezervoari_P_RezervoarID",
                        column: x => x.P_RezervoarID,
                        principalTable: "Rezervoari",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Akvarijumi_Ribe_P_RibaID",
                        column: x => x.P_RibaID,
                        principalTable: "Ribe",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Akvarijumi_P_RezervoarID",
                table: "Akvarijumi",
                column: "P_RezervoarID");

            migrationBuilder.CreateIndex(
                name: "IX_Akvarijumi_P_RibaID",
                table: "Akvarijumi",
                column: "P_RibaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ribe_GdeSeNalaziID",
                table: "Ribe",
                column: "GdeSeNalaziID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Akvarijumi");

            migrationBuilder.DropTable(
                name: "Ribe");

            migrationBuilder.DropTable(
                name: "Rezervoari");
        }
    }
}
