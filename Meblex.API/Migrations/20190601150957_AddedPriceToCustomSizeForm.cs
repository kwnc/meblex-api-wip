using Microsoft.EntityFrameworkCore.Migrations;

namespace Meblex.API.Migrations
{
    public partial class AddedPriceToCustomSizeForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "CustomSizeForms",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "CustomSizeForms");
        }
    }
}
