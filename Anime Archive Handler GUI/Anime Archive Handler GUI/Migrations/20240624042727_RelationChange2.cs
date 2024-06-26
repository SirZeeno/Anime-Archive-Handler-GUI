using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AnimeMalId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DemographicsDto_AnimeMalId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ExplicitGenresDto_AnimeMalId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GenresDto_AnimeMalId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProducersDto_AnimeMalId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StudiosDto_AnimeMalId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ThemesDto_AnimeMalId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeMalId",
                table: "MalUrls",
                column: "AnimeMalId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_DemographicsDto_AnimeMalId",
                table: "MalUrls",
                column: "DemographicsDto_AnimeMalId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_ExplicitGenresDto_AnimeMalId",
                table: "MalUrls",
                column: "ExplicitGenresDto_AnimeMalId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_GenresDto_AnimeMalId",
                table: "MalUrls",
                column: "GenresDto_AnimeMalId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_ProducersDto_AnimeMalId",
                table: "MalUrls",
                column: "ProducersDto_AnimeMalId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_StudiosDto_AnimeMalId",
                table: "MalUrls",
                column: "StudiosDto_AnimeMalId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_ThemesDto_AnimeMalId",
                table: "MalUrls",
                column: "ThemesDto_AnimeMalId");

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeMalId",
                table: "MalUrls",
                column: "AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_DemographicsDto_AnimeMalId",
                table: "MalUrls",
                column: "DemographicsDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_ExplicitGenresDto_AnimeMalId",
                table: "MalUrls",
                column: "ExplicitGenresDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_GenresDto_AnimeMalId",
                table: "MalUrls",
                column: "GenresDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_ProducersDto_AnimeMalId",
                table: "MalUrls",
                column: "ProducersDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_StudiosDto_AnimeMalId",
                table: "MalUrls",
                column: "StudiosDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_ThemesDto_AnimeMalId",
                table: "MalUrls",
                column: "ThemesDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_DemographicsDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ExplicitGenresDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_GenresDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ProducersDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_StudiosDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ThemesDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_DemographicsDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_ExplicitGenresDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_GenresDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_ProducersDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_StudiosDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_ThemesDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "DemographicsDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "ExplicitGenresDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "GenresDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "ProducersDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "StudiosDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "ThemesDto_AnimeMalId",
                table: "MalUrls");
        }
    }
}
