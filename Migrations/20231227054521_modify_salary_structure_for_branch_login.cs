using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class modify_salary_structure_for_branch_login : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "incomeTax",
            //    table: "salary_allocation_hijri",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "localTax",
            //    table: "salary_allocation_hijri",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "qardanHasanahNonRefundable",
            //    table: "salary_allocation_hijri",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "qardanHasanahRefundable",
            //    table: "salary_allocation_hijri",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "incomeTax",
            //    table: "salary_allocation_gegorian",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "localTax",
            //    table: "salary_allocation_gegorian",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "qardanHasanahNonRefundable",
            //    table: "salary_allocation_gegorian",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "qardanHasanahRefundable",
            //    table: "salary_allocation_gegorian",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "salaryFrom",
            //    table: "salary_allocation_gegorian",
            //    type: "datetime",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "salaryTo",
            //    table: "salary_allocation_gegorian",
            //    type: "datetime",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "fromDate",
            //    table: "payroll_salary_packages",
            //    type: "datetime",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "qismId",
            //    table: "payroll_salary_packages",
            //    type: "int(11)",
            //    nullable: false,
            //    defaultValue: 1);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "toDate",
            //    table: "payroll_salary_packages",
            //    type: "datetime",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "qardanHasanahNonRefundable",
            //    table: "employee_salary",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "qardanHasanahRefundable",
            //    table: "employee_salary",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_payroll_salary_packages_qismId",
            //    table: "payroll_salary_packages",
            //    column: "qismId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_payroll_salary_packages_qism_al_tahfeez_qismId",
            //    table: "payroll_salary_packages",
            //    column: "qismId",
            //    principalTable: "qism_al_tahfeez",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payroll_salary_packages_qism_al_tahfeez_qismId",
                table: "payroll_salary_packages");

            migrationBuilder.DropIndex(
                name: "IX_payroll_salary_packages_qismId",
                table: "payroll_salary_packages");

            migrationBuilder.DropColumn(
                name: "incomeTax",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "localTax",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "qardanHasanahNonRefundable",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "qardanHasanahRefundable",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "incomeTax",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "localTax",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "qardanHasanahNonRefundable",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "qardanHasanahRefundable",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "salaryFrom",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "salaryTo",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "fromDate",
                table: "payroll_salary_packages");

            migrationBuilder.DropColumn(
                name: "qismId",
                table: "payroll_salary_packages");

            migrationBuilder.DropColumn(
                name: "toDate",
                table: "payroll_salary_packages");

            migrationBuilder.DropColumn(
                name: "qardanHasanahNonRefundable",
                table: "employee_salary");

            migrationBuilder.DropColumn(
                name: "qardanHasanahRefundable",
                table: "employee_salary");
        }
    }
}
