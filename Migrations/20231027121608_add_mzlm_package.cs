using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_mzlm_package : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "qismTahfeez",
                table: "venue",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ho",
                table: "venue",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "displayName",
                table: "venue",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "venue",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "CashBalance",
                table: "venue",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<string>(
                name: "CampVenue",
                table: "venue",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "CampId",
                table: "venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "venue",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AddColumn<int>(
                name: "packageId",
                table: "mzlm_leave_application",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "khidmat_guzaar",
                type: "varchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValueSql: "''",
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mzlm_leave_package",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    stageId = table.Column<int>(type: "int(11)", nullable: false),
                    purpose = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mzlm_leave_package", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_leave_apply_package_idx",
                table: "mzlm_leave_application",
                column: "packageId");

            migrationBuilder.CreateIndex(
                name: "fk_leave_apply_stage_idx",
                table: "mzlm_leave_application",
                column: "stageId");

            migrationBuilder.AddForeignKey(
                name: "FK_mzlm_leave_application_mzlm_leave_package_packageId",
                table: "mzlm_leave_application",
                column: "packageId",
                principalTable: "mzlm_leave_package",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mzlm_leave_application_mzlm_leave_stage_stageId",
                table: "mzlm_leave_application",
                column: "stageId",
                principalTable: "mzlm_leave_stage",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mzlm_leave_application_mzlm_leave_package_packageId",
                table: "mzlm_leave_application");

            migrationBuilder.DropForeignKey(
                name: "FK_mzlm_leave_application_mzlm_leave_stage_stageId",
                table: "mzlm_leave_application");

            migrationBuilder.DropTable(
                name: "mzlm_leave_package");

            migrationBuilder.DropIndex(
                name: "fk_leave_apply_package_idx",
                table: "mzlm_leave_application");

            migrationBuilder.DropIndex(
                name: "fk_leave_apply_stage_idx",
                table: "mzlm_leave_application");

            migrationBuilder.DropColumn(
                name: "packageId",
                table: "mzlm_leave_application");

            migrationBuilder.DropColumn(
                name: "poackageId",
                table: "mzlm_leave_application");

            migrationBuilder.UpdateData(
                table: "venue",
                keyColumn: "qismTahfeez",
                keyValue: null,
                column: "qismTahfeez",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "qismTahfeez",
                table: "venue",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.UpdateData(
                table: "venue",
                keyColumn: "ho",
                keyValue: null,
                column: "ho",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ho",
                table: "venue",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.UpdateData(
                table: "venue",
                keyColumn: "displayName",
                keyValue: null,
                column: "displayName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "displayName",
                table: "venue",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.UpdateData(
                table: "venue",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "venue",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "CashBalance",
                table: "venue",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "venue",
                keyColumn: "CampVenue",
                keyValue: null,
                column: "CampVenue",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CampVenue",
                table: "venue",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.UpdateData(
                table: "venue",
                keyColumn: "CampId",
                keyValue: null,
                column: "CampId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CampId",
                table: "venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "venue",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "khidmat_guzaar",
                type: "varchar(1)",
                maxLength: 1,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1,
                oldDefaultValueSql: "''")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");
        }
    }
}
