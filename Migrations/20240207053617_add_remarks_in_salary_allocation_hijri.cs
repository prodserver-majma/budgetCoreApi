using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_remarks_in_salary_allocation_hijri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "qismRemarks",
            //    table: "salary_allocation_hijri",
            //    type: "text",
            //    nullable: true,
            //    collation: "utf8_general_ci")
            //    .Annotation("MySql:CharSet", "utf8");

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "salaryFrom",
            //    table: "salary_allocation_hijri",
            //    type: "datetime",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "salaryTo",
            //    table: "salary_allocation_hijri",
            //    type: "datetime",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<string>(
            //    name: "systemRemarks",
            //    table: "salary_allocation_hijri",
            //    type: "text",
            //    nullable: true,
            //    collation: "utf8_general_ci")
            //    .Annotation("MySql:CharSet", "utf8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "qismRemarks",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "salaryFrom",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "salaryTo",
                table: "salary_allocation_hijri");

            migrationBuilder.DropColumn(
                name: "systemRemarks",
                table: "salary_allocation_hijri");
        }
    }
}
