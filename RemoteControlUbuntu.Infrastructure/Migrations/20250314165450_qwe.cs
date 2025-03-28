using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RemoteControlUbuntu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class qwe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0547aad5-f29b-4ec2-9efe-e314df1ba780"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("58409bdf-86a2-417a-9d85-08c87feb6233"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("20877430-d9c9-4109-b19b-60287f284ea9"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("79c7f783-36f9-47f3-8e36-d5a07190b175"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("819cce11-635c-4b49-bb4b-89a84f4ce160"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("906c9272-1b21-4b17-a336-d4775104f6e0"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("0547aad5-f29b-4ec2-9efe-e314df1ba780"), null, "User", "USER" },
                    { new Guid("58409bdf-86a2-417a-9d85-08c87feb6233"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "IconPath", "Name", "TerminalCommand", "UserId" },
                values: new object[,]
                {
                    { new Guid("20877430-d9c9-4109-b19b-60287f284ea9"), null, "Telegram", "telegram-desktop", null },
                    { new Guid("79c7f783-36f9-47f3-8e36-d5a07190b175"), null, "Firefox", "firefox", null },
                    { new Guid("819cce11-635c-4b49-bb4b-89a84f4ce160"), null, "List", "ls -l", null },
                    { new Guid("906c9272-1b21-4b17-a336-d4775104f6e0"), null, "Off", "shutdown", null }
                });
        }
    }
}
