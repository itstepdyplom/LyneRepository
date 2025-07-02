using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lyne.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAddressConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "State", "Street", "Zip" },
                values: new object[,]
                {
                    { 3, "Київ", "Україна", "Київська", "вул. Хрещатик, 1", "01001" },
                    { 4, "Одеса", "Україна", "Одеська", "вул. Дерибасівська, 10", "65000" },
                    { 5, "Default City", "Україна", "Default State", "Default Address", "00000" },
                    { 6, "Дніпро", "Україна", "Дніпропетровська", "вул. Соборна, 25", "49000" },
                    { 7, "Харків", "Україна", "Харківська", "вул. Сумська, 50", "61000" },
                    { 8, "Запоріжжя", "Україна", "Запорізька", "вул. Героїв Майдану, 33", "69000" },
                    { 9, "Полтава", "Україна", "Полтавська", "вул. Центральна, 15", "36000" },
                    { 10, "Чернівці", "Україна", "Чернівецька", "вул. Миру, 8", "58000" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
