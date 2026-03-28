using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class modify_bank_and_salary_details_modified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "srno_UNIQUE",
            //    table: "employee_dept_salary");

            //migrationBuilder.DropColumn(
            //    name: "srno",
            //    table: "employee_dept_salary");

            //migrationBuilder.AddColumn<int>(
            //    name: "arrears",
            //    table: "salary_allocation_hijri",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "otherDeductions",
            //    table: "salary_allocation_hijri",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "penalties",
            //    table: "salary_allocation_hijri",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "arrears",
            //    table: "salary_allocation_gegorian",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "otherDeductions",
            //    table: "salary_allocation_gegorian",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "penalties",
            //    table: "salary_allocation_gegorian",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "arrears",
            //    table: "employee_salary",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "incomeTax",
            //    table: "employee_salary",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "localTax",
            //    table: "employee_salary",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "otherDeductions",
            //    table: "employee_salary",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "penalties",
            //    table: "employee_salary",
            //    type: "int(11)",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "country",
            //    table: "employee_bank_details",
            //    type: "text",
            //    nullable: true,
            //    collation: "utf8_general_ci")
            //    .Annotation("MySql:CharSet", "utf8");

            //migrationBuilder.AddColumn<string>(
            //    name: "domesticCode",
            //    table: "employee_bank_details",
            //    type: "text",
            //    nullable: true,
            //    collation: "utf8_general_ci")
            //    .Annotation("MySql:CharSet", "utf8");

            //migrationBuilder.AddColumn<string>(
            //    name: "internationalCode",
            //    table: "employee_bank_details",
            //    type: "text",
            //    nullable: true,
            //    collation: "utf8_general_ci")
            //    .Annotation("MySql:CharSet", "utf8");

            //migrationBuilder.AddColumn<string>(
            //    name: "internationalCodeType",
            //    table: "employee_bank_details",
            //    type: "text",
            //    nullable: true,
            //    collation: "utf8_general_ci")
            //    .Annotation("MySql:CharSet", "utf8");

            //migrationBuilder.CreateIndex(
            //    name: "IX_employee_academic_details_itsId",
            //    table: "employee_academic_details",
            //    column: "itsId",
            //    unique: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "panCard",
            //    table: "employee_bank_details",
            //    type: "text",
            //    nullable: true,
            //    collation: "utf8_general_ci")
            //    .Annotation("MySql:CharSet", "utf8");

            //migrationBuilder.AddColumn<string>(
            //    name: "panCardAttachment",
            //    table: "employee_bank_details",
            //    type: "text",
            //    nullable: true,
            //    collation: "utf8_general_ci")
            //    .Annotation("MySql:CharSet", "utf8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_employee_academic_details_itsId",
                table: "employee_academic_details");

            migrationBuilder.DropColumn(
                name: "arrears",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "otherDeductions",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "penalties",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "arrears",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "otherDeductions",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "penalties",
                table: "salary_allocation_gegorian");

            migrationBuilder.DropColumn(
                name: "arrears",
                table: "employee_salary");

            migrationBuilder.DropColumn(
                name: "incomeTax",
                table: "employee_salary");

            migrationBuilder.DropColumn(
                name: "localTax",
                table: "employee_salary");

            migrationBuilder.DropColumn(
                name: "otherDeductions",
                table: "employee_salary");

            migrationBuilder.DropColumn(
                name: "penalties",
                table: "employee_salary");

            migrationBuilder.DropColumn(
                name: "country",
                table: "employee_bank_details");

            migrationBuilder.DropColumn(
                name: "domesticCode",
                table: "employee_bank_details");

            migrationBuilder.DropColumn(
                name: "internationalCode",
                table: "employee_bank_details");

            migrationBuilder.DropColumn(
                name: "internationalCodeType",
                table: "employee_bank_details");

            //migrationBuilder.AddColumn<int>(
            //    name: "srno",
            //    table: "employee_dept_salary",
            //    type: "int(11)",
            //    nullable: false,
            //    defaultValue: 0)
            //    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            //migrationBuilder.CreateIndex(
            //    name: "srno_UNIQUE",
            //    table: "employee_dept_salary",
            //    column: "srno",
            //    unique: true);

            migrationBuilder.DropColumn(
                name: "panCard",
                table: "employee_bank_details");

            migrationBuilder.DropColumn(
                name: "panCardAttachment",
                table: "employee_bank_details");
        }
    }
}
