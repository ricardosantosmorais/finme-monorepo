using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Finme.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePitch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationPitch");

            migrationBuilder.AddColumn<DateTime>(
                name: "PitchDate",
                table: "Operation",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PitchImage",
                table: "Operation",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PitchText",
                table: "Operation",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PitchTitle",
                table: "Operation",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PitchDate",
                table: "Operation");

            migrationBuilder.DropColumn(
                name: "PitchImage",
                table: "Operation");

            migrationBuilder.DropColumn(
                name: "PitchText",
                table: "Operation");

            migrationBuilder.DropColumn(
                name: "PitchTitle",
                table: "Operation");

            migrationBuilder.CreateTable(
                name: "OperationPitch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OperationId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationPitch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationPitch_Operation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationPitch_OperationId",
                table: "OperationPitch",
                column: "OperationId",
                unique: true);
        }
    }
}
