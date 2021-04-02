using Microsoft.EntityFrameworkCore.Migrations;

namespace ToyStore.Repository.Migrations
{
    public partial class migration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Orders_CurrentOrderOrderId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_SellableStacks_Locations_LocationId",
                table: "SellableStacks");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "SellableStacks",
                newName: "locationId");

            migrationBuilder.RenameIndex(
                name: "IX_SellableStacks_LocationId",
                table: "SellableStacks",
                newName: "IX_SellableStacks_locationId");

            migrationBuilder.RenameColumn(
                name: "CurrentOrderOrderId",
                table: "Customers",
                newName: "CurrentOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CurrentOrderOrderId",
                table: "Customers",
                newName: "IX_Customers_CurrentOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Orders_CurrentOrderId",
                table: "Customers",
                column: "CurrentOrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStacks_Locations_locationId",
                table: "SellableStacks",
                column: "locationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Orders_CurrentOrderId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_SellableStacks_Locations_locationId",
                table: "SellableStacks");

            migrationBuilder.RenameColumn(
                name: "locationId",
                table: "SellableStacks",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_SellableStacks_locationId",
                table: "SellableStacks",
                newName: "IX_SellableStacks_LocationId");

            migrationBuilder.RenameColumn(
                name: "CurrentOrderId",
                table: "Customers",
                newName: "CurrentOrderOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CurrentOrderId",
                table: "Customers",
                newName: "IX_Customers_CurrentOrderOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Orders_CurrentOrderOrderId",
                table: "Customers",
                column: "CurrentOrderOrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStacks_Locations_LocationId",
                table: "SellableStacks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
