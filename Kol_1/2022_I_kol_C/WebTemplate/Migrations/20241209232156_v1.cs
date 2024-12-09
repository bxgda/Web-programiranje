using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bolnice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojOdeljenja = table.Column<int>(type: "int", nullable: false),
                    BrojOsoblja = table.Column<int>(type: "int", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolnice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lekari",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateOnly>(type: "date", nullable: false),
                    DatumDiplomiranja = table.Column<DateOnly>(type: "date", nullable: false),
                    DatumDobijanjaLicence = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekari", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ugovori",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDBroj = table.Column<int>(type: "int", nullable: false),
                    DatumPotpisivanja = table.Column<DateOnly>(type: "date", nullable: false),
                    Specijalnos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P_BolnicaID = table.Column<int>(type: "int", nullable: true),
                    P_LekarID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ugovori", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ugovori_Bolnice_P_BolnicaID",
                        column: x => x.P_BolnicaID,
                        principalTable: "Bolnice",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ugovori_Lekari_P_LekarID",
                        column: x => x.P_LekarID,
                        principalTable: "Lekari",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_P_BolnicaID",
                table: "Ugovori",
                column: "P_BolnicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_P_LekarID",
                table: "Ugovori",
                column: "P_LekarID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ugovori");

            migrationBuilder.DropTable(
                name: "Bolnice");

            migrationBuilder.DropTable(
                name: "Lekari");
        }
    }
}
