using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class modify_salary_allocation_with_holding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "withHoldings",
                table: "salary_allocation_hijri",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "withHoldings",
                table: "salary_allocation_gegorian",
                type: "int(11)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "withHoldings",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "withHoldings",
                table: "salary_allocation_gegorian");
        }
    }
}
