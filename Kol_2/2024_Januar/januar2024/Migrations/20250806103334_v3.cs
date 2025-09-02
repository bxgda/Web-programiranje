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
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_Chat_ChatId",
                table: "Korisnik");

            migrationBuilder.DropIndex(
                name: "IX_Korisnik_ChatId",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Korisnik");

            migrationBuilder.AddColumn<int>(
                name: "KorisniciId",
                table: "Chat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chat_KorisniciId",
                table: "Chat",
                column: "KorisniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Korisnik_KorisniciId",
                table: "Chat",
                column: "KorisniciId",
                principalTable: "Korisnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Korisnik_KorisniciId",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_KorisniciId",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "KorisniciId",
                table: "Chat");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "Korisnik",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_ChatId",
                table: "Korisnik",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnik_Chat_ChatId",
                table: "Korisnik",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id");
        }
    }
}
