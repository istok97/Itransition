using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Kufar.Migrations
{
    public partial class PhotoAddedToAdvertisement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Advertisements",
                newName: "Photo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Advertisements",
                newName: "State");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Advertisements",
                nullable: true);
        }
    }
}
