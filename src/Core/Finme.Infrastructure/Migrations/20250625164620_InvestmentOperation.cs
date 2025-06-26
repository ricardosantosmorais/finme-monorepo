using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InvestmentOperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DividendsReceived",
                table: "Investment",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Investment_OperationId",
                table: "Investment",
                column: "OperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Investment_Operation_OperationId",
                table: "Investment",
                column: "OperationId",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investment_Operation_OperationId",
                table: "Investment");

            migrationBuilder.DropIndex(
                name: "IX_Investment_OperationId",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "DividendsReceived",
                table: "Investment");
        }
    }
}
