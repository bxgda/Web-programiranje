using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ribe_Rezervoari_GdeSeNalaziID",
                table: "Ribe");

            migrationBuilder.DropIndex(
                name: "IX_Ribe_GdeSeNalaziID",
                table: "Ribe");

            migrationBuilder.DropColumn(
                name: "GdeSeNalaziID",
                table: "Ribe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GdeSeNalaziID",
                table: "Ribe",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ribe_GdeSeNalaziID",
                table: "Ribe",
                column: "GdeSeNalaziID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ribe_Rezervoari_GdeSeNalaziID",
                table: "Ribe",
                column: "GdeSeNalaziID",
                principalTable: "Rezervoari",
                principalColumn: "ID");
        }
    }
}
