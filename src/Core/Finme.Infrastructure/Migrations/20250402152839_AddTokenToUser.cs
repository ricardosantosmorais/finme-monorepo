using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetToken",
                table: "Users",
                newName: "Token");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Users",
                newName: "ResetToken");
        }
    }
}
