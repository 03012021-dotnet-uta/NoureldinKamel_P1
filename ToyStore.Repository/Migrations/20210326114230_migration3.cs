using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToyStore.Repository.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Sellables_SellableId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_SellableId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "SellableId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "SellableTag",
                columns: table => new
                {
                    TagSellablesSellableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsTagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellableTag", x => new { x.TagSellablesSellableId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_SellableTag_Sellables_TagSellablesSellableId",
                        column: x => x.TagSellablesSellableId,
                        principalTable: "Sellables",
                        principalColumn: "SellableId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellableTag_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SellableTag_TagsTagId",
                table: "SellableTag",
                column: "TagsTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellableTag");

            migrationBuilder.AddColumn<Guid>(
                name: "SellableId",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SellableId",
                table: "Tags",
                column: "SellableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Sellables_SellableId",
                table: "Tags",
                column: "SellableId",
                principalTable: "Sellables",
                principalColumn: "SellableId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
