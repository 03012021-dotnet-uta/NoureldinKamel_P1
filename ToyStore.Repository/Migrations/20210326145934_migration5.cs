using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToyStore.Repository.Migrations
{
    public partial class migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellableStacks_Sellable_ItemSellableId",
                table: "SellableStacks");

            migrationBuilder.DropForeignKey(
                name: "FK_SellableTag_Sellable_TagSellablesSellableId",
                table: "SellableTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sellable",
                table: "Sellable");

            migrationBuilder.RenameTable(
                name: "Sellable",
                newName: "Sellables");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentOfferSellableId",
                table: "Sellables",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sellables",
                table: "Sellables",
                column: "SellableId");

            migrationBuilder.CreateIndex(
                name: "IX_Sellables_CurrentOfferSellableId",
                table: "Sellables",
                column: "CurrentOfferSellableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellables_Sellables_CurrentOfferSellableId",
                table: "Sellables",
                column: "CurrentOfferSellableId",
                principalTable: "Sellables",
                principalColumn: "SellableId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStacks_Sellables_ItemSellableId",
                table: "SellableStacks",
                column: "ItemSellableId",
                principalTable: "Sellables",
                principalColumn: "SellableId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellableTag_Sellables_TagSellablesSellableId",
                table: "SellableTag",
                column: "TagSellablesSellableId",
                principalTable: "Sellables",
                principalColumn: "SellableId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellables_Sellables_CurrentOfferSellableId",
                table: "Sellables");

            migrationBuilder.DropForeignKey(
                name: "FK_SellableStacks_Sellables_ItemSellableId",
                table: "SellableStacks");

            migrationBuilder.DropForeignKey(
                name: "FK_SellableTag_Sellables_TagSellablesSellableId",
                table: "SellableTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sellables",
                table: "Sellables");

            migrationBuilder.DropIndex(
                name: "IX_Sellables_CurrentOfferSellableId",
                table: "Sellables");

            migrationBuilder.DropColumn(
                name: "CurrentOfferSellableId",
                table: "Sellables");

            migrationBuilder.RenameTable(
                name: "Sellables",
                newName: "Sellable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sellable",
                table: "Sellable",
                column: "SellableId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellableStacks_Sellable_ItemSellableId",
                table: "SellableStacks",
                column: "ItemSellableId",
                principalTable: "Sellable",
                principalColumn: "SellableId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellableTag_Sellable_TagSellablesSellableId",
                table: "SellableTag",
                column: "TagSellablesSellableId",
                principalTable: "Sellable",
                principalColumn: "SellableId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
