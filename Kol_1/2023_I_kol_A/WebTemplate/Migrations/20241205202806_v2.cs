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
                name: "Autori",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pol = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autori", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IzdavackeKuce",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzdavackeKuce", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Knjige",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumObjavljivanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojStranica = table.Column<int>(type: "int", nullable: false),
                    Zanr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knjige", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ugovori",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumPotpisivanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojUgovora = table.Column<int>(type: "int", nullable: false),
                    AutorID = table.Column<int>(type: "int", nullable: true),
                    KnjigaFK = table.Column<int>(type: "int", nullable: true),
                    IzdavackaKucaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ugovori", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ugovori_Autori_AutorID",
                        column: x => x.AutorID,
                        principalTable: "Autori",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ugovori_IzdavackeKuce_IzdavackaKucaID",
                        column: x => x.IzdavackaKucaID,
                        principalTable: "IzdavackeKuce",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ugovori_Knjige_KnjigaFK",
                        column: x => x.KnjigaFK,
                        principalTable: "Knjige",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_AutorID",
                table: "Ugovori",
                column: "AutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_IzdavackaKucaID",
                table: "Ugovori",
                column: "IzdavackaKucaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_KnjigaFK",
                table: "Ugovori",
                column: "KnjigaFK",
                unique: true,
                filter: "[KnjigaFK] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ugovori");

            migrationBuilder.DropTable(
                name: "Autori");

            migrationBuilder.DropTable(
                name: "IzdavackeKuce");

            migrationBuilder.DropTable(
                name: "Knjige");
        }
    }
}
