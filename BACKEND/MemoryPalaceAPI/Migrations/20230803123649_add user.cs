using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoryPalaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class adduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TwoDigitElements_twoDigitSystems_TwoDigitSystemId",
                table: "TwoDigitElements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_twoDigitSystems",
                table: "twoDigitSystems");

            migrationBuilder.RenameTable(
                name: "twoDigitSystems",
                newName: "TwoDigitSystems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TwoDigitSystems",
                table: "TwoDigitSystems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TwoDigitElements_TwoDigitSystems_TwoDigitSystemId",
                table: "TwoDigitElements",
                column: "TwoDigitSystemId",
                principalTable: "TwoDigitSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TwoDigitElements_TwoDigitSystems_TwoDigitSystemId",
                table: "TwoDigitElements");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TwoDigitSystems",
                table: "TwoDigitSystems");

            migrationBuilder.RenameTable(
                name: "TwoDigitSystems",
                newName: "twoDigitSystems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_twoDigitSystems",
                table: "twoDigitSystems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TwoDigitElements_twoDigitSystems_TwoDigitSystemId",
                table: "TwoDigitElements",
                column: "TwoDigitSystemId",
                principalTable: "twoDigitSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
