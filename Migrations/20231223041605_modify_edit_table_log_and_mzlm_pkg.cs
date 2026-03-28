using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class modify_edit_table_log_and_mzlm_pkg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "edit_table_column_logs_column_idx",
                table: "edit_table_column_logs");

            migrationBuilder.DropIndex(
                name: "edit_table_column_logs_table_idx",
                table: "edit_table_column_logs");

            migrationBuilder.DropIndex(
                name: "edit_table_column_logs_tableid_idx",
                table: "edit_table_column_logs");

            migrationBuilder.DropColumn(
                name: "column_id",
                table: "edit_table_column_logs");

            migrationBuilder.DropColumn(
                name: "table_id",
                table: "edit_table_column_logs");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "column_id",
                table: "edit_table_column_logs",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "table_id",
                table: "edit_table_column_logs",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "edit_table_column_logs_column_idx",
                table: "edit_table_column_logs",
                column: "column_id");

            migrationBuilder.CreateIndex(
                name: "edit_table_column_logs_table_idx",
                table: "edit_table_column_logs",
                column: "table_id");

            migrationBuilder.CreateIndex(
                name: "edit_table_column_logs_tableid_idx",
                table: "edit_table_column_logs",
                column: "table_id");
        }
    }
}
