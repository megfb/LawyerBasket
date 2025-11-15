using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LawyerBasket.ProfileService.Data.Migrations
{
    /// <inheritdoc />
    public partial class improvment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "LawyerProfile",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Certificates",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Degree",
                table: "Academy",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Academy",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0001-4f5a-8c9b-1a2b3c4d5e6f",
                column: "Name",
                value: "Adana");

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0002-4f5a-8c9b-1a2b3c4d5e6f",
                column: "Name",
                value: "Adıyaman");

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0003-4f5a-8c9b-1a2b3c4d5e6f",
                column: "Name",
                value: "Afyonkarahisar");

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0004-4f5a-8c9b-1a2b3c4d5e6f",
                column: "Name",
                value: "Ağrı");

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0005-4f5a-8c9b-1a2b3c4d5e6f",
                column: "Name",
                value: "Amasya");

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { "b1e2c3d4-0006-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ankara", null },
                    { "b1e2c3d4-0007-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Antalya", null },
                    { "b1e2c3d4-0008-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Artvin", null },
                    { "b1e2c3d4-0009-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Aydın", null },
                    { "b1e2c3d4-0010-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Balıkesir", null },
                    { "b1e2c3d4-0011-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bilecik", null },
                    { "b1e2c3d4-0012-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bingöl", null },
                    { "b1e2c3d4-0013-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bitlis", null },
                    { "b1e2c3d4-0014-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bolu", null },
                    { "b1e2c3d4-0015-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Burdur", null },
                    { "b1e2c3d4-0016-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bursa", null },
                    { "b1e2c3d4-0017-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Çanakkale", null },
                    { "b1e2c3d4-0018-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Çankırı", null },
                    { "b1e2c3d4-0019-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Çorum", null },
                    { "b1e2c3d4-0020-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Denizli", null },
                    { "b1e2c3d4-0021-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Diyarbakır", null },
                    { "b1e2c3d4-0022-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Edirne", null },
                    { "b1e2c3d4-0023-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Elazığ", null },
                    { "b1e2c3d4-0024-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Erzincan", null },
                    { "b1e2c3d4-0025-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Erzurum", null },
                    { "b1e2c3d4-0026-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Eskişehir", null },
                    { "b1e2c3d4-0027-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gaziantep", null },
                    { "b1e2c3d4-0028-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Giresun", null },
                    { "b1e2c3d4-0029-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gümüşhane", null },
                    { "b1e2c3d4-0030-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hakkari", null },
                    { "b1e2c3d4-0031-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hatay", null },
                    { "b1e2c3d4-0032-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Isparta", null },
                    { "b1e2c3d4-0033-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mersin", null },
                    { "b1e2c3d4-0034-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "İstanbul", null },
                    { "b1e2c3d4-0035-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "İzmir", null },
                    { "b1e2c3d4-0036-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kars", null },
                    { "b1e2c3d4-0037-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kastamonu", null },
                    { "b1e2c3d4-0038-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kayseri", null },
                    { "b1e2c3d4-0039-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kırklareli", null },
                    { "b1e2c3d4-0040-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kırşehir", null },
                    { "b1e2c3d4-0041-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kocaeli", null },
                    { "b1e2c3d4-0042-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Konya", null },
                    { "b1e2c3d4-0043-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kütahya", null },
                    { "b1e2c3d4-0044-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Malatya", null },
                    { "b1e2c3d4-0045-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Manisa", null },
                    { "b1e2c3d4-0046-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kahramanmaraş", null },
                    { "b1e2c3d4-0047-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mardin", null },
                    { "b1e2c3d4-0048-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Muğla", null },
                    { "b1e2c3d4-0049-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Muş", null },
                    { "b1e2c3d4-0050-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Nevşehir", null },
                    { "b1e2c3d4-0051-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Niğde", null },
                    { "b1e2c3d4-0052-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ordu", null },
                    { "b1e2c3d4-0053-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rize", null },
                    { "b1e2c3d4-0054-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sakarya", null },
                    { "b1e2c3d4-0055-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Samsun", null },
                    { "b1e2c3d4-0056-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Siirt", null },
                    { "b1e2c3d4-0057-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sinop", null },
                    { "b1e2c3d4-0058-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sivas", null },
                    { "b1e2c3d4-0059-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tekirdağ", null },
                    { "b1e2c3d4-0060-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tokat", null },
                    { "b1e2c3d4-0061-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Trabzon", null },
                    { "b1e2c3d4-0062-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tunceli", null },
                    { "b1e2c3d4-0063-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Şanlıurfa", null },
                    { "b1e2c3d4-0064-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Uşak", null },
                    { "b1e2c3d4-0065-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Van", null },
                    { "b1e2c3d4-0066-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yozgat", null },
                    { "b1e2c3d4-0067-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Zonguldak", null },
                    { "b1e2c3d4-0068-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Aksaray", null },
                    { "b1e2c3d4-0069-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bayburt", null },
                    { "b1e2c3d4-0070-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Karaman", null },
                    { "b1e2c3d4-0071-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kırıkkale", null },
                    { "b1e2c3d4-0072-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Batman", null },
                    { "b1e2c3d4-0073-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Şırnak", null },
                    { "b1e2c3d4-0074-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bartın", null },
                    { "b1e2c3d4-0075-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ardahan", null },
                    { "b1e2c3d4-0076-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Iğdır", null },
                    { "b1e2c3d4-0077-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yalova", null },
                    { "b1e2c3d4-0078-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Karabük", null },
                    { "b1e2c3d4-0079-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kilis", null },
                    { "b1e2c3d4-0080-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Osmaniye", null },
                    { "b1e2c3d4-0081-4f5a-8c9b-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Düzce", null }
                });

            migrationBuilder.UpdateData(
                table: "Gender",
                keyColumn: "Id",
                keyValue: "c1d2e3f4-0001-4a5b-8c9d-1a2b3c4d5e6f",
                columns: new[] { "Description", "Name" },
                values: new object[] { "Erkek", "Erkek" });

            migrationBuilder.UpdateData(
                table: "Gender",
                keyColumn: "Id",
                keyValue: "c1d2e3f4-0002-4a5b-8c9d-1a2b3c4d5e6f",
                columns: new[] { "Description", "Name" },
                values: new object[] { "Kadın", "Kadın" });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { "c1d2e3f4-0003-4a5b-8c9d-1a2b3c4d5e6f", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Belirtmek İstemiyorum", "Belirtmek İstemiyorum", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0006-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0007-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0008-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0009-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0010-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0011-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0012-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0013-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0014-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0015-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0016-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0017-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0018-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0019-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0020-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0021-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0022-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0023-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0024-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0025-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0026-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0027-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0028-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0029-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0030-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0031-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0032-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0033-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0034-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0035-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0036-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0037-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0038-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0039-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0040-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0041-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0042-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0043-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0044-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0045-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0046-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0047-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0048-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0049-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0050-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0051-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0052-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0053-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0054-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0055-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0056-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0057-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0058-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0059-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0060-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0061-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0062-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0063-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0064-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0065-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0066-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0067-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0068-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0069-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0070-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0071-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0072-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0073-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0074-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0075-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0076-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0077-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0078-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0079-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0080-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0081-4f5a-8c9b-1a2b3c4d5e6f");

            migrationBuilder.DeleteData(
                table: "Gender",
                keyColumn: "Id",
                keyValue: "c1d2e3f4-0003-4a5b-8c9d-1a2b3c4d5e6f");

            migrationBuilder.DropColumn(
                name: "About",
                table: "LawyerProfile");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Academy");

            migrationBuilder.AlterColumn<string>(
                name: "Degree",
                table: "Academy",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0001-4f5a-8c9b-1a2b3c4d5e6f",
                column: "Name",
                value: "İstanbul");

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0002-4f5a-8c9b-1a2b3c4d5e6f",
                column: "Name",
                value: "Ankara");

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0003-4f5a-8c9b-1a2b3c4d5e6f",
                column: "Name",
                value: "İzmir");

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0004-4f5a-8c9b-1a2b3c4d5e6f",
                column: "Name",
                value: "Bursa");

            migrationBuilder.UpdateData(
                table: "City",
                keyColumn: "Id",
                keyValue: "b1e2c3d4-0005-4f5a-8c9b-1a2b3c4d5e6f",
                column: "Name",
                value: "Antalya");

            migrationBuilder.UpdateData(
                table: "Gender",
                keyColumn: "Id",
                keyValue: "c1d2e3f4-0001-4a5b-8c9d-1a2b3c4d5e6f",
                columns: new[] { "Description", "Name" },
                values: new object[] { "Male", "Male" });

            migrationBuilder.UpdateData(
                table: "Gender",
                keyColumn: "Id",
                keyValue: "c1d2e3f4-0002-4a5b-8c9d-1a2b3c4d5e6f",
                columns: new[] { "Description", "Name" },
                values: new object[] { "Female", "Female" });
        }
    }
}
