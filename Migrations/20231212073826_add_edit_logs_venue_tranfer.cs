using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_edit_logs_venue_tranfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "venue_transfer_approval",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    requested_by = table.Column<int>(type: "int(11)", nullable: false),
                    employeeIts = table.Column<int>(type: "int(11)", nullable: false),
                    current_venue_id = table.Column<int>(type: "int(11)", nullable: false),
                    reviewed_by = table.Column<int>(type: "int(11)", nullable: true),
                    stage = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    requested_on = table.Column<DateTime>(type: "datetime", nullable: false),
                    approval_comment = table.Column<string>(type: "text", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_venue_transfer_approval", x => x.id);
                    table.ForeignKey(
                        name: "FK_venue_transfer_approval_khidmat_guzaar_employeeIts",
                        column: x => x.employeeIts,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_venue_transfer_approval_khidmat_guzaar_requested_by",
                        column: x => x.requested_by,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_venue_transfer_approval_khidmat_guzaar_reviewed_by",
                        column: x => x.reviewed_by,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId");
                    table.ForeignKey(
                        name: "FK_venue_transfer_approval_venue_current_venue_id",
                        column: x => x.current_venue_id,
                        principalTable: "venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_venue_transfer_approval_current_venue_id",
                table: "venue_transfer_approval",
                column: "current_venue_id");

            migrationBuilder.CreateIndex(
                name: "IX_venue_transfer_approval_employeeIts",
                table: "venue_transfer_approval",
                column: "employeeIts");

            migrationBuilder.CreateIndex(
                name: "IX_venue_transfer_approval_requested_by",
                table: "venue_transfer_approval",
                column: "requested_by");

            migrationBuilder.CreateIndex(
                name: "IX_venue_transfer_approval_reviewed_by",
                table: "venue_transfer_approval",
                column: "reviewed_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "venue_transfer_approval");
        }
    }
}
