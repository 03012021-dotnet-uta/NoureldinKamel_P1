using Microsoft.EntityFrameworkCore.Migrations;

namespace ToyStore.Repository.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellableStack_Locations_LocationId",
                table: "SellableStack");

            migrationBuilder.DropForeignKey(
                name: "FK_SellableStack_Orders_OrderId",
                table: "SellableStack");

            migrationBuilder.DropForeignKey(
                name: "FK_SellableStack_Sellables_ItemSellableId",
                table: "SellableStack");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellableStack",
                table: "SellableStack");

            migrationBuilder.RenameTable(
                name: "SellableStack",
                newName: "SellableStacks");

            migrationBuilder.RenameIndex(
                name: "IX_SellableStack_OrderId",
                table: "SellableStacks",
                newName: "IX_SellableStacks_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SellableStack_LocationId",
                table: "SellableStacks",
                newName: "IX_SellableStacks_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_SellableStack_ItemSellableId",
                table: "SellableStacks",
                newName: "IX_SellableStacks_ItemSellableId");

            migrationBuilder.AddColumn<string>(
                name: "TagName",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellableStacks",
                table: "SellableStacks",
                column: "SellableStackId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagName",
                table: "Tags",
                column: "TagName",
                unique: true,
                filter: "[TagName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStacks_Locations_LocationId",
                table: "SellableStacks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStacks_Orders_OrderId",
                table: "SellableStacks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStacks_Sellables_ItemSellableId",
                table: "SellableStacks",
                column: "ItemSellableId",
                principalTable: "Sellables",
                principalColumn: "SellableId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellableStacks_Locations_LocationId",
                table: "SellableStacks");

            migrationBuilder.DropForeignKey(
                name: "FK_SellableStacks_Orders_OrderId",
                table: "SellableStacks");

            migrationBuilder.DropForeignKey(
                name: "FK_SellableStacks_Sellables_ItemSellableId",
                table: "SellableStacks");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TagName",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellableStacks",
                table: "SellableStacks");

            migrationBuilder.DropColumn(
                name: "TagName",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "SellableStacks",
                newName: "SellableStack");

            migrationBuilder.RenameIndex(
                name: "IX_SellableStacks_OrderId",
                table: "SellableStack",
                newName: "IX_SellableStack_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SellableStacks_LocationId",
                table: "SellableStack",
                newName: "IX_SellableStack_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_SellableStacks_ItemSellableId",
                table: "SellableStack",
                newName: "IX_SellableStack_ItemSellableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellableStack",
                table: "SellableStack",
                column: "SellableStackId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStack_Locations_LocationId",
                table: "SellableStack",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStack_Orders_OrderId",
                table: "SellableStack",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStack_Sellables_ItemSellableId",
                table: "SellableStack",
                column: "ItemSellableId",
                principalTable: "Sellables",
                principalColumn: "SellableId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
