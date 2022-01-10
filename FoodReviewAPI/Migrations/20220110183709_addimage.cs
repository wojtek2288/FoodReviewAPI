using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodReviewAPI.Migrations
{
    public partial class addimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "MenuItems");
        }
    }
}
