using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class modify_user_dept_venue_add_user_venue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "itsId",
            //    table: "user_deptvenue",
            //    type: "int(11)",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(int),
            //    oldType: "int(11)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "deptVenueId",
            //    table: "user_deptvenue",
            //    type: "int(11)",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(int),
            //    oldType: "int(11)",
            //    oldNullable: true);

            //migrationBuilder.CreateTable(
            //    name: "user_venue",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int(11)", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        itsId = table.Column<int>(type: "int(11)", nullable: false),
            //        venueId = table.Column<int>(type: "int(11)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_user_venue", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_user_venue_khidmat_guzaar_itsId",
            //            column: x => x.itsId,
            //            principalTable: "khidmat_guzaar",
            //            principalColumn: "itsId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_user_venue_venue_venueId",
            //            column: x => x.venueId,
            //            principalTable: "venue",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    })
            //    .Annotation("MySql:CharSet", "utf8")
            //    .Annotation("Relational:Collation", "utf8_general_ci");

            //migrationBuilder.CreateIndex(
            //    name: "IX_user_deptvenue_deptVenueId",
            //    table: "user_deptvenue",
            //    column: "deptVenueId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_user_deptvenue_itsId",
            //    table: "user_deptvenue",
            //    column: "itsId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_user_venue_itsId",
            //    table: "user_venue",
            //    column: "itsId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_user_venue_venueId",
            //    table: "user_venue",
            //    column: "venueId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_user_deptvenue_dept_venue_deptVenueId",
            //    table: "user_deptvenue",
            //    column: "deptVenueId",
            //    principalTable: "dept_venue",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_user_deptvenue_khidmat_guzaar_itsId",
            //    table: "user_deptvenue",
            //    column: "itsId",
            //    principalTable: "khidmat_guzaar",
            //    principalColumn: "itsId",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_deptvenue_dept_venue_deptVenueId",
                table: "user_deptvenue");

            migrationBuilder.DropForeignKey(
                name: "FK_user_deptvenue_khidmat_guzaar_itsId",
                table: "user_deptvenue");

            migrationBuilder.DropTable(
                name: "user_venue");

            migrationBuilder.DropIndex(
                name: "IX_user_deptvenue_deptVenueId",
                table: "user_deptvenue");

            migrationBuilder.DropIndex(
                name: "IX_user_deptvenue_itsId",
                table: "user_deptvenue");

            migrationBuilder.AlterColumn<int>(
                name: "itsId",
                table: "user_deptvenue",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "deptVenueId",
                table: "user_deptvenue",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");
        }
    }
}
