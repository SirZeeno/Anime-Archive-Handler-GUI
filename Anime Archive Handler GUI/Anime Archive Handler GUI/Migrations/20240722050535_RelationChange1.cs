using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeBroadcasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Day = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Time = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Timezone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    String = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeBroadcasts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    SmallImageUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    MediumImageUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    LargeImageUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    MaximumImageUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimePeriod",
                columns: table => new
                {
                    From = table.Column<DateTime>(type: "TEXT", nullable: true),
                    To = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Titles_fts",
                columns: table => new
                {
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    AnimeId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ImagesSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JPGId = table.Column<int>(type: "INTEGER", nullable: false),
                    WebPId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagesSets_ImageDto_JPGId",
                        column: x => x.JPGId,
                        principalTable: "ImageDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagesSets_ImageDto_WebPId",
                        column: x => x.WebPId,
                        principalTable: "ImageDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageBitmaps_AnimeImageBitmap_WebPId",
                        column: x => x.WebPId,
                        principalTable: "AnimeImageBitmap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    MalId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ImagesId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnimeImageSetBitmapId = table.Column<long>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    TitleEnglish = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    TitleJapanese = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Source = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Episodes = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Airing = table.Column<bool>(type: "INTEGER", nullable: false),
                    Duration = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Rating = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Score = table.Column<double>(type: "REAL", nullable: true),
                    ScoredBy = table.Column<int>(type: "INTEGER", nullable: true),
                    Rank = table.Column<int>(type: "INTEGER", nullable: true),
                    Popularity = table.Column<int>(type: "INTEGER", nullable: true),
                    Members = table.Column<int>(type: "INTEGER", nullable: true),
                    Favorites = table.Column<int>(type: "INTEGER", nullable: true),
                    Synopsis = table.Column<string>(type: "TEXT", nullable: true),
                    Background = table.Column<string>(type: "TEXT", nullable: true),
                    Season = table.Column<int>(type: "INTEGER", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: true),
                    BroadcastId = table.Column<int>(type: "INTEGER", nullable: false),
                    Approved = table.Column<bool>(type: "INTEGER", nullable: false),
                    MalUrlDtoId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.MalId);
                    table.ForeignKey(
                        name: "FK_Animes_AnimeBroadcasts_BroadcastId",
                        column: x => x.BroadcastId,
                        principalTable: "AnimeBroadcasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Animes_ImageBitmaps_AnimeImageSetBitmapId",
                        column: x => x.AnimeImageSetBitmapId,
                        principalTable: "ImageBitmaps",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Animes_ImagesSets_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "ImagesSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeTrailers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    YoutubeId = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Url = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    EmbedUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ImageId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnimeId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeTrailers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeTrailers_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeTrailers_ImageDto_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ImageDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MalUrls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Url = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    AnimeId = table.Column<long>(type: "INTEGER", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MalUrls_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimePeriodDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    From = table.Column<DateTime>(type: "TEXT", nullable: true),
                    To = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AnimeId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriodDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimePeriodDto_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitleEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    AnimeId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitleEntries_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitleSynonyms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Synonym = table.Column<string>(type: "TEXT", nullable: true),
                    AnimeId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleSynonyms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitleSynonyms_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeImageBitmap_AnimeImageSetBitmapId",
                table: "AnimeImageBitmap",
                column: "AnimeImageSetBitmapId");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_AnimeImageSetBitmapId",
                table: "Animes",
                column: "AnimeImageSetBitmapId");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_BroadcastId",
                table: "Animes",
                column: "BroadcastId");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_ImagesId",
                table: "Animes",
                column: "ImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_MalUrlDtoId",
                table: "Animes",
                column: "MalUrlDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeTrailers_AnimeId",
                table: "AnimeTrailers",
                column: "AnimeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimeTrailers_ImageId",
                table: "AnimeTrailers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageBitmaps_JPGId",
                table: "ImageBitmaps",
                column: "JPGId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageBitmaps_WebPId",
                table: "ImageBitmaps",
                column: "WebPId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesSets_JPGId",
                table: "ImagesSets",
                column: "JPGId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesSets_WebPId",
                table: "ImagesSets",
                column: "WebPId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeId",
                table: "MalUrls",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimePeriodDto_AnimeId",
                table: "TimePeriodDto",
                column: "AnimeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TitleEntries_AnimeId",
                table: "TitleEntries",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleSynonyms_AnimeId",
                table: "TitleSynonyms",
                column: "AnimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeImageBitmap_ImageBitmaps_AnimeImageSetBitmapId",
                table: "AnimeImageBitmap",
                column: "AnimeImageSetBitmapId",
                principalTable: "ImageBitmaps",
                principalColumn: "MalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_MalUrls_MalUrlDtoId",
                table: "Animes",
                column: "MalUrlDtoId",
                principalTable: "MalUrls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeImageBitmap_ImageBitmaps_AnimeImageSetBitmapId",
                table: "AnimeImageBitmap");

            migrationBuilder.DropForeignKey(
                name: "FK_Animes_ImageBitmaps_AnimeImageSetBitmapId",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_Animes_AnimeBroadcasts_BroadcastId",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_Animes_ImagesSets_ImagesId",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_Animes_MalUrls_MalUrlDtoId",
                table: "Animes");

            migrationBuilder.DropTable(
                name: "AnimeTrailers");

            migrationBuilder.DropTable(
                name: "TimePeriod");

            migrationBuilder.DropTable(
                name: "TimePeriodDto");

            migrationBuilder.DropTable(
                name: "TitleEntries");

            migrationBuilder.DropTable(
                name: "Titles_fts");

            migrationBuilder.DropTable(
                name: "TitleSynonyms");

            migrationBuilder.DropTable(
                name: "ImageBitmaps");

            migrationBuilder.DropTable(
                name: "AnimeImageBitmap");

            migrationBuilder.DropTable(
                name: "AnimeBroadcasts");

            migrationBuilder.DropTable(
                name: "ImagesSets");

            migrationBuilder.DropTable(
                name: "ImageDto");

            migrationBuilder.DropTable(
                name: "MalUrls");

            migrationBuilder.DropTable(
                name: "Animes");
        }
    }
}
