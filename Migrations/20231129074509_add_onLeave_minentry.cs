using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_onLeave_minentry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isOnLeave",
                table: "azwaaj_minentry",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isOnLeave",
                table: "azwaaj_minentry");
        }
    }
}
