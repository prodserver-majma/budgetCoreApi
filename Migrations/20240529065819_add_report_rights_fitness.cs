using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_report_rights_fitness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "fitness_activity",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        activityName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        venue = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        hours = table.Column<int>(type: "int", nullable: true),
            //        minutes = table.Column<int>(type: "int", nullable: true),
            //        itsId = table.Column<int>(type: "int", nullable: true),
            //        createdOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
            //        attachmentFile = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        routine = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        academicYear = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.id);
            //    })
            //    .Annotation("MySql:CharSet", "utf8")
            //    .Annotation("Relational:Collation", "utf8_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "reports_rights",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        reportId = table.Column<int>(type: "int", nullable: true),
            //        itsId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.id);
            //    })
            //    .Annotation("MySql:CharSet", "utf8")
            //    .Annotation("Relational:Collation", "utf8_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fitness_activity");

            migrationBuilder.DropTable(
                name: "reports_rights");
        }
    }
}
