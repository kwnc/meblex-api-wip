using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Meblex.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 132, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    HexCode = table.Column<string>(maxLength: 7, nullable: false),
                    Slug = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Slug = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "Patterns",
                columns: table => new
                {
                    PatternId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Slug = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patterns", x => x.PatternId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 64, nullable: false),
                    Role = table.Column<string>(maxLength: 32, nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    PieceOfFurnitureId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Size = table.Column<string>(maxLength: 32, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    RoomId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furniture", x => x.PieceOfFurnitureId);
                    table.ForeignKey(
                        name: "FK_Furniture_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Furniture_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Street = table.Column<string>(maxLength: 32, nullable: false),
                    Address = table.Column<string>(maxLength: 32, nullable: false),
                    State = table.Column<string>(maxLength: 32, nullable: false),
                    City = table.Column<string>(maxLength: 32, nullable: false),
                    PostCode = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    PieceOfFurnitureId = table.Column<int>(nullable: false),
                    PatternId = table.Column<int>(nullable: false),
                    ColorId = table.Column<int>(nullable: false),
                    MaterialId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_Parts_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parts_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parts_Patterns_PatternId",
                        column: x => x.PatternId,
                        principalTable: "Patterns",
                        principalColumn: "PatternId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parts_Furniture_PieceOfFurnitureId",
                        column: x => x.PieceOfFurnitureId,
                        principalTable: "Furniture",
                        principalColumn: "PieceOfFurnitureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 132, nullable: false),
                    PieceOfFurnitureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_Photos_Furniture_PieceOfFurnitureId",
                        column: x => x.PieceOfFurnitureId,
                        principalTable: "Furniture",
                        principalColumn: "PieceOfFurnitureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomSizeForms",
                columns: table => new
                {
                    CustomSizeFormId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Size = table.Column<string>(maxLength: 32, nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    PieceOfFurnitureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomSizeForms", x => x.CustomSizeFormId);
                    table.ForeignKey(
                        name: "FK_CustomSizeForms_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomSizeForms_Furniture_PieceOfFurnitureId",
                        column: x => x.PieceOfFurnitureId,
                        principalTable: "Furniture",
                        principalColumn: "PieceOfFurnitureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransactionId = table.Column<int>(nullable: false),
                    Delivery = table.Column<string>(maxLength: 32, nullable: false),
                    Reservation = table.Column<bool>(nullable: false),
                    Street = table.Column<string>(maxLength: 32, nullable: false),
                    Address = table.Column<string>(maxLength: 32, nullable: false),
                    State = table.Column<string>(maxLength: 32, nullable: false),
                    City = table.Column<string>(maxLength: 32, nullable: false),
                    PostCode = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    OrderLineId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Size = table.Column<string>(maxLength: 32, nullable: false),
                    PieceOfFurnitureId = table.Column<int>(nullable: true),
                    PartId = table.Column<int>(nullable: true),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.OrderLineId);
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLines_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_Furniture_PieceOfFurnitureId",
                        column: x => x.PieceOfFurnitureId,
                        principalTable: "Furniture",
                        principalColumn: "PieceOfFurnitureId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomSizeForms_ClientId",
                table: "CustomSizeForms",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomSizeForms_PieceOfFurnitureId",
                table: "CustomSizeForms",
                column: "PieceOfFurnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_CategoryId",
                table: "Furniture",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_RoomId",
                table: "Furniture",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_PartId",
                table: "OrderLines",
                column: "PartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_PieceOfFurnitureId",
                table: "OrderLines",
                column: "PieceOfFurnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ColorId",
                table: "Parts",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_MaterialId",
                table: "Parts",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PatternId",
                table: "Parts",
                column: "PatternId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PieceOfFurnitureId",
                table: "Parts",
                column: "PieceOfFurnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PieceOfFurnitureId",
                table: "Photos",
                column: "PieceOfFurnitureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomSizeForms");

            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Patterns");

            migrationBuilder.DropTable(
                name: "Furniture");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
