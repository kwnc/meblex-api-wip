using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Meblex.API.Migrations
{
    public partial class AddedPhotoToMaterialAndPattern : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialPhotos",
                columns: table => new
                {
                    MaterialPhotoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 132, nullable: false),
                    MaterialId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialPhotos", x => x.MaterialPhotoId);
                    table.ForeignKey(
                        name: "FK_MaterialPhotos_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatternPhotos",
                columns: table => new
                {
                    PatternPhotoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 132, nullable: false),
                    PatternId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternPhotos", x => x.PatternPhotoId);
                    table.ForeignKey(
                        name: "FK_PatternPhotos_Patterns_PatternId",
                        column: x => x.PatternId,
                        principalTable: "Patterns",
                        principalColumn: "PatternId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPhotos_MaterialId",
                table: "MaterialPhotos",
                column: "MaterialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatternPhotos_PatternId",
                table: "PatternPhotos",
                column: "PatternId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialPhotos");

            migrationBuilder.DropTable(
                name: "PatternPhotos");
        }
    }
}
