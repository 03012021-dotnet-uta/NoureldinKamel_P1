using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToyStore.Repository.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Sellables",
                columns: table => new
                {
                    SellableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellableDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellablePrice = table.Column<double>(type: "float", nullable: false),
                    SellableImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellables", x => x.SellableId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tags_Sellables_SellableId",
                        column: x => x.SellableId,
                        principalTable: "Sellables",
                        principalColumn: "SellableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderLocationLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderedByCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Locations_OrderLocationLocationId",
                        column: x => x.OrderLocationLocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerUName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerPass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentOrderOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Orders_CurrentOrderOrderId",
                        column: x => x.CurrentOrderOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellableStack",
                columns: table => new
                {
                    SellableStackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemSellableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellableStack", x => x.SellableStackId);
                    table.ForeignKey(
                        name: "FK_SellableStack_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SellableStack_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SellableStack_Sellables_ItemSellableId",
                        column: x => x.ItemSellableId,
                        principalTable: "Sellables",
                        principalColumn: "SellableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CurrentOrderOrderId",
                table: "Customers",
                column: "CurrentOrderOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderedByCustomerId",
                table: "Orders",
                column: "OrderedByCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderLocationLocationId",
                table: "Orders",
                column: "OrderLocationLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SellableStack_ItemSellableId",
                table: "SellableStack",
                column: "ItemSellableId");

            migrationBuilder.CreateIndex(
                name: "IX_SellableStack_LocationId",
                table: "SellableStack",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SellableStack_OrderId",
                table: "SellableStack",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SellableId",
                table: "Tags",
                column: "SellableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_OrderedByCustomerId",
                table: "Orders",
                column: "OrderedByCustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Orders_CurrentOrderOrderId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "SellableStack");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Sellables");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
