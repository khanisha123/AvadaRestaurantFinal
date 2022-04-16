using Microsoft.EntityFrameworkCore.Migrations;

namespace AvadaRestaurantFinal.Migrations
{
    public partial class addRelationWithReservationAndAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "reservations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_reservations_AppUserId",
                table: "reservations",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_AspNetUsers_AppUserId",
                table: "reservations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservations_AspNetUsers_AppUserId",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_reservations_AppUserId",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "reservations");
        }
    }
}
