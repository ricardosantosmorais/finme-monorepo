using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "VerificationCodes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "VerificationCodes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "VerificationCodes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "VerificationCodes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvestmentId",
                table: "VerificationCodes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OperationId",
                table: "VerificationCodes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "VerificationCodes",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "InvestmentId",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "VerificationCodes");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "VerificationCodes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
