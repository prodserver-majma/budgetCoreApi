using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_indexs_in_edit_logs_fix_issue_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameIndex(
                name: "fk_edit_table_column_logs_tableid_idx",
                table: "edit_table_column_logs",
                newName: "edit_table_column_logs_tableid_idx");

            migrationBuilder.RenameIndex(
                name: "fk_edit_table_column_logs_table_name_idx",
                table: "edit_table_column_logs",
                newName: "edit_table_column_logs_table_name_idx");

            migrationBuilder.RenameIndex(
                name: "fk_edit_table_column_logs_table_idx",
                table: "edit_table_column_logs",
                newName: "edit_table_column_logs_table_idx");

            migrationBuilder.RenameIndex(
                name: "fk_edit_table_column_logs_column_name_idx",
                table: "edit_table_column_logs",
                newName: "edit_table_column_logs_column_name_idx");

            migrationBuilder.RenameIndex(
                name: "fk_edit_table_column_logs_column_idx",
                table: "edit_table_column_logs",
                newName: "edit_table_column_logs_column_idx");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "edit_table_column_logs_tableid_idx",
                table: "edit_table_column_logs",
                newName: "fk_edit_table_column_logs_tableid_idx");

            migrationBuilder.RenameIndex(
                name: "edit_table_column_logs_table_name_idx",
                table: "edit_table_column_logs",
                newName: "fk_edit_table_column_logs_table_name_idx");

            migrationBuilder.RenameIndex(
                name: "edit_table_column_logs_table_idx",
                table: "edit_table_column_logs",
                newName: "fk_edit_table_column_logs_table_idx");

            migrationBuilder.RenameIndex(
                name: "edit_table_column_logs_column_name_idx",
                table: "edit_table_column_logs",
                newName: "fk_edit_table_column_logs_column_name_idx");

            migrationBuilder.RenameIndex(
                name: "edit_table_column_logs_column_idx",
                table: "edit_table_column_logs",
                newName: "fk_edit_table_column_logs_column_idx");

        }
    }
}
