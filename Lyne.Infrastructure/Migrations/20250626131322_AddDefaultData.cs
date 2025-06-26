using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lyne.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "State", "Street", "Zip" },
                values: new object[,]
                {
                    { 1, "Львів", "Україна", "Львівська", "вул. Січових Стрільців, 12", "79000" },
                    { 2, "Київ", "Україна", "Київська", "вул. Тараса Шевченка, 115", "01001" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Куртки, футболки, штани та інший одяг для чоловіків", "Чоловічий одяг" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Сукні, спідниці, топи, костюми для жінок", "Жіночий одяг" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Color", "CreatedAt", "Description", "ImageUrl", "IsActive", "Name", "OrderId", "Price", "Size", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Zara", new Guid("11111111-1111-1111-1111-111111111111"), "Білий", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Класична футболка з бавовни, біла", "https://example.com/images/mens-tshirt.jpg", true, "Футболка чоловіча BASIC", null, 599.00m, "L", 100, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Mango", new Guid("22222222-2222-2222-2222-222222222222"), "Синій", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Вечірня сукня з відкритими плечима, синя", "https://example.com/images/womens-dress.jpg", true, "Сукня вечірня ELEGANT", null, 1599.00m, "M", 50, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddressId", "CreatedAt", "DateOfBirth", "Email", "ForName", "Genre", "Name", "PhoneNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "kosacho@gmail.com", "Косач", "Жіноча", "Ольга", "+380501234567", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "alekskochmar18@gmail.com", "Кочмар", "Чоловіча", "Алекс", "+380986199887", new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }
    }
}
