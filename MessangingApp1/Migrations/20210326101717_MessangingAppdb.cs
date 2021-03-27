using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MessangingApp1.Migrations
{
    public partial class MessangingAppdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "channels",
                columns: table => new
                {
                    ChannelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_channels", x => x.ChannelId);
                });

            migrationBuilder.CreateTable(
                name: "inviteUsers",
                columns: table => new
                {
                    InviteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InviteUserName = table.Column<string>(nullable: true),
                    ChannelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inviteUsers", x => x.InviteId);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(nullable: true),
                    ChannelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "channels");

            migrationBuilder.DropTable(
                name: "inviteUsers");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
