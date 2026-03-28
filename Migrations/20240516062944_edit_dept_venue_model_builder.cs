using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class edit_dept_venue_model_builder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_dept_venue_qism_al_tahfeez",
                table: "dept_venue");

            migrationBuilder.AlterColumn<int>(
                name: "kgIts",
                table: "nisaab_alumni",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AddForeignKey(
                name: "fk_dept_venue_qism_al_tahfeez",
                table: "dept_venue",
                column: "qismId",
                principalTable: "qism_al_tahfeez",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_dept_venue_qism_al_tahfeez",
                table: "dept_venue");

            migrationBuilder.AlterColumn<int>(
                name: "kgIts",
                table: "nisaab_alumni",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_dept_venue_qism_al_tahfeez",
                table: "dept_venue",
                column: "qismId",
                principalTable: "qism_al_tahfeez",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
