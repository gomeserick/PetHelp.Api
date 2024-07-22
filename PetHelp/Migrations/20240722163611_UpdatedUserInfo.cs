using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotificationEnabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationEnabled",
                table: "AspNetUsers");
        }
    }
}
