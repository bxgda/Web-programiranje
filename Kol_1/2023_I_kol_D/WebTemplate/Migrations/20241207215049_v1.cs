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
                name: "Prodavnice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    ImeZaduzenogLica = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavnice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Identifikator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    DatumProizvodnje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumIstekaRoka = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Magacin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    BrojDostupnih = table.Column<int>(type: "int", nullable: false),
                    ProdavnicaID = table.Column<int>(type: "int", nullable: true),
                    ProizvodID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magacin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Magacin_Prodavnice_ProdavnicaID",
                        column: x => x.ProdavnicaID,
                        principalTable: "Prodavnice",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Magacin_Proizvodi_ProizvodID",
                        column: x => x.ProizvodID,
                        principalTable: "Proizvodi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Magacin_ProdavnicaID",
                table: "Magacin",
                column: "ProdavnicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Magacin_ProizvodID",
                table: "Magacin",
                column: "ProizvodID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Magacin");

            migrationBuilder.DropTable(
                name: "Prodavnice");

            migrationBuilder.DropTable(
                name: "Proizvodi");
        }
    }
}
