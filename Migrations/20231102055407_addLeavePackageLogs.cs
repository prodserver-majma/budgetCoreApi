using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class addLeavePackageLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mzlm_leave_package_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    packageId = table.Column<int>(type: "int(11)", nullable: false),
                    remark = table.Column<string>(type: "text", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    stageId = table.Column<int>(type: "int(11)", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    createdBy = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mzlm_leave_package_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_mzlm_leave_package_logs_khidmat_guzaar_createdBy",
                        column: x => x.createdBy,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mzlm_leave_package_logs_mzlm_leave_package_packageId",
                        column: x => x.packageId,
                        principalTable: "mzlm_leave_package",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mzlm_leave_package_logs_mzlm_leave_stage_stageId",
                        column: x => x.stageId,
                        principalTable: "mzlm_leave_stage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_mzlm_leave_package_log_idx",
                table: "mzlm_leave_package_logs",
                column: "packageId");

            migrationBuilder.CreateIndex(
                name: "fk_mzlm_leave_updateby_idx1",
                table: "mzlm_leave_package_logs",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "fk_mzlm_stage_id_idx1",
                table: "mzlm_leave_package_logs",
                column: "stageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mzlm_leave_package_logs");
        }
    }
}
