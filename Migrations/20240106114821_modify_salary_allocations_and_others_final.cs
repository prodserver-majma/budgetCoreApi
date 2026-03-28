using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class modify_salary_allocations_and_others_final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "otherDeductions",
                table: "employee_salary");

            migrationBuilder.DropColumn(
                name: "penalties",
                table: "employee_salary");

            migrationBuilder.RenameColumn(
                name: "penalties",
                table: "salary_allocation_hijri",
                newName: "timeDelta");

            migrationBuilder.RenameColumn(
                name: "otherDeductions",
                table: "salary_allocation_hijri",
                newName: "shortfall");

            migrationBuilder.RenameColumn(
                name: "penalties",
                table: "salary_allocation_gegorian",
                newName: "timeDelta");

            migrationBuilder.RenameColumn(
                name: "otherDeductions",
                table: "salary_allocation_gegorian",
                newName: "shortfall");

            migrationBuilder.AddColumn<int>(
                name: "dayDelta",
                table: "salary_allocation_hijri",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "overtime",
                table: "salary_allocation_hijri",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dayDelta",
                table: "salary_allocation_gegorian",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "overtime",
                table: "salary_allocation_gegorian",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qismRemarks",
                table: "salary_allocation_gegorian",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.AddColumn<string>(
                name: "systemRemarks",
                table: "salary_allocation_gegorian",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dayDelta",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "overtime",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "dayDelta",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "overtime",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "qismRemarks",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "systemRemarks",
                table: "salary_allocation_gegorian");

            migrationBuilder.RenameColumn(
                name: "timeDelta",
                table: "salary_allocation_hijri",
                newName: "penalties");

            migrationBuilder.RenameColumn(
                name: "shortfall",
                table: "salary_allocation_hijri",
                newName: "otherDeductions");

            migrationBuilder.RenameColumn(
                name: "timeDelta",
                table: "salary_allocation_gegorian",
                newName: "penalties");

            migrationBuilder.RenameColumn(
                name: "shortfall",
                table: "salary_allocation_gegorian",
                newName: "otherDeductions");

            migrationBuilder.AddColumn<int>(
                name: "otherDeductions",
                table: "employee_salary",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "penalties",
                table: "employee_salary",
                type: "int(11)",
                nullable: true);
        }
    }
}
