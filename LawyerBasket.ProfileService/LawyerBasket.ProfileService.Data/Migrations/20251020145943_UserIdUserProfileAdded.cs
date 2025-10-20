using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerBasket.ProfileService.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserIdUserProfileAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserProfile",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserProfile");
        }
    }
}
