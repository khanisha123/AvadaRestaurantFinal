using Microsoft.EntityFrameworkCore.Migrations;

namespace AvadaRestaurantFinal.Migrations
{
    public partial class addBulletin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "News",
                table: "Bulletin");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Bulletin",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Bulletin");

            migrationBuilder.AddColumn<string>(
                name: "News",
                table: "Bulletin",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
