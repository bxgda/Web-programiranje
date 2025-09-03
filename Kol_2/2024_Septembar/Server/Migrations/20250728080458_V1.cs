using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stanovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vlasnik = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Povrsina = table.Column<int>(type: "int", nullable: false),
                    BrojClanova = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanovi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Racuni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StanID = table.Column<int>(type: "int", nullable: false),
                    Mesec = table.Column<int>(type: "int", nullable: false),
                    CenaVode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racuni", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Racuni_Stanovi_StanID",
                        column: x => x.StanID,
                        principalTable: "Stanovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_StanID",
                table: "Racuni",
                column: "StanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Racuni");

            migrationBuilder.DropTable(
                name: "Stanovi");
        }
    }
}
