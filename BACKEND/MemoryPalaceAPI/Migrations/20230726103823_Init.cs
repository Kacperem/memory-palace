using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoryPalaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "twoDigitSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_twoDigitSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TwoDigitElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwoDigitSystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwoDigitElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TwoDigitElements_twoDigitSystems_TwoDigitSystemId",
                        column: x => x.TwoDigitSystemId,
                        principalTable: "twoDigitSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TwoDigitElements_TwoDigitSystemId",
                table: "TwoDigitElements",
                column: "TwoDigitSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TwoDigitElements");

            migrationBuilder.DropTable(
                name: "twoDigitSystems");
        }
    }
}
