using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerLeague.UI.Data.Migrations
{
    public partial class AddedAuthorsToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_HostId",
                table: "Matches");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Players",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Matches",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Leagues",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Arenas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_AuthorId",
                table: "Teams",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_AuthorId",
                table: "Players",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AuthorId",
                table: "Matches",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_AuthorId",
                table: "Leagues",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Arenas_AuthorId",
                table: "Arenas",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arenas_AspNetUsers_AuthorId",
                table: "Arenas",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_AspNetUsers_AuthorId",
                table: "Leagues",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_AspNetUsers_AuthorId",
                table: "Matches",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_HostId",
                table: "Matches",
                column: "HostId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_AspNetUsers_AuthorId",
                table: "Players",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_AuthorId",
                table: "Teams",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arenas_AspNetUsers_AuthorId",
                table: "Arenas");

            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_AspNetUsers_AuthorId",
                table: "Leagues");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_AspNetUsers_AuthorId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_HostId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_AspNetUsers_AuthorId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_AuthorId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_AuthorId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_AuthorId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Matches_AuthorId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_AuthorId",
                table: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_Arenas_AuthorId",
                table: "Arenas");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Arenas");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_HostId",
                table: "Matches",
                column: "HostId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
