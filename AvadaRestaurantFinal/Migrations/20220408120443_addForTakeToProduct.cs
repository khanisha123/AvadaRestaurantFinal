using Microsoft.EntityFrameworkCore.Migrations;

namespace AvadaRestaurantFinal.Migrations
{
    public partial class addForTakeToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ForTake",
                table: "products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForTake",
                table: "products");
        }
    }
}
