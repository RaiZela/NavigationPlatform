using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Journey.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByFKToJourney : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Journeys");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Journeys");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Journeys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedByUserId",
                table: "Journeys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Auth0Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_CreatedByUserId",
                table: "Journeys",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_LastModifiedByUserId",
                table: "Journeys",
                column: "LastModifiedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Users_CreatedByUserId",
                table: "Journeys",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Users_LastModifiedByUserId",
                table: "Journeys",
                column: "LastModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Users_CreatedByUserId",
                table: "Journeys");

            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Users_LastModifiedByUserId",
                table: "Journeys");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Journeys_CreatedByUserId",
                table: "Journeys");

            migrationBuilder.DropIndex(
                name: "IX_Journeys_LastModifiedByUserId",
                table: "Journeys");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Journeys");

            migrationBuilder.DropColumn(
                name: "LastModifiedByUserId",
                table: "Journeys");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Journeys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Journeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
