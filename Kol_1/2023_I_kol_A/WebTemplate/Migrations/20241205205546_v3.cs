using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutorID",
                table: "Knjige",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Knjige_AutorID",
                table: "Knjige",
                column: "AutorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Knjige_Autori_AutorID",
                table: "Knjige",
                column: "AutorID",
                principalTable: "Autori",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Knjige_Autori_AutorID",
                table: "Knjige");

            migrationBuilder.DropIndex(
                name: "IX_Knjige_AutorID",
                table: "Knjige");

            migrationBuilder.DropColumn(
                name: "AutorID",
                table: "Knjige");
        }
    }
}
