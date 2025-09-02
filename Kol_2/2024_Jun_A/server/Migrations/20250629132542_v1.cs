using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rent_a_car.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Automobili",
                columns: table => new
                {
                    AutomobilID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredjenaKilometraza = table.Column<int>(type: "int", nullable: false),
                    Godiste = table.Column<int>(type: "int", nullable: false),
                    CenaPoDanu = table.Column<int>(type: "int", nullable: false),
                    Iznajmljen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobili", x => x.AutomobilID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    BrojVozackeDozvole = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikID);
                });

            migrationBuilder.CreateTable(
                name: "UgovoriIznajmljivanja",
                columns: table => new
                {
                    IznajmljivanjeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojDana = table.Column<int>(type: "int", nullable: false),
                    KorisnikID = table.Column<int>(type: "int", nullable: true),
                    AutomobilID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UgovoriIznajmljivanja", x => x.IznajmljivanjeID);
                    table.ForeignKey(
                        name: "FK_UgovoriIznajmljivanja_Automobili_AutomobilID",
                        column: x => x.AutomobilID,
                        principalTable: "Automobili",
                        principalColumn: "AutomobilID");
                    table.ForeignKey(
                        name: "FK_UgovoriIznajmljivanja_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UgovoriIznajmljivanja_AutomobilID",
                table: "UgovoriIznajmljivanja",
                column: "AutomobilID");

            migrationBuilder.CreateIndex(
                name: "IX_UgovoriIznajmljivanja_KorisnikID",
                table: "UgovoriIznajmljivanja",
                column: "KorisnikID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UgovoriIznajmljivanja");

            migrationBuilder.DropTable(
                name: "Automobili");

            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
