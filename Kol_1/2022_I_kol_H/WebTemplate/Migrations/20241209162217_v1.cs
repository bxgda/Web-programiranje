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
                name: "Autori",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumPrvogAlbuma = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autori", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Numere",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Duzina = table.Column<double>(type: "float", nullable: true),
                    Zanr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojUmetnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numere", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Albumi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GodinaIzdavanja = table.Column<int>(type: "int", nullable: false),
                    IzdavackaKuca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutorAlbumaID = table.Column<int>(type: "int", nullable: true),
                    NumeraAlbumaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albumi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Albumi_Autori_AutorAlbumaID",
                        column: x => x.AutorAlbumaID,
                        principalTable: "Autori",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Albumi_Numere_NumeraAlbumaID",
                        column: x => x.NumeraAlbumaID,
                        principalTable: "Numere",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albumi_AutorAlbumaID",
                table: "Albumi",
                column: "AutorAlbumaID");

            migrationBuilder.CreateIndex(
                name: "IX_Albumi_NumeraAlbumaID",
                table: "Albumi",
                column: "NumeraAlbumaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albumi");

            migrationBuilder.DropTable(
                name: "Autori");

            migrationBuilder.DropTable(
                name: "Numere");
        }
    }
}
