using Microsoft.EntityFrameworkCore.Migrations;

namespace ToyStore.Repository.Migrations
{
    public partial class migration11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellableStacks_Orders_OrderId",
                table: "SellableStacks");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "SellableStacks",
                newName: "orderId");

            migrationBuilder.RenameIndex(
                name: "IX_SellableStacks_OrderId",
                table: "SellableStacks",
                newName: "IX_SellableStacks_orderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStacks_Orders_orderId",
                table: "SellableStacks",
                column: "orderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellableStacks_Orders_orderId",
                table: "SellableStacks");

            migrationBuilder.RenameColumn(
                name: "orderId",
                table: "SellableStacks",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SellableStacks_orderId",
                table: "SellableStacks",
                newName: "IX_SellableStacks_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStacks_Orders_OrderId",
                table: "SellableStacks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
