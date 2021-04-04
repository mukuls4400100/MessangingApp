using Microsoft.EntityFrameworkCore.Migrations;

namespace MessangingApp1.Migrations
{
    public partial class messappdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inviteUsers_users_UserId",
                table: "inviteUsers");

            migrationBuilder.DropIndex(
                name: "IX_inviteUsers_UserId",
                table: "inviteUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "inviteUsers");

            migrationBuilder.AddColumn<string>(
                name: "InviteUserName",
                table: "inviteUsers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteUserName",
                table: "inviteUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "inviteUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_inviteUsers_UserId",
                table: "inviteUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_inviteUsers_users_UserId",
                table: "inviteUsers",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
