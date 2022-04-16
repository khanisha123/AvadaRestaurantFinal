using Microsoft.EntityFrameworkCore.Migrations;

namespace AvadaRestaurantFinal.Migrations
{
    public partial class addForDetailNameAndForDetailImageUrlToBulletin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForDetailImageUrl",
                table: "Bulletin",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForDetailName",
                table: "Bulletin",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForDetailImageUrl",
                table: "Bulletin");

            migrationBuilder.DropColumn(
                name: "ForDetailName",
                table: "Bulletin");
        }
    }
}
