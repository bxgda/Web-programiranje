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
                name: "Fakulteti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fakulteti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Smerovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FakultetID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smerovi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Smerovi_Fakulteti_FakultetID",
                        column: x => x.FakultetID,
                        principalTable: "Fakulteti",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojIndeksa = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    GodinaRodjenja = table.Column<int>(type: "int", nullable: false),
                    SrednjaSkola = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FakultetID = table.Column<int>(type: "int", nullable: true),
                    SmerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Studenti_Fakulteti_FakultetID",
                        column: x => x.FakultetID,
                        principalTable: "Fakulteti",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Studenti_Smerovi_SmerID",
                        column: x => x.SmerID,
                        principalTable: "Smerovi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Upisi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumUpisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ESPB = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: true),
                    SmerID = table.Column<int>(type: "int", nullable: true),
                    FakultetID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upisi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Upisi_Fakulteti_FakultetID",
                        column: x => x.FakultetID,
                        principalTable: "Fakulteti",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Upisi_Smerovi_SmerID",
                        column: x => x.SmerID,
                        principalTable: "Smerovi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Upisi_Studenti_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Studenti",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Smerovi_FakultetID",
                table: "Smerovi",
                column: "FakultetID");

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_FakultetID",
                table: "Studenti",
                column: "FakultetID");

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_SmerID",
                table: "Studenti",
                column: "SmerID");

            migrationBuilder.CreateIndex(
                name: "IX_Upisi_FakultetID",
                table: "Upisi",
                column: "FakultetID");

            migrationBuilder.CreateIndex(
                name: "IX_Upisi_SmerID",
                table: "Upisi",
                column: "SmerID");

            migrationBuilder.CreateIndex(
                name: "IX_Upisi_StudentID",
                table: "Upisi",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Upisi");

            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "Smerovi");

            migrationBuilder.DropTable(
                name: "Fakulteti");
        }
    }
}
