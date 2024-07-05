using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagesSets_ImageDto_JPGId",
                table: "ImagesSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagesSets_ImageDto_WebPId",
                table: "ImagesSets");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesSets_ImageDto_JPGId",
                table: "ImagesSets",
                column: "JPGId",
                principalTable: "ImageDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesSets_ImageDto_WebPId",
                table: "ImagesSets",
                column: "WebPId",
                principalTable: "ImageDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagesSets_ImageDto_JPGId",
                table: "ImagesSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagesSets_ImageDto_WebPId",
                table: "ImagesSets");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesSets_ImageDto_JPGId",
                table: "ImagesSets",
                column: "JPGId",
                principalTable: "ImageDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesSets_ImageDto_WebPId",
                table: "ImagesSets",
                column: "WebPId",
                principalTable: "ImageDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
