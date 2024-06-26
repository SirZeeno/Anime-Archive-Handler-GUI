using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_AnimeTrailers_TrailerYoutubeId",
                table: "Animes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimeTrailers",
                table: "AnimeTrailers");

            migrationBuilder.DropIndex(
                name: "IX_Animes_TrailerYoutubeId",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "TrailerYoutubeId",
                table: "Animes");

            migrationBuilder.AddColumn<string>(
                name: "LargeImageUrl",
                table: "ImageDto",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaximumImageUrl",
                table: "ImageDto",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MediumImageUrl",
                table: "ImageDto",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SmallImageUrl",
                table: "ImageDto",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AnimeTrailers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<long>(
                name: "AnimeId",
                table: "AnimeTrailers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimeTrailers",
                table: "AnimeTrailers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeTrailers_AnimeId",
                table: "AnimeTrailers",
                column: "AnimeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeTrailers_Animes_AnimeId",
                table: "AnimeTrailers",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeTrailers_Animes_AnimeId",
                table: "AnimeTrailers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimeTrailers",
                table: "AnimeTrailers");

            migrationBuilder.DropIndex(
                name: "IX_AnimeTrailers_AnimeId",
                table: "AnimeTrailers");

            migrationBuilder.DropColumn(
                name: "LargeImageUrl",
                table: "ImageDto");

            migrationBuilder.DropColumn(
                name: "MaximumImageUrl",
                table: "ImageDto");

            migrationBuilder.DropColumn(
                name: "MediumImageUrl",
                table: "ImageDto");

            migrationBuilder.DropColumn(
                name: "SmallImageUrl",
                table: "ImageDto");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AnimeTrailers");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "AnimeTrailers");

            migrationBuilder.AddColumn<string>(
                name: "TrailerYoutubeId",
                table: "Animes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimeTrailers",
                table: "AnimeTrailers",
                column: "YoutubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_TrailerYoutubeId",
                table: "Animes",
                column: "TrailerYoutubeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_AnimeTrailers_TrailerYoutubeId",
                table: "Animes",
                column: "TrailerYoutubeId",
                principalTable: "AnimeTrailers",
                principalColumn: "YoutubeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
