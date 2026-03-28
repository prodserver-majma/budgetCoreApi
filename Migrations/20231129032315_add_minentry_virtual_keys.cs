using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_minentry_virtual_keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "fk_minentry_deptVenue",
                table: "azwaaj_minentry",
                column: "deptVenueId",
                principalTable: "dept_venue",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_minentry_salaryType",
                table: "azwaaj_minentry",
                column: "policyId",
                principalTable: "salary_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_minentry_deptVenue",
                table: "azwaaj_minentry");

            migrationBuilder.DropForeignKey(
                name: "fk_minentry_salaryType",
                table: "azwaaj_minentry");
        }
    }
}
