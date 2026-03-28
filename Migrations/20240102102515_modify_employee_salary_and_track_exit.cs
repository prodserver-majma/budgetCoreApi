using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class modify_employee_salary_and_track_exit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "createdBy",
            //    table: "payroll_salary_packages",
            //    type: "int(11)",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "createdOn",
            //    table: "payroll_salary_packages",
            //    type: "datetime",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "doeGregorian",
            //    table: "khidmat_guzaar",
            //    type: "datetime",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "lastSalary",
            //    table: "employee_salary",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "lastSalaryDate",
            //    table: "employee_salary",
            //    type: "date",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_payroll_salary_packages_createdBy",
            //    table: "payroll_salary_packages",
            //    column: "createdBy");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_payroll_salary_packages_khidmat_guzaar_createdBy",
            //    table: "payroll_salary_packages",
            //    column: "createdBy",
            //    principalTable: "khidmat_guzaar",
            //    principalColumn: "itsId",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddColumn<int>(
            //    name: "withHoldings",
            //    table: "employee_salary",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "workingDays",
            //    table: "employee_dept_salary",
            //    type: "int(11)",
            //    nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "withHoldings",
                table: "employee_salary");

            migrationBuilder.DropColumn(
                name: "workingDays",
                table: "employee_dept_salary");

            migrationBuilder.DropForeignKey(
                name: "FK_payroll_salary_packages_khidmat_guzaar_createdBy",
                table: "payroll_salary_packages");

            migrationBuilder.DropIndex(
                name: "IX_payroll_salary_packages_createdBy",
                table: "payroll_salary_packages");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "payroll_salary_packages");

            migrationBuilder.DropColumn(
                name: "createdOn",
                table: "payroll_salary_packages");

            migrationBuilder.DropColumn(
                name: "doeGregorian",
                table: "khidmat_guzaar");

            migrationBuilder.DropColumn(
                name: "lastSalary",
                table: "employee_salary");

            migrationBuilder.DropColumn(
                name: "lastSalaryDate",
                table: "employee_salary");
        }
    }
}
