using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Journey.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LastModifiedJourneys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Users_LastModifiedByUserId",
                table: "Journeys");

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Users_LastModifiedByUserId",
                table: "Journeys",
                column: "LastModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Users_LastModifiedByUserId",
                table: "Journeys");

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Users_LastModifiedByUserId",
                table: "Journeys",
                column: "LastModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
