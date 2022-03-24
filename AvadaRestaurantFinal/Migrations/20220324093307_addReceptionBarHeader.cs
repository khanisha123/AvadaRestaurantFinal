﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace AvadaRestaurantFinal.Migrations
{
    public partial class addReceptionBarHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceptionBarHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Header = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionBarHeader", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceptionBarHeader");
        }
    }
}
