using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_nisabstudentlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "nisaab_student_logs",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        itsId = table.Column<int>(type: "int", nullable: false),
            //        courseName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        courseDuration = table.Column<int>(type: "int", nullable: false),
            //        instituteCountry = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        instituteCity = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        instituteName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        courseStartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
            //        courseEndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
            //        academicYear = table.Column<int>(type: "int", nullable: false)
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
                name: "nisaab_student_logs");
        }
    }
}
