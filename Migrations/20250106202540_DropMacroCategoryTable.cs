using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Void.Chef.Migrations
{
    /// <inheritdoc />
    public partial class DropMacroCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MacroCategories_MacroCategoryId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "MacroCategories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MacroCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MacroCategoryId",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MacroCategoryId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MacroCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MacroCategoryId",
                table: "Categories",
                column: "MacroCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MacroCategories_MacroCategoryId",
                table: "Categories",
                column: "MacroCategoryId",
                principalTable: "MacroCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
