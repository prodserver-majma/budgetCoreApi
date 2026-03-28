using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class addbankmasterandstudentlogstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "bankmaster",
            //     columns: table => new
            //     {
            //         Id = table.Column<long>(type: "bigint", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         BankId = table.Column<int>(type: "int", nullable: false),
            //         BankName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //             .Annotation("MySql:CharSet", "utf8")
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PRIMARY", x => x.Id);
            //     })
            //     .Annotation("MySql:CharSet", "utf8")
            //     .Annotation("Relational:Collation", "utf8_general_ci");

            // migrationBuilder.CreateTable(
            //     name: "mz_student_log_type",
            //     columns: table => new
            //     {
            //         id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         name = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //             .Annotation("MySql:CharSet", "utf8"),
            //         description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //             .Annotation("MySql:CharSet", "utf8")
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PRIMARY", x => x.id);
            //     })
            //     .Annotation("MySql:CharSet", "utf8")
            //     .Annotation("Relational:Collation", "utf8_general_ci");

            // migrationBuilder.CreateTable(
            //     name: "mz_student_logs",
            //     columns: table => new
            //     {
            //         id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //         typeId = table.Column<int>(type: "int", nullable: true),
            //         studentId = table.Column<int>(type: "int", nullable: true),
            //         description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //             .Annotation("MySql:CharSet", "utf8"),
            //         createdBy = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //             .Annotation("MySql:CharSet", "utf8"),
            //         createdOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
            //         currentObject = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //             .Annotation("MySql:CharSet", "utf8"),
            //         changes = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //             .Annotation("MySql:CharSet", "utf8"),
            //         logId = table.Column<int>(type: "int", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PRIMARY", x => x.id);
            //     })
            //     .Annotation("MySql:CharSet", "utf8")
            //     .Annotation("Relational:Collation", "utf8_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bankmaster");

            migrationBuilder.DropTable(
                name: "mz_student_log_type");

            migrationBuilder.DropTable(
                name: "mz_student_logs");
        }
    }
}
