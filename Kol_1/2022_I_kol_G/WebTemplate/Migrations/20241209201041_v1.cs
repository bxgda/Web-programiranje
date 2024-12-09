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
                name: "Nekretnine",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vrednost = table.Column<double>(type: "float", nullable: false),
                    BrojPrethnodnihVlasnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nekretnine", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vlasnici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MestoRodjenja = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vlasnici", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kupovine",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojUgovora = table.Column<int>(type: "int", nullable: false),
                    DatumKupovime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsplacenaVrednost = table.Column<double>(type: "float", nullable: true),
                    KupljenaNekretninaID = table.Column<int>(type: "int", nullable: false),
                    KoJeKupioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupovine", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Kupovine_Nekretnine_KupljenaNekretninaID",
                        column: x => x.KupljenaNekretninaID,
                        principalTable: "Nekretnine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kupovine_Vlasnici_KoJeKupioID",
                        column: x => x.KoJeKupioID,
                        principalTable: "Vlasnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kupovine_KoJeKupioID",
                table: "Kupovine",
                column: "KoJeKupioID");

            migrationBuilder.CreateIndex(
                name: "IX_Kupovine_KupljenaNekretninaID",
                table: "Kupovine",
                column: "KupljenaNekretninaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kupovine");

            migrationBuilder.DropTable(
                name: "Nekretnine");

            migrationBuilder.DropTable(
                name: "Vlasnici");
        }
    }
}
