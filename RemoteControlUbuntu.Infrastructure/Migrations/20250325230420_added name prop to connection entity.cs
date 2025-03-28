using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RemoteControlUbuntu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addednameproptoconnectionentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Connections",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("bef74c03-d181-4f82-b1a5-ab42bb3948f2"), null, "Admin", "ADMIN" },
                    { new Guid("f4fc577f-ae82-4968-87d5-b6aed53c1c15"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "IconPath", "Name", "TerminalCommand", "UserId" },
                values: new object[,]
                {
                    { new Guid("0a2fb9f8-b585-4268-9add-c8284801daae"), null, "Firefox", "firefox", null },
                    { new Guid("63126bb5-2fff-4887-bbd7-4ef25f268d22"), null, "Off", "shutdown", null },
                    { new Guid("a00d46ef-6d0f-448f-bdcc-ab086ced72c2"), null, "Telegram", "telegram-desktop", null },
                    { new Guid("a7664bb4-5676-4b01-bb20-802aaef87ae1"), null, "List", "ls -l", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bef74c03-d181-4f82-b1a5-ab42bb3948f2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f4fc577f-ae82-4968-87d5-b6aed53c1c15"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("0a2fb9f8-b585-4268-9add-c8284801daae"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("63126bb5-2fff-4887-bbd7-4ef25f268d22"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("a00d46ef-6d0f-448f-bdcc-ab086ced72c2"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("a7664bb4-5676-4b01-bb20-802aaef87ae1"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Connections");

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
    }
}
