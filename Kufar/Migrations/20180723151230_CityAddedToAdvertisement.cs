using Microsoft.EntityFrameworkCore.Migrations;

namespace Kufar.Migrations
{
    public partial class CityAddedToAdvertisement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {//
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Advertisements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Advertisements");
        }
    }
}
