using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "content",
                table: "AboutContent",
                newName: "Content");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "ShortURLs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "AboutContent",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ShortURLs");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "AboutContent",
                newName: "content");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "AboutContent",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)");
        }
    }
}
