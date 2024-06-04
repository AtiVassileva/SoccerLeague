using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerLeague.UI.Data.Migrations
{
    public partial class AddedLogoToLeagues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Leagues");
        }
    }
}
