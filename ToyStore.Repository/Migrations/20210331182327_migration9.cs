using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToyStore.Repository.Migrations
{
    public partial class migration9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellableStacks_Sellables_ItemSellableId",
                table: "SellableStacks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ItemSellableId",
                table: "SellableStacks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStacks_Sellables_ItemSellableId",
                table: "SellableStacks",
                column: "ItemSellableId",
                principalTable: "Sellables",
                principalColumn: "SellableId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellableStacks_Sellables_ItemSellableId",
                table: "SellableStacks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ItemSellableId",
                table: "SellableStacks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStacks_Sellables_ItemSellableId",
                table: "SellableStacks",
                column: "ItemSellableId",
                principalTable: "Sellables",
                principalColumn: "SellableId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
