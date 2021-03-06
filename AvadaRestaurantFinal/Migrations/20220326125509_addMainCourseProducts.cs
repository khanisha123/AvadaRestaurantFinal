using Microsoft.EntityFrameworkCore.Migrations;

namespace AvadaRestaurantFinal.Migrations
{
    public partial class addMainCourseProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainCourseProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DescriptionFront = table.Column<string>(nullable: true),
                    Calories = table.Column<int>(nullable: false),
                    GlutenFree = table.Column<string>(nullable: true),
                    LactoseFree = table.Column<string>(nullable: true),
                    TakeoutSideDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCourseProducts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainCourseProducts");
        }
    }
}
