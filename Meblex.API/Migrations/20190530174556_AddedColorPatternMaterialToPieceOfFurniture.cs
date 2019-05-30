using Microsoft.EntityFrameworkCore.Migrations;

namespace Meblex.API.Migrations
{
    public partial class AddedColorPatternMaterialToPieceOfFurniture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Furniture",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Furniture",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatternId",
                table: "Furniture",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_ColorId",
                table: "Furniture",
                column: "ColorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_MaterialId",
                table: "Furniture",
                column: "MaterialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_PatternId",
                table: "Furniture",
                column: "PatternId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Furniture_Colors_ColorId",
                table: "Furniture",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Furniture_Materials_MaterialId",
                table: "Furniture",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Furniture_Patterns_PatternId",
                table: "Furniture",
                column: "PatternId",
                principalTable: "Patterns",
                principalColumn: "PatternId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furniture_Colors_ColorId",
                table: "Furniture");

            migrationBuilder.DropForeignKey(
                name: "FK_Furniture_Materials_MaterialId",
                table: "Furniture");

            migrationBuilder.DropForeignKey(
                name: "FK_Furniture_Patterns_PatternId",
                table: "Furniture");

            migrationBuilder.DropIndex(
                name: "IX_Furniture_ColorId",
                table: "Furniture");

            migrationBuilder.DropIndex(
                name: "IX_Furniture_MaterialId",
                table: "Furniture");

            migrationBuilder.DropIndex(
                name: "IX_Furniture_PatternId",
                table: "Furniture");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Furniture");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Furniture");

            migrationBuilder.DropColumn(
                name: "PatternId",
                table: "Furniture");
        }
    }
}
