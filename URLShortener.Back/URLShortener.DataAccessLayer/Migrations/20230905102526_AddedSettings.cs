using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Title = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingsAccounts",
                columns: table => new
                {
                    Setting_Id = table.Column<int>(type: "int", nullable: false),
                    Account_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsAccounts", x => new { x.Account_Id, x.Setting_Id });
                    table.ForeignKey(
                        name: "FK_SettingsAccounts_AspNetUsers_Account_Id",
                        column: x => x.Account_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettingsAccounts_Settings_Setting_Id",
                        column: x => x.Setting_Id,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_Key",
                table: "Settings",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SettingsAccounts_Setting_Id",
                table: "SettingsAccounts",
                column: "Setting_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SettingsAccounts");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
