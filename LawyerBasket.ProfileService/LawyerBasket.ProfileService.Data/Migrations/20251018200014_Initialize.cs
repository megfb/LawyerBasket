using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LawyerBasket.ProfileService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expertisement",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expertisement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    GenderId = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NationalId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserProfileId = table.Column<string>(type: "text", nullable: false),
                    AddressLine = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CityId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LawyerProfile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserProfileId = table.Column<string>(type: "text", nullable: false),
                    BarAssociation = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BarNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LicenseNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LicenseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerProfile_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Academy",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LawyerProfileId = table.Column<string>(type: "text", nullable: false),
                    University = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Degree = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Academy_LawyerProfile_LawyerProfileId",
                        column: x => x.LawyerProfileId,
                        principalTable: "LawyerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LawyerProfileId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Institution = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DateReceived = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_LawyerProfile_LawyerProfileId",
                        column: x => x.LawyerProfileId,
                        principalTable: "LawyerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LawyerProfileId = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    AlternatePhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AlternateEmail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_LawyerProfile_LawyerProfileId",
                        column: x => x.LawyerProfileId,
                        principalTable: "LawyerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LawyerProfileId = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Position = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experience_LawyerProfile_LawyerProfileId",
                        column: x => x.LawyerProfileId,
                        principalTable: "LawyerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LawyerExpertisement",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LawyerProfileId = table.Column<string>(type: "text", nullable: false),
                    ExpertisementId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerExpertisement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawyerExpertisement_Expertisement_ExpertisementId",
                        column: x => x.ExpertisementId,
                        principalTable: "Expertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LawyerExpertisement_LawyerProfile_LawyerProfileId",
                        column: x => x.LawyerProfileId,
                        principalTable: "LawyerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { "b1e2c3d4-0001-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "İstanbul", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "b1e2c3d4-0002-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ankara", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "b1e2c3d4-0003-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "İzmir", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "b1e2c3d4-0004-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bursa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "b1e2c3d4-0005-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Antalya", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Expertisement",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { "e1a2b3c4-0001-4f5a-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Criminal Law", "Ceza Hukuku", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e1a2b3c4-0002-4f5a-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Commercial / Corporate Law", "Ticaret / Şirketler Hukuku", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e1a2b3c4-0003-4f5a-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Labor / Employment Law", "İş Hukuku", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e1a2b3c4-0004-4f5a-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Family Law", "Aile Hukuku", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e1a2b3c4-0005-4f5a-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Inheritance Law", "Miras Hukuku", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e1a2b3c4-0006-4f5a-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Real Estate Law", "Gayrimenkul / Emlak Hukuku", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e1a2b3c4-0007-4f5a-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Intellectual Property Law", "Fikri Mülkiyet Hukuku", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e1a2b3c4-0008-4f5a-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tax Law", "Vergi Hukuku", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e1a2b3c4-0009-4f5a-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Administrative Law", "İdare Hukuku", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e1a2b3c4-0010-4f5a-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "International Law", "Uluslararası Hukuk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { "c1d2e3f4-0001-4a5b-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Male", "Male", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "c1d2e3f4-0002-4a5b-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Female", "Female", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Academy_LawyerProfileId",
                table: "Academy",
                column: "LawyerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserProfileId",
                table: "Address",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_LawyerProfileId",
                table: "Certificates",
                column: "LawyerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_LawyerProfileId",
                table: "Contact",
                column: "LawyerProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experience_LawyerProfileId",
                table: "Experience",
                column: "LawyerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerExpertisement_ExpertisementId",
                table: "LawyerExpertisement",
                column: "ExpertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerExpertisement_LawyerProfileId",
                table: "LawyerExpertisement",
                column: "LawyerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerProfile_UserProfileId",
                table: "LawyerProfile",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_GenderId",
                table: "UserProfile",
                column: "GenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Academy");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "LawyerExpertisement");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Expertisement");

            migrationBuilder.DropTable(
                name: "LawyerProfile");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "Gender");
        }
    }
}
