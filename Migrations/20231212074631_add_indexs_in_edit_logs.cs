using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_indexs_in_edit_logs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "edit_table_column_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    table_id = table.Column<int>(type: "int(11)", nullable: false),
                    column_id = table.Column<int>(type: "int(11)", nullable: false),
                    old_value = table.Column<string>(type: "text", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    new_value = table.Column<string>(type: "text", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    edited_by = table.Column<int>(type: "int(11)", nullable: false),
                    table_name = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    column_name = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    table_primary_key_value = table.Column<string>(type: "text", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    edit_date_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_edit_table_column_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_edit_table_column_logs_khidmat_guzaar_edited_by",
                        column: x => x.edited_by,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_edit_table_column_logs_column_idx",
                table: "edit_table_column_logs",
                column: "column_id");

            migrationBuilder.CreateIndex(
                name: "fk_edit_table_column_logs_column_name_idx",
                table: "edit_table_column_logs",
                column: "column_name");

            migrationBuilder.CreateIndex(
                name: "fk_edit_table_column_logs_khidmatguzaar_idx",
                table: "edit_table_column_logs",
                column: "edited_by");

            migrationBuilder.CreateIndex(
                name: "fk_edit_table_column_logs_table_idx",
                table: "edit_table_column_logs",
                column: "table_id");

            migrationBuilder.CreateIndex(
                name: "fk_edit_table_column_logs_table_name_idx",
                table: "edit_table_column_logs",
                column: "table_name");

            migrationBuilder.CreateIndex(
                name: "fk_edit_table_column_logs_tableid_idx",
                table: "edit_table_column_logs",
                column: "table_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "edit_table_column_logs");
        }
    }
}
