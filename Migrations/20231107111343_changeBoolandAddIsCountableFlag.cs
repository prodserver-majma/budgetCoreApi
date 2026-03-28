using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class changeBoolandAddIsCountableFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isDefault",
                table: "platform_role",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<bool>(
                name: "isDefault",
                table: "platform_module",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<bool>(
                name: "active",
                table: "mzlm_leave_type",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeductable",
                table: "mzlm_leave_stage",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "isRepeated",
                table: "mzlm_leave_category",
                type: "tinyint(1)",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldDefaultValueSql: "'1'");

            migrationBuilder.AlterColumn<bool>(
                name: "isHijri",
                table: "mzlm_leave_category",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<bool>(
                name: "isDeductable",
                table: "mzlm_leave_category",
                type: "tinyint(1)",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldDefaultValueSql: "'1'");

            migrationBuilder.AlterColumn<bool>(
                name: "isCarryForward",
                table: "mzlm_leave_category",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<bool>(
                name: "active",
                table: "mzlm_leave_category",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "hasAttempted",
                table: "ikhtibaar_marksheet",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)");

            migrationBuilder.AlterColumn<bool>(
                name: "hasItemBlock",
                table: "dept_venue_baseitem",
                type: "tinyint(1)",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldDefaultValueSql: "'1'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeductable",
                table: "mzlm_leave_stage");

            migrationBuilder.AlterColumn<sbyte>(
                name: "isDefault",
                table: "platform_role",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<sbyte>(
                name: "isDefault",
                table: "platform_module",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<sbyte>(
                name: "active",
                table: "mzlm_leave_type",
                type: "tinyint(4)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<sbyte>(
                name: "isRepeated",
                table: "mzlm_leave_category",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValueSql: "'1'");

            migrationBuilder.AlterColumn<sbyte>(
                name: "isHijri",
                table: "mzlm_leave_category",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<sbyte>(
                name: "isDeductable",
                table: "mzlm_leave_category",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValueSql: "'1'");

            migrationBuilder.AlterColumn<sbyte>(
                name: "isCarryForward",
                table: "mzlm_leave_category",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<sbyte>(
                name: "active",
                table: "mzlm_leave_category",
                type: "tinyint(4)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<sbyte>(
                name: "hasAttempted",
                table: "ikhtibaar_marksheet",
                type: "tinyint(4)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<sbyte>(
                name: "hasItemBlock",
                table: "dept_venue_baseitem",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'1'",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValueSql: "'1'");
        }
    }
}
