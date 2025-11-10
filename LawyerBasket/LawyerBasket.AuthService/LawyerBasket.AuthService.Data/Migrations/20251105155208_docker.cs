using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LawyerBasket.AuthService.Data.Migrations
{
    /// <inheritdoc />
    public partial class docker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserRole_AppRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRole_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { "5f1f0a00-cf2e-4ee8-b0e8-23e3a091cdee", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Role for clients", "Client", null },
                    { "8a1f4a29-4f91-4b6b-835b-9c12f89e6f21", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Role for Admin", "Admin", null },
                    { "a1a7a79e-5c53-47e8-b44d-19a98f5ac789", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Role for lawyers", "Lawyer", null },
                    { "f39d0b0f-4a2e-4f9a-a4da-9b61b0b8c112", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Role for User", "User", null }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "CreatedAt", "Email", "LastLoginAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "d9f8c5b2-4a1e-4c3b-9f21-7e2b8c123456", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@admin.com", null, "$2a$11$6zFgHTy5N6mMPRIyEiut1ei.PTzGCZA3wZYWgfRToORZP1oW7qBBi", null });

            migrationBuilder.InsertData(
                table: "AppUserRole",
                columns: new[] { "Id", "CreatedAt", "RoleId", "UpdatedAt", "UserId" },
                values: new object[] { "a3f5d2c7-9b8e-4f1a-92d4-6c3e7b8f1a2b", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "8a1f4a29-4f91-4b6b-835b-9c12f89e6f21", null, "d9f8c5b2-4a1e-4c3b-9f21-7e2b8c123456" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRole_RoleId",
                table: "AppUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRole_UserId",
                table: "AppUserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserRole");

            migrationBuilder.DropTable(
                name: "AppRole");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
