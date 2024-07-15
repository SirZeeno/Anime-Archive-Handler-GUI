using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange35 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeImageBitmap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageBitmap = table.Column<byte[]>(type: "BLOB", nullable: true),
                    SmallImageBitmap = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MediumImageBitmap = table.Column<byte[]>(type: "BLOB", nullable: true),
                    LargeImageBitmap = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MaximumImageBitmap = table.Column<byte[]>(type: "BLOB", nullable: true),
                    AnimeImageSetBitmapId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeImageBitmap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageBitmaps",
                columns: table => new
                {
                    MalId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JPGId = table.Column<int>(type: "INTEGER", nullable: false),
                    WebPId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageBitmaps", x => x.MalId);
                    table.ForeignKey(
                        name: "FK_ImageBitmaps_AnimeImageBitmap_JPGId",
                        column: x => x.JPGId,
                        principalTable: "AnimeImageBitmap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageBitmaps_AnimeImageBitmap_WebPId",
                        column: x => x.WebPId,
                        principalTable: "AnimeImageBitmap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeImageBitmap_AnimeImageSetBitmapId",
                table: "AnimeImageBitmap",
                column: "AnimeImageSetBitmapId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageBitmaps_JPGId",
                table: "ImageBitmaps",
                column: "JPGId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageBitmaps_WebPId",
                table: "ImageBitmaps",
                column: "WebPId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeImageBitmap_ImageBitmaps_AnimeImageSetBitmapId",
                table: "AnimeImageBitmap",
                column: "AnimeImageSetBitmapId",
                principalTable: "ImageBitmaps",
                principalColumn: "MalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeImageBitmap_ImageBitmaps_AnimeImageSetBitmapId",
                table: "AnimeImageBitmap");

            migrationBuilder.DropTable(
                name: "ImageBitmaps");

            migrationBuilder.DropTable(
                name: "AnimeImageBitmap");
        }
    }
}
