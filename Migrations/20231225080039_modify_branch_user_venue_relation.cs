using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class modify_branch_user_venue_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "qism_al_tahfeez_user_venue",
            //    columns: table => new
            //    {
            //        branchUserId = table.Column<int>(type: "int(11)", nullable: false),
            //        venueId = table.Column<int>(type: "int(11)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => new { x.branchUserId, x.venueId });
            //        table.ForeignKey(
            //            name: "fk_qism_al_tahfeez_branch_user_venue",
            //            column: x => x.branchUserId,
            //            principalTable: "branch_user",
            //            principalColumn: "itsId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "fk_qism_al_tahfeez_user_venue",
            //            column: x => x.venueId,
            //            principalTable: "venue",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    })
            //    .Annotation("MySql:CharSet", "utf8")
            //    .Annotation("Relational:Collation", "utf8_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "qism_al_tahfeez_user_venue");

        }
    }
}
