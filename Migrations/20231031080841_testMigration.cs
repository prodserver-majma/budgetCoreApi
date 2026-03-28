using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class testMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "packageId",
                table: "mzlm_leave_application",
                type: "int(11)",
                nullable: true,
                defaultValueSql: "'0'",
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldDefaultValueSql: "'0'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "packageId",
                table: "mzlm_leave_application",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'0'",
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true,
                oldDefaultValueSql: "'0'");
        }
    }
}
