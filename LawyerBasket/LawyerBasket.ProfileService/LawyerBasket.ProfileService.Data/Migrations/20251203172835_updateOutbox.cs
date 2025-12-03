using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerBasket.ProfileService.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "OutboxMessage",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "OutboxMessage",
                newName: "MyProperty");
        }
    }
}
