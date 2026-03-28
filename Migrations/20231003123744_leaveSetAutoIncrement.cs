using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class leaveSetAutoIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "khidmat_guzaar",
                type: "varchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "mzlm_leave_stage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mzlm_leave_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    accessTo = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    daysAllotted = table.Column<int>(type: "int(11)", nullable: false),
                    approvalFlow = table.Column<string>(type: "json", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    applicableTo = table.Column<string>(type: "json", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    active = table.Column<sbyte>(type: "tinyint(4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mzlm_leave_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    leaveTypeId = table.Column<int>(type: "int(11)", nullable: false),
                    maxAllowed = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'1'"),
                    consicutiveLimit = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'1'"),
                    isHijri = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    minApplicationDate = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'3'"),
                    isDeductable = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'1'"),
                    isRepeated = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'1'"),
                    isCarryForward = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    notifyTo = table.Column<string>(type: "json", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_leave_type_category",
                        column: x => x.leaveTypeId,
                        principalTable: "mzlm_leave_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mzlm_leave_application",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    typeId = table.Column<int>(type: "int(11)", nullable: false),
                    categoryId = table.Column<int>(type: "int(11)", nullable: false),
                    fromDayId = table.Column<int>(type: "int(11)", nullable: false),
                    fromMonthId = table.Column<int>(type: "int(11)", nullable: false),
                    toDayId = table.Column<int>(type: "int(11)", nullable: false),
                    toMonthId = table.Column<int>(type: "int(11)", nullable: false),
                    morningShift = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    eveningShift = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    shiftCount = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'1'"),
                    hijrAcademicYear = table.Column<int>(type: "int(11)", nullable: false),
                    stageId = table.Column<int>(type: "int(11)", nullable: false),
                    venueId = table.Column<int>(type: "int(11)", nullable: false),
                    appliedBy = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdBy = table.Column<int>(type: "int(11)", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    purpose = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_mzlm_leave_application_khidmat_guzaar_itsId",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mzlm_leave_application_mzlm_leave_category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "mzlm_leave_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mzlm_leave_application_mzlm_leave_type_typeId",
                        column: x => x.typeId,
                        principalTable: "mzlm_leave_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mzlm_leave_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    leaveId = table.Column<int>(type: "int(11)", nullable: false),
                    remark = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    stageId = table.Column<int>(type: "int(11)", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    createdBy = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_mzlm_leave_application_log",
                        column: x => x.leaveId,
                        principalTable: "mzlm_leave_application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mzlm_leave_updateby",
                        column: x => x.createdBy,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId");
                    table.ForeignKey(
                        name: "fk_mzlm_stage_id_leave_log",
                        column: x => x.stageId,
                        principalTable: "mzlm_leave_stage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_leave_apply_category_idx",
                table: "mzlm_leave_application",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "fk_leave_apply_khidmatguzaar_idx",
                table: "mzlm_leave_application",
                column: "itsId");

            migrationBuilder.CreateIndex(
                name: "fk_leave_apply_type_idx",
                table: "mzlm_leave_application",
                column: "typeId");

            migrationBuilder.CreateIndex(
                name: "fk_leave_type_category_idx",
                table: "mzlm_leave_category",
                column: "leaveTypeId");

            migrationBuilder.CreateIndex(
                name: "fk_mzlm_leave_application_log_idx",
                table: "mzlm_leave_logs",
                column: "leaveId");

            migrationBuilder.CreateIndex(
                name: "fk_mzlm_leave_updateby_idx",
                table: "mzlm_leave_logs",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "fk_mzlm_stage_id_idx",
                table: "mzlm_leave_logs",
                column: "stageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mzlm_leave_logs");

            migrationBuilder.DropTable(
                name: "mzlm_leave_application");

            migrationBuilder.DropTable(
                name: "mzlm_leave_stage");

            migrationBuilder.DropTable(
                name: "mzlm_leave_category");

            migrationBuilder.DropTable(
                name: "mzlm_leave_type");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "khidmat_guzaar");
        }
    }
}
