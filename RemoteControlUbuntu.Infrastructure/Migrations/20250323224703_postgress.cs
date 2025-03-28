using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RemoteControlUbuntu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class postgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2642711a-5fa0-40db-a969-fde997e4ae96"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eb9c9e58-0b8f-4242-919d-c40b30349a8f"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("296b02ce-4012-40fa-ae0b-7325a16b182e"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("593ecb13-8624-40a3-9787-6f4a05822a3d"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("a1e41d7e-68f1-41c0-9d2e-e959e33570f0"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("e3279d21-4de2-4365-b187-8f2d5be09144"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("356e05a6-7374-46c5-884a-a845dac32287"), null, "User", "USER" },
                    { new Guid("a9114779-2d02-4df4-a5bd-05ca8c51bc60"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "IconPath", "Name", "TerminalCommand", "UserId" },
                values: new object[,]
                {
                    { new Guid("125300a8-5161-49c9-a1db-668054e92102"), null, "Off", "shutdown", null },
                    { new Guid("5f99d3ad-837c-46d7-b4b8-abbb86867e33"), null, "Telegram", "telegram-desktop", null },
                    { new Guid("82da7a19-f56e-45b8-a5ba-59fd25414595"), null, "List", "ls -l", null },
                    { new Guid("8a9def8c-ed2f-4414-8780-bde014df9e7d"), null, "Firefox", "firefox", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("356e05a6-7374-46c5-884a-a845dac32287"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a9114779-2d02-4df4-a5bd-05ca8c51bc60"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("125300a8-5161-49c9-a1db-668054e92102"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("5f99d3ad-837c-46d7-b4b8-abbb86867e33"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("82da7a19-f56e-45b8-a5ba-59fd25414595"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("8a9def8c-ed2f-4414-8780-bde014df9e7d"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2642711a-5fa0-40db-a969-fde997e4ae96"), null, "User", "USER" },
                    { new Guid("eb9c9e58-0b8f-4242-919d-c40b30349a8f"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "IconPath", "Name", "TerminalCommand", "UserId" },
                values: new object[,]
                {
                    { new Guid("296b02ce-4012-40fa-ae0b-7325a16b182e"), null, "Firefox", "firefox", null },
                    { new Guid("593ecb13-8624-40a3-9787-6f4a05822a3d"), null, "Telegram", "telegram-desktop", null },
                    { new Guid("a1e41d7e-68f1-41c0-9d2e-e959e33570f0"), null, "List", "ls -l", null },
                    { new Guid("e3279d21-4de2-4365-b187-8f2d5be09144"), null, "Off", "shutdown", null }
                });
        }
    }
}
