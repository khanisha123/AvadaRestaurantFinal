using Microsoft.EntityFrameworkCore.Migrations;

namespace AvadaRestaurantFinal.Migrations
{
    public partial class addFixx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservations_Table_TableId",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_reservations_TableId",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Table");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "reservations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Table",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_reservations_TableId",
                table: "reservations",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_Table_TableId",
                table: "reservations",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
