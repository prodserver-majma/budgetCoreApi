using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_indexs_in_venue_transfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "reviewed_by",
                table: "venue_transfer_approval",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_edit_table_column_logs",
                table: "edit_table_column_logs",
                column: "edited_by",
                principalTable: "khidmat_guzaar",
                principalColumn: "itsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_venue_transfer_approval_current_venue",
                table: "venue_transfer_approval",
                column: "current_venue_id",
                principalTable: "venue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_venue_transfer_approval_khidmatguzaar",
                table: "venue_transfer_approval",
                column: "employeeIts",
                principalTable: "khidmat_guzaar",
                principalColumn: "itsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_venue_transfer_approval_khidmatguzaar1",
                table: "venue_transfer_approval",
                column: "requested_by",
                principalTable: "khidmat_guzaar",
                principalColumn: "itsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_venue_transfer_approval_khidmatguzaar2",
                table: "venue_transfer_approval",
                column: "reviewed_by",
                principalTable: "khidmat_guzaar",
                principalColumn: "itsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_edit_table_column_logs",
                table: "edit_table_column_logs");

            migrationBuilder.DropForeignKey(
                name: "fk_venue_transfer_approval_current_venue",
                table: "venue_transfer_approval");

            migrationBuilder.DropForeignKey(
                name: "fk_venue_transfer_approval_khidmatguzaar",
                table: "venue_transfer_approval");

            migrationBuilder.DropForeignKey(
                name: "fk_venue_transfer_approval_khidmatguzaar1",
                table: "venue_transfer_approval");

            migrationBuilder.DropForeignKey(
                name: "fk_venue_transfer_approval_khidmatguzaar2",
                table: "venue_transfer_approval");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "venue_transfer_approval");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "edit_table_column_logs");

            migrationBuilder.RenameIndex(
                name: "fk_venue_transfer_approval_khidmatguzaar2_idx",
                table: "venue_transfer_approval",
                newName: "IX_venue_transfer_approval_reviewed_by");

            migrationBuilder.RenameIndex(
                name: "fk_venue_transfer_approval_khidmatguzaar1_idx",
                table: "venue_transfer_approval",
                newName: "IX_venue_transfer_approval_requested_by");

            migrationBuilder.RenameIndex(
                name: "fk_venue_transfer_approval_khidmatguzaar_idx",
                table: "venue_transfer_approval",
                newName: "IX_venue_transfer_approval_employeeIts");

            migrationBuilder.RenameIndex(
                name: "fk_venue_transfer_approval_current_venue_idx",
                table: "venue_transfer_approval",
                newName: "IX_venue_transfer_approval_current_venue_id");

            migrationBuilder.AlterColumn<int>(
                name: "reviewed_by",
                table: "venue_transfer_approval",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_venue_transfer_approval_id",
                table: "venue_transfer_approval",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_edit_table_column_logs",
                table: "edit_table_column_logs",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_edit_table_column_logs_khidmat_guzaar_edited_by",
                table: "edit_table_column_logs",
                column: "edited_by",
                principalTable: "khidmat_guzaar",
                principalColumn: "itsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_venue_transfer_approval_khidmat_guzaar_reviewed_by",
                table: "venue_transfer_approval",
                column: "reviewed_by",
                principalTable: "khidmat_guzaar",
                principalColumn: "itsId");

            migrationBuilder.AddForeignKey(
                name: "FK_venue_transfer_approval_venue_current_venue_id",
                table: "venue_transfer_approval",
                column: "current_venue_id",
                principalTable: "venue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
