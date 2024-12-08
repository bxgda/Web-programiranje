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
                name: "Kuvari",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StrucnaSprema = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kuvari", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Restorani",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaksBrojGostiju = table.Column<int>(type: "int", nullable: false),
                    MaksBrojKuvara = table.Column<int>(type: "int", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restorani", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumZaposljenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Plata = table.Column<long>(type: "bigint", nullable: false),
                    Pozicija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestoranID = table.Column<int>(type: "int", nullable: true),
                    KuvarID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zaposlenja_Kuvari_KuvarID",
                        column: x => x.KuvarID,
                        principalTable: "Kuvari",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Zaposlenja_Restorani_RestoranID",
                        column: x => x.RestoranID,
                        principalTable: "Restorani",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenja_KuvarID",
                table: "Zaposlenja",
                column: "KuvarID");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenja_RestoranID",
                table: "Zaposlenja",
                column: "RestoranID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zaposlenja");

            migrationBuilder.DropTable(
                name: "Kuvari");

            migrationBuilder.DropTable(
                name: "Restorani");
        }
    }
}
