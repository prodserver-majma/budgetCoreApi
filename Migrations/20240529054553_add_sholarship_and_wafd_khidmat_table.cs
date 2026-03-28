using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_sholarship_and_wafd_khidmat_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "qismal_tahfeez_student");

            //migrationBuilder.CreateTable(
            //    name: "enayat_scholarship_billentry",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        billPeriodName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        billPeriodId = table.Column<int>(type: "int", nullable: true),
            //        applicantItsId = table.Column<int>(type: "int", nullable: true),
            //        createdOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
            //        category = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        subCategory1 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        studentName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        courseYear = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        period = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        standard = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        admissionFee = table.Column<int>(type: "int", nullable: true),
            //        tutuionFee = table.Column<int>(type: "int", nullable: true),
            //        diniyatFee = table.Column<int>(type: "int", nullable: true),
            //        exCurricularFee = table.Column<int>(type: "int", nullable: true),
            //        courseBooks = table.Column<int>(type: "int", nullable: true),
            //        stationary = table.Column<int>(type: "int", nullable: true),
            //        uniform = table.Column<int>(type: "int", nullable: true),
            //        conveyence = table.Column<int>(type: "int", nullable: true),
            //        termFee = table.Column<int>(type: "int", nullable: true),
            //        totalAmount = table.Column<int>(type: "int", nullable: true),
            //        activityFee = table.Column<int>(type: "int", nullable: true),
            //        examinationFee = table.Column<int>(type: "int", nullable: true),
            //        diniyatFeeExam = table.Column<int>(type: "int", nullable: true),
            //        billStatus = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        status = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        updatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
            //        updatedBy = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        currencySymbol = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        subCategory2 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        relationTypeId = table.Column<int>(type: "int", nullable: true),
            //        billType = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        institutionName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        qualification = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        billattchment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        marksheetattachment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        amount_billClearance = table.Column<int>(type: "int", nullable: true),
            //        relationItsId = table.Column<int>(type: "int", nullable: true),
            //        billNumber = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        venue = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        billDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
            //        originalBillStatus = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.id);
            //    })
            //    .Annotation("MySql:CharSet", "utf8")
            //    .Annotation("Relational:Collation", "utf8_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "wafdulhuffaz_khidmat_mawaze",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        khidmatMainType = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        khidmatSubType = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
            //        hijriYear = table.Column<int>(type: "int", nullable: true),
            //        mozeName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
            //            .Annotation("MySql:CharSet", "utf8"),
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
                name: "enayat_scholarship_billentry");

            migrationBuilder.DropTable(
                name: "wafdulhuffaz_khidmat_mawaze");

            migrationBuilder.CreateTable(
                name: "qismal_tahfeez_student",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    activeStatus = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    age = table.Column<int>(type: "int(11)", nullable: true),
                    bloodGroup = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    category = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdBy = table.Column<string>(type: "text", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: true),
                    dob = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    emailId = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    feeStatus = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    gender = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    hdob = table.Column<string>(type: "text", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    inActive_Reason = table.Column<string>(type: "text", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    jamaat = table.Column<string>(type: "text", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    mobile1 = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    mobile2 = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    nationality = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    programId = table.Column<int>(type: "int(11)", nullable: true),
                    studentItsId = table.Column<int>(type: "int(11)", nullable: true),
                    studentName = table.Column<string>(type: "text", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    updatedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");
        }
    }
}
