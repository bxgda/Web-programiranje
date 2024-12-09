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
            migrationBuilder.DropForeignKey(
                name: "FK_Albumi_Numere_NumeraAlbumaID",
                table: "Albumi");

            migrationBuilder.DropIndex(
                name: "IX_Albumi_NumeraAlbumaID",
                table: "Albumi");

            migrationBuilder.DropColumn(
                name: "NumeraAlbumaID",
                table: "Albumi");

            migrationBuilder.AddColumn<int>(
                name: "AlbumID",
                table: "Numere",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Numere_AlbumID",
                table: "Numere",
                column: "AlbumID");

            migrationBuilder.AddForeignKey(
                name: "FK_Numere_Albumi_AlbumID",
                table: "Numere",
                column: "AlbumID",
                principalTable: "Albumi",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Numere_Albumi_AlbumID",
                table: "Numere");

            migrationBuilder.DropIndex(
                name: "IX_Numere_AlbumID",
                table: "Numere");

            migrationBuilder.DropColumn(
                name: "AlbumID",
                table: "Numere");

            migrationBuilder.AddColumn<int>(
                name: "NumeraAlbumaID",
                table: "Albumi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albumi_NumeraAlbumaID",
                table: "Albumi",
                column: "NumeraAlbumaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Albumi_Numere_NumeraAlbumaID",
                table: "Albumi",
                column: "NumeraAlbumaID",
                principalTable: "Numere",
                principalColumn: "ID");
        }
    }
}
