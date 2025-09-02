using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class vv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Korisnik_KorisniciId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Soba_SobaId",
                table: "Chat");

            migrationBuilder.AlterColumn<int>(
                name: "SobaId",
                table: "Chat",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KorisniciId",
                table: "Chat",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Boja",
                table: "Chat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Korisnik_KorisniciId",
                table: "Chat",
                column: "KorisniciId",
                principalTable: "Korisnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Soba_SobaId",
                table: "Chat",
                column: "SobaId",
                principalTable: "Soba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Korisnik_KorisniciId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Soba_SobaId",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "Boja",
                table: "Chat");

            migrationBuilder.AlterColumn<int>(
                name: "SobaId",
                table: "Chat",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "KorisniciId",
                table: "Chat",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Korisnik_KorisniciId",
                table: "Chat",
                column: "KorisniciId",
                principalTable: "Korisnik",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Soba_SobaId",
                table: "Chat",
                column: "SobaId",
                principalTable: "Soba",
                principalColumn: "Id");
        }
    }
}
