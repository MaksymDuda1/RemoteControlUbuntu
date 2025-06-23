using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RemoteControlUbuntu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCommandsWhiteList_AspNetUsers_UserId",
                table: "UserCommandsWhiteList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCommandsWhiteList",
                table: "UserCommandsWhiteList");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c7c1c6f1-4c11-4afb-a94d-7d433430ad96"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcb1158a-4d65-43dc-bd8b-e10f9cddca8b"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("1585987b-90ca-43d8-9c72-6ebca2fb21e5"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("3ab578d4-7da7-4caa-b922-654ef8531b85"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("a0c591ab-edb7-4e1b-bffc-8ce6dd14d509"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("dfef4ea6-d5b0-45a6-879a-5475662dda7e"));

            migrationBuilder.RenameTable(
                name: "UserCommandsWhiteList",
                newName: "UserCommandsWhiteLists");

            migrationBuilder.RenameIndex(
                name: "IX_UserCommandsWhiteList_UserId",
                table: "UserCommandsWhiteLists",
                newName: "IX_UserCommandsWhiteLists_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Os",
                table: "Connections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCommandsWhiteLists",
                table: "UserCommandsWhiteLists",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "commandsBlackLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commands = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commandsBlackLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommandSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "commandsWhiteLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commands = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commandsWhiteLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommandCommandSet",
                columns: table => new
                {
                    CommandSetsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommandsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandCommandSet", x => new { x.CommandSetsId, x.CommandsId });
                    table.ForeignKey(
                        name: "FK_CommandCommandSet_CommandSets_CommandSetsId",
                        column: x => x.CommandSetsId,
                        principalTable: "CommandSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandCommandSet_Commands_CommandsId",
                        column: x => x.CommandsId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("060ecb46-efc4-4692-aef1-158cfe709e35"), null, "Admin", "ADMIN" },
                    { new Guid("2c78bee4-b001-4b83-85e0-a21b15b28865"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "IconPath", "Name", "TerminalCommand", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("121ef484-9f53-4472-945f-ce45ece1c8d2"), null, "Telegram", "telegram-desktop", "OpenApp", null },
                    { new Guid("15680a0c-d791-4f6e-93fa-9f43abcd976b"), null, "Firefox", "firefox", "OpenApp", null },
                    { new Guid("218105b6-7292-428d-85d1-128bd66696b5"), null, "Off", "shutdown", "Off", null },
                    { new Guid("a87c92d0-2ef5-4024-96d4-6fef63591c17"), null, "List", "ls -l", "List", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandCommandSet_CommandsId",
                table: "CommandCommandSet",
                column: "CommandsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommandsWhiteLists_AspNetUsers_UserId",
                table: "UserCommandsWhiteLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCommandsWhiteLists_AspNetUsers_UserId",
                table: "UserCommandsWhiteLists");

            migrationBuilder.DropTable(
                name: "CommandCommandSet");

            migrationBuilder.DropTable(
                name: "commandsBlackLists");

            migrationBuilder.DropTable(
                name: "commandsWhiteLists");

            migrationBuilder.DropTable(
                name: "CommandSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCommandsWhiteLists",
                table: "UserCommandsWhiteLists");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("060ecb46-efc4-4692-aef1-158cfe709e35"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c78bee4-b001-4b83-85e0-a21b15b28865"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("121ef484-9f53-4472-945f-ce45ece1c8d2"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("15680a0c-d791-4f6e-93fa-9f43abcd976b"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("218105b6-7292-428d-85d1-128bd66696b5"));

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: new Guid("a87c92d0-2ef5-4024-96d4-6fef63591c17"));

            migrationBuilder.DropColumn(
                name: "Os",
                table: "Connections");

            migrationBuilder.RenameTable(
                name: "UserCommandsWhiteLists",
                newName: "UserCommandsWhiteList");

            migrationBuilder.RenameIndex(
                name: "IX_UserCommandsWhiteLists_UserId",
                table: "UserCommandsWhiteList",
                newName: "IX_UserCommandsWhiteList_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCommandsWhiteList",
                table: "UserCommandsWhiteList",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("c7c1c6f1-4c11-4afb-a94d-7d433430ad96"), null, "User", "USER" },
                    { new Guid("dcb1158a-4d65-43dc-bd8b-e10f9cddca8b"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "IconPath", "Name", "TerminalCommand", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("1585987b-90ca-43d8-9c72-6ebca2fb21e5"), null, "Firefox", "firefox", "OpenApp", null },
                    { new Guid("3ab578d4-7da7-4caa-b922-654ef8531b85"), null, "Off", "shutdown", "Off", null },
                    { new Guid("a0c591ab-edb7-4e1b-bffc-8ce6dd14d509"), null, "Telegram", "telegram-desktop", "OpenApp", null },
                    { new Guid("dfef4ea6-d5b0-45a6-879a-5475662dda7e"), null, "List", "ls -l", "List", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommandsWhiteList_AspNetUsers_UserId",
                table: "UserCommandsWhiteList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
