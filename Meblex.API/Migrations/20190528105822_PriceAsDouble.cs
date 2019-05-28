using Microsoft.EntityFrameworkCore.Migrations;

namespace Meblex.API.Migrations
{
    public partial class PriceAsDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Furniture",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Furniture",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
