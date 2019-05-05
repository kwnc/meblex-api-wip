using Microsoft.EntityFrameworkCore.Migrations;

namespace Meblex.API.Migrations
{
    public partial class FixedNIP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NIP",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NIP",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
