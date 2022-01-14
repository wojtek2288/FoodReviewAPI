using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodReviewAPI.Migrations
{
    public partial class ChangeNickToUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nick",
                table: "Users",
                newName: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Nick");
        }
    }
}
