using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoryPalaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class authorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "TwoDigitSystems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TwoDigitSystems_CreatedById",
                table: "TwoDigitSystems",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TwoDigitSystems_Users_CreatedById",
                table: "TwoDigitSystems",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TwoDigitSystems_Users_CreatedById",
                table: "TwoDigitSystems");

            migrationBuilder.DropIndex(
                name: "IX_TwoDigitSystems_CreatedById",
                table: "TwoDigitSystems");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TwoDigitSystems");
        }
    }
}
