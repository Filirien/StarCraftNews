using Microsoft.EntityFrameworkCore.Migrations;

namespace StarCraftNews.Data.Migrations
{
    public partial class AddDescriptionToNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "News",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "News");
        }
    }
}
