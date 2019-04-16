using Microsoft.EntityFrameworkCore.Migrations;

namespace Meblex.API.Migrations
{
    public partial class FinalDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Street",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "NIP",
                table: "Clients",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NIP",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Clients",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }
    }
}
