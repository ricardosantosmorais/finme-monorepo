using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOperationFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "OperationFile",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "OperationFile",
                newName: "BucketName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "OperationFile",
                newName: "File");

            migrationBuilder.RenameColumn(
                name: "BucketName",
                table: "OperationFile",
                newName: "Date");
        }
    }
}
