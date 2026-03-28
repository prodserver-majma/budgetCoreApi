using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class relate_kg_qualification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "itsid",
                table: "wafdprofile_qualification_new",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_wafdprofile_qualification_new_itsid",
                table: "wafdprofile_qualification_new",
                column: "itsid");

            migrationBuilder.AddForeignKey(
                name: "fk_qualification_kg",
                table: "wafdprofile_qualification_new",
                column: "itsid",
                principalTable: "khidmat_guzaar",
                principalColumn: "itsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_qualification_kg",
                table: "wafdprofile_qualification_new");

            migrationBuilder.DropIndex(
                name: "IX_wafdprofile_qualification_new_itsid",
                table: "wafdprofile_qualification_new");

            migrationBuilder.AlterColumn<int>(
                name: "itsid",
                table: "wafdprofile_qualification_new",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");
        }
    }
}
