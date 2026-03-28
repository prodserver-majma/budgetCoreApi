using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "acedemicyear_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fromday_hijri = table.Column<int>(type: "int(11)", nullable: true),
                    frommonth_hijri = table.Column<int>(type: "int(11)", nullable: true),
                    fromyear_hijri = table.Column<int>(type: "int(11)", nullable: true),
                    today_hijri = table.Column<int>(type: "int(11)", nullable: true),
                    tomonth_hijri = table.Column<int>(type: "int(11)", nullable: true),
                    toyear_hijri = table.Column<int>(type: "int(11)", nullable: true),
                    acedemicYear = table.Column<int>(type: "int(11)", nullable: true),
                    acedemicName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "azwaaj_minentry",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsid = table.Column<int>(type: "int(11)", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    min = table.Column<int>(type: "int(11)", nullable: true),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    policyId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "branch_userdept_venue",
                columns: table => new
                {
                    deptVenueId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branch_userdept_venue", x => new { x.deptVenueId, x.userId });
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "branch_userregistrationform_dropdown_set",
                columns: table => new
                {
                    psetId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branch_userregistrationform_dropdown_set", x => new { x.psetId, x.userId });
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "enayat_medical_billentry",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    billPeriod = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    requestType = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    requestFor = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    entryDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    billType = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    billDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    billFrom = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    amount = table.Column<int>(type: "int", nullable: true),
                    illness = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    billPeriodId = table.Column<int>(type: "int", nullable: true),
                    aplicantItsId = table.Column<int>(type: "int", nullable: true),
                    relationType = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    relationTypeId = table.Column<int>(type: "int", nullable: true),
                    billStatus = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    status = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    updatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updatedBy = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    currencySymbol = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    attachment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    amount_billClearance = table.Column<int>(type: "int", nullable: true),
                    billNumber = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    relationItsId = table.Column<int>(type: "int", nullable: true),
                    originalBillStatus = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "current_counter",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    count = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "campfasal_kutub",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fasalclass = table.Column<int>(type: "int", nullable: true),
                    pdfLink = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    deptId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    deptName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    tag = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.deptId);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "dropdown_dataset_header",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "export_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    categoryId = table.Column<int>(type: "int(11)", nullable: true),
                    categoryName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    fieldActualName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    fieldDisplayName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "export_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    fileName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "export_type_displayheader",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    typeId = table.Column<int>(type: "int(11)", nullable: true),
                    actualName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    displayName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "form",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    description = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdByIts = table.Column<int>(type: "int(11)", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    isActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    setting = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "global_constant",
                columns: table => new
                {
                    key = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    value = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.key);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "hijri_calender",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hijri_day = table.Column<int>(type: "int(11)", nullable: true),
                    hijri_month = table.Column<int>(type: "int(11)", nullable: true),
                    hijri_year = table.Column<int>(type: "int(11)", nullable: true),
                    english_day = table.Column<int>(type: "int(11)", nullable: true),
                    english_month = table.Column<int>(type: "int(11)", nullable: true),
                    english_year = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "hijri_months",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hijriMonthName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "holiday_calender",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    holidayDate = table.Column<DateOnly>(type: "date", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    createdBy = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    duration = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "holiday_hijri_miqaat",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    month_id = table.Column<int>(type: "int(11)", nullable: true),
                    date_id = table.Column<int>(type: "int(11)", nullable: true),
                    miqaats_title = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    miqaats_description = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    miqaats_priority = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "ikhtibaar",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "kg_faimalydetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hofItsId = table.Column<int>(type: "int(11)", nullable: true),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    age = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    relation = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    jamaat = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    idara = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    occupation = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    hifzStatus = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    nationality = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    dob = table.Column<DateTime>(type: "datetime", nullable: true),
                    bloodGroup = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "kg_faimalydetails_its",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hofItsId = table.Column<int>(type: "int(11)", nullable: true),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    age = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    relation = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    jamaat = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    idara = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    occupation = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    hifzStatus = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    nationality = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    dob = table.Column<DateTime>(type: "datetime", nullable: true),
                    bloodGroup = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "kg_identitycards",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cardType = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    country = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    nameOnCard = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    cardNumber = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    attachment = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "kg_self_assessment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    strength = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    weakness = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    longTermGoal = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    roleModel = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    changeAboutYourself = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    alternativeCareerPath = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    personalitytype = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    personalityReport = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    aboutYourSelf = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "kg_venue_worktype",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    venueId = table.Column<int>(type: "int(11)", nullable: true),
                    workTypeId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "kg_worktype",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    typeName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "masterdepartment",
                columns: table => new
                {
                    masterDeptId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    masterDeptName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.masterDeptId);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "module",
                columns: table => new
                {
                    moduleId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    moduleName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.moduleId);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "module_rights",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    moduleId = table.Column<int>(type: "int(11)", nullable: true),
                    rightsId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_bank_transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    bankName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    credit = table.Column<int>(type: "int(11)", nullable: true),
                    debit = table.Column<int>(type: "int(11)", nullable: true),
                    paymentMode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentId = table.Column<int>(type: "int(11)", nullable: true),
                    transactionId = table.Column<int>(type: "int(11)", nullable: true),
                    remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_bill_master",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    billNo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    billDate = table.Column<DateOnly>(type: "date", nullable: true),
                    billAmount = table.Column<int>(type: "int(11)", nullable: true),
                    vendorId = table.Column<int>(type: "int(11)", nullable: true),
                    baseItemId = table.Column<int>(type: "int(11)", nullable: true),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: true),
                    financialYear = table.Column<int>(type: "int(11)", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    txn_Id = table.Column<int>(type: "int(11)", nullable: true),
                    paymentMode_User = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentMode_Admin = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentTo_AccNum = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentTo_AccName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentTo_BankName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentTo_ifsc = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentFrom_BankName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    status = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    isWaived = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    packageId = table.Column<int>(type: "int(11)", nullable: true),
                    gstPercentage = table.Column<float>(type: "float", nullable: true),
                    gstAmount = table.Column<int>(type: "int(11)", nullable: true),
                    tdsApplicableAmount = table.Column<int>(type: "int(11)", nullable: true),
                    tdsPercentage = table.Column<float>(type: "float", nullable: true),
                    tdsAmount = table.Column<int>(type: "int(11)", nullable: true),
                    conveyanceAmount = table.Column<int>(type: "int(11)", nullable: true),
                    isReconciled = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    isFundRequested = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_bills_package",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    amount = table.Column<int>(type: "int(11)", nullable: true),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_budget_transfer_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fromDeptVenueId = table.Column<int>(type: "int(11)", nullable: true),
                    fromBaseItemId = table.Column<int>(type: "int(11)", nullable: true),
                    toDeptVenueId = table.Column<int>(type: "int(11)", nullable: true),
                    toBaseItemId = table.Column<int>(type: "int(11)", nullable: true),
                    amount = table.Column<int>(type: "int(11)", nullable: true),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_deptvenue_cash_wallet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: true),
                    debit = table.Column<int>(type: "int(11)", nullable: true),
                    credit = table.Column<int>(type: "int(11)", nullable: true),
                    currency = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentType = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    note = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_estimate_student",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    psetId = table.Column<int>(type: "int(11)", nullable: true),
                    fcId = table.Column<int>(type: "int(11)", nullable: true),
                    feesAmount = table.Column<int>(type: "int(11)", nullable: true),
                    studentCountPerMonth = table.Column<int>(type: "int(11)", nullable: true),
                    financialYear = table.Column<int>(type: "int(11)", nullable: true),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    duration = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'12'"),
                    stage = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, defaultValueSql: "'Initiated'", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    remarks_admin = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_online_payment_users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    ifsc = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    accNum = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    accName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    bankName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_procurement_baseitem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    isCapital = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isIncome = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_procurement_baseitemmz_expense_procurement_item",
                columns: table => new
                {
                    baseItemId = table.Column<int>(type: "int", nullable: false),
                    itemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mz_expense_procurement_baseitemmz_expense_procurement_item", x => new { x.baseItemId, x.itemId });
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_procurement_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    type = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    uom = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_sanctioned_budget",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: true),
                    baseItemId = table.Column<int>(type: "int(11)", nullable: true),
                    user_arazAmount = table.Column<int>(type: "int(11)", nullable: true),
                    admin_arazAmount = table.Column<int>(type: "int(11)", nullable: true),
                    sanctioned_amount = table.Column<int>(type: "int(11)", nullable: true),
                    financialYear = table.Column<int>(type: "int(11)", nullable: true),
                    updatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_vendor_master",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    phoneNo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    mobileNo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    whatsappNo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    address = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    state = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    city = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    ifscCode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    bankName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    accountNo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    accountName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    panCardNo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    type = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    email = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    gstNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_vendor_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vendorId = table.Column<int>(type: "int(11)", nullable: true),
                    paymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    debit = table.Column<int>(type: "int(11)", nullable: true),
                    credit = table.Column<int>(type: "int(11)", nullable: true),
                    paymentMode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    currency = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    status = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    note = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    chequeDate = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    transactionId = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_vendor_transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    billId = table.Column<int>(type: "int(11)", nullable: true),
                    vendorId = table.Column<int>(type: "int(11)", nullable: true),
                    credit = table.Column<int>(type: "int(11)", nullable: true),
                    debit = table.Column<int>(type: "int(11)", nullable: true),
                    currency = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentMode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentId = table.Column<int>(type: "int(11)", nullable: true),
                    transactionId = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_vendor_wallet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    vendorId = table.Column<int>(type: "int(11)", nullable: true),
                    debit = table.Column<int>(type: "int(11)", nullable: true),
                    credit = table.Column<int>(type: "int(11)", nullable: true),
                    currency = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentType = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    note = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_fee_collection_center",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_faculty_loginlogs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    ipAddress = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    logoutTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    deviceDetails = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_loginlogs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    ipAddress = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    logoutTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    deviceDetails = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_kg_wajebaat_araz",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    hijriYear = table.Column<int>(type: "int(11)", nullable: true),
                    niyyatAmount = table.Column<int>(type: "int(11)", nullable: true),
                    takhmeenAmount = table.Column<float>(type: "float", nullable: true),
                    paidAmount = table.Column<int>(type: "int(11)", nullable: true),
                    currency = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    bankName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    draftNo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    draftDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    updatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    userRemarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    officeRemarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    displayCurrency = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    currencyRate = table.Column<float>(type: "float", nullable: true),
                    wajebaatType = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, defaultValueSql: "'Wajebaat Niyat'", collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    stage = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, defaultValueSql: "'Initialized'", collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    verifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                },
                comment: "		")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_on_off_modules",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_pset_elqgrpid_mapping",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    pSetId = table.Column<int>(type: "int(11)", nullable: true),
                    elqId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_receipt_payment_mode_rights",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    paymentModeId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_receipt_payment_modes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_student",
                columns: table => new
                {
                    mz_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsID = table.Column<int>(type: "int(11)", nullable: false),
                    nameEng = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    nameArabic = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    gender = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    bloodGroup = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    studentEmail = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    studentMobile = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    studentWhatsapp = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    dobGregorian = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    dobHijri = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    age = table.Column<int>(type: "int(11)", nullable: true),
                    fatherEmail = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    fatherMobile = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    fatherWhatsapp = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    motherEmail = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    motherMobile = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    motherWhatsapp = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    address = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    jamaat = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    jamiat = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    vatan = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    maqaam = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    nationality = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    activeStatus = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    psetId = table.Column<int>(type: "int(11)", nullable: true),
                    photoPath = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    photoBase64 = table.Column<string>(type: "varchar(10000)", maxLength: 10000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    fcId = table.Column<int>(type: "int(11)", nullable: true),
                    elq_GroupId = table.Column<int>(type: "int(11)", nullable: true),
                    elq_BranchName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    trNo = table.Column<int>(type: "int(11)", nullable: true),
                    classId = table.Column<int>(type: "int(11)", nullable: true),
                    hifzSanadYear = table.Column<int>(type: "int(11)", nullable: true),
                    dq_fasal = table.Column<int>(type: "int(11)", nullable: true),
                    hifzStatus = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    idara = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.mz_id);
                    table.UniqueConstraint("AK_mz_student_itsID", x => x.itsID);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_student_ewallet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    studentId = table.Column<int>(type: "int(11)", nullable: true),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    debit = table.Column<int>(type: "int(11)", nullable: true),
                    credit = table.Column<int>(type: "int(11)", nullable: true),
                    paymentType = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    note = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    currency = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_student_fee_allotment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    studentId = table.Column<int>(type: "int(11)", nullable: true),
                    pSetId = table.Column<int>(type: "int(11)", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    feeAlloted = table.Column<int>(type: "int(11)", nullable: true),
                    fcId = table.Column<int>(type: "int(11)", nullable: true),
                    reason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    monthId = table.Column<int>(type: "int(11)", nullable: true),
                    hijriYear = table.Column<int>(type: "int(11)", nullable: true),
                    currency = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    txn_Id = table.Column<int>(type: "int(11)", nullable: true),
                    waiveStatus = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_student_fee_excluding_list",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    studentMzId = table.Column<int>(type: "int(11)", nullable: true),
                    monthId = table.Column<int>(type: "int(11)", nullable: true),
                    hijriYear = table.Column<int>(type: "int(11)", nullable: true),
                    pSetId = table.Column<int>(type: "int(11)", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_student_fee_transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    studentId = table.Column<int>(type: "int(11)", nullable: true),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    currency = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    debit = table.Column<int>(type: "int(11)", nullable: true),
                    credit = table.Column<int>(type: "int(11)", nullable: true),
                    paymentMode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    psetId = table.Column<int>(type: "int(11)", nullable: true),
                    recieptId = table.Column<int>(type: "int(11)", nullable: true),
                    allotmentId = table.Column<int>(type: "int(11)", nullable: true),
                    transactionId = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    collection_center_no = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_student_feecategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    categoryName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_student_feecategory_pset",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fcId = table.Column<int>(type: "int(11)", nullable: true),
                    psetId = table.Column<int>(type: "int(11)", nullable: true),
                    amount = table.Column<int>(type: "int(11)", nullable: true),
                    currency = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_student_receipt",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    studentId = table.Column<int>(type: "int(11)", nullable: true),
                    recieptNumber = table.Column<int>(type: "int(11)", nullable: true),
                    recieptDate = table.Column<DateOnly>(type: "date", nullable: true),
                    collectionCenter = table.Column<int>(type: "int(11)", nullable: true),
                    paymentMode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    currency = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    amount = table.Column<int>(type: "int(11)", nullable: true),
                    status = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    psetId = table.Column<int>(type: "int(11)", nullable: true),
                    transactionId = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    bankName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    account = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    note = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    chequeDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "nisaab_classes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    std = table.Column<int>(type: "int(11)", nullable: true),
                    div = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    gender = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    className = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    showTimeTable = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "nisaab_classes_monitor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    monitorItsId = table.Column<int>(type: "int(11)", nullable: true),
                    classId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "nisaab_jadwal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    periodId = table.Column<int>(type: "int(11)", nullable: true),
                    classId = table.Column<int>(type: "int(11)", nullable: true),
                    dayId = table.Column<int>(type: "int(11)", nullable: true),
                    subjectId = table.Column<int>(type: "int(11)", nullable: true),
                    teacherId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "nisaab_periods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    periodName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    timing = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "nisaabtalabat_results",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int", nullable: true),
                    resultYear = table.Column<int>(type: "int", nullable: true),
                    classId = table.Column<int>(type: "int", nullable: true),
                    darajah = table.Column<int>(type: "int", nullable: true),
                    rank = table.Column<int>(type: "int", nullable: true),
                    alShafahi = table.Column<int>(type: "int", nullable: true),
                    alMaqalaInsha = table.Column<int>(type: "int", nullable: true),
                    alTaqyim = table.Column<int>(type: "int", nullable: true),
                    alFiqh = table.Column<int>(type: "int", nullable: true),
                    alAdab = table.Column<int>(type: "int", nullable: true),
                    alTadreebHikam = table.Column<int>(type: "int", nullable: true),
                    alUlumKauniyya = table.Column<int>(type: "int", nullable: true),
                    fuyuzHidayat = table.Column<int>(type: "int", nullable: true),
                    alIkhtibaar = table.Column<int>(type: "int", nullable: true),
                    alHifz = table.Column<int>(type: "int", nullable: true),
                    alAkhbar = table.Column<int>(type: "int", nullable: true),
                    alTamrinTaweel = table.Column<int>(type: "int", nullable: true),
                    english = table.Column<int>(type: "int", nullable: true),
                    gTotal = table.Column<int>(type: "int", nullable: true),
                    percentage = table.Column<float>(type: "float", nullable: true),
                    grade = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    TadbeeralManzeli = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "password_reset_requests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int(11)", nullable: true),
                    UniqueKey = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    ExpiryTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "payroll_salary_packages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    amount = table.Column<int>(type: "int(11)", nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    paymentFrom = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "personality_type_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    strength = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    weakness = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "platform_button",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "platform_moduleplatform_role",
                columns: table => new
                {
                    moduleId = table.Column<int>(type: "int", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_platform_moduleplatform_role", x => new { x.moduleId, x.roleId });
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "platform_page",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    pageName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    description = table.Column<string>(type: "varchar(75)", maxLength: 75, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    link = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    icon = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "platform_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    isDefault = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    icon = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    link = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "platform_roleplatform_role",
                columns: table => new
                {
                    mainRole = table.Column<int>(type: "int", nullable: false),
                    subRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_platform_roleplatform_role", x => new { x.mainRole, x.subRole });
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "printableformat_report",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    fileName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    reportName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "printableformat_report_rights",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    reportId = table.Column<int>(type: "int(11)", nullable: true),
                    itsId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "qism_al_tahfeez_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    qismId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "qism_al_tahfeez_role_module",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    roleId = table.Column<int>(type: "int(11)", nullable: false),
                    moduleId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "qism_al_tahfeez_role_right",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    roleId = table.Column<int>(type: "int(11)", nullable: false),
                    rightId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "qism_al_tahfeez_user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    qismId = table.Column<int>(type: "int(11)", nullable: true),
                    userId = table.Column<int>(type: "int(11)", nullable: true),
                    roleId = table.Column<int>(type: "int(11)", nullable: true),
                    isAdmin = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "qism_al_tahfeez_api_logs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    apiString = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    logId = table.Column<long>(type: "bigint(20)", nullable: true),
                    loginName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    ipAddress = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    deviceDetails = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    referrer = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    apiWithParameter = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    httpRequestType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "qism_al_tahfeez_login_logs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    ipAddress = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    email = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    logoutTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    deviceDetails = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "qismal_tahfeez_student",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    studentName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    studentItsId = table.Column<int>(type: "int(11)", nullable: true),
                    mobile1 = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    mobile2 = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    emailId = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    dob = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    hdob = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    age = table.Column<int>(type: "int(11)", nullable: true),
                    gender = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    bloodGroup = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    nationality = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    jamaat = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    activeStatus = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    updatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: true),
                    programId = table.Column<int>(type: "int(11)", nullable: true),
                    feeStatus = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    category = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    inActive_Reason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "registrationform_programs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "registrationform_subprograms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "rights",
                columns: table => new
                {
                    rightsId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    rightsName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.rightsId);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    roleName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.roleId);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "role_module",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    roleId = table.Column<int>(type: "int(11)", nullable: true),
                    moduleId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "role_rights",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    roleId = table.Column<int>(type: "int(11)", nullable: true),
                    rightsId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "salary_querylogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hijriMonth = table.Column<int>(type: "int(11)", nullable: true),
                    hijriYear = table.Column<int>(type: "int(11)", nullable: true),
                    type = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    fromDate = table.Column<DateOnly>(type: "date", nullable: true),
                    toDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "salary_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    Name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "training_subject",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    status = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, defaultValueSql: "'Not Active'", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    outOf = table.Column<int>(type: "int(11)", nullable: false),
                    qustionare = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    accademicYear = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    ItsId = table.Column<int>(type: "int(11)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Accesslevel = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    DID = table.Column<int>(type: "int(11)", nullable: false),
                    EmailId = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Mobile = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    roleId = table.Column<int>(type: "int(11)", nullable: true),
                    loginStatus = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "user_dept-venue_baseitem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    dept_venueId = table.Column<int>(type: "int(11)", nullable: true),
                    baseItemId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "user_deptvenue",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "userdeptassociation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DID = table.Column<int>(type: "int(11)", nullable: false),
                    Idara = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    Department = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    DisplayName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    DepartmentCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "useritemassociation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int(11)", nullable: false),
                    DID = table.Column<int>(type: "int(11)", nullable: false),
                    BaseItemId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafd_fieldofinterest",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    fieldofInterest = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    selfRanking = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafd_languageproficiency",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    language = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    selfRanking = table.Column<int>(type: "int(11)", nullable: true),
                    speaking = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    reading = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    writing = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    listening = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafd_mahad_past_mawaze",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsIs = table.Column<int>(type: "int(11)", nullable: true),
                    fromYear = table.Column<int>(type: "int(11)", nullable: true),
                    toYear = table.Column<int>(type: "int(11)", nullable: true),
                    mauze = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    program = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafd_otheridara_mawaze",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    fromYear = table.Column<int>(type: "int(11)", nullable: true),
                    toYear = table.Column<int>(type: "int(11)", nullable: true),
                    mauze = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    khidmatNature = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafd_physicalfitness",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    sports = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    selfRanking = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_dropdown_authoredcategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_dropdown_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_dropdown_degree",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_dropdown_fieldofinterest",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_dropdown_mode",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_dropdown_titlecategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_dropdown_workshopcategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_english_assessment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    verificationCode = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    examLink = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    userName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    password = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_qualification_new",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsid = table.Column<int>(type: "int(11)", nullable: true),
                    country = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    mediumOfEducation = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    stage = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    degree = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    institutionName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    status = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    pursuingYear = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    year = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    attachment = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_qualification_stage_degree",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    stage = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    degree = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_workshop_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    subCategory = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    cetificateCredentials = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    keypoints = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    courseName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    category = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    mode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    course = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    type = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    year = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    attachment = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    totalDays = table.Column<int>(type: "int(11)", nullable: true),
                    hoursPerDay = table.Column<int>(type: "int(11)", nullable: true),
                    totalHours = table.Column<int>(type: "int(11)", nullable: true),
                    completionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    academicYear = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_workshops_category_subcategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    category = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    subCategory = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "yellowreceipt",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItsId = table.Column<int>(type: "int(11)", nullable: false),
                    Name = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    Amount = table.Column<int>(type: "int(11)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    PaymentMode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    BankName = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    ChequeNo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    PaidAt = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    Account = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    FinancialYear = table.Column<int>(type: "int(11)", nullable: false),
                    Remarks = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    EntryId = table.Column<int>(type: "int(11)", nullable: true),
                    purpose = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    cancelDate = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, defaultValueSql: "'Paid'", collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    paymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    whatsappNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "dropdown_dataset_options",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    headerId = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_options_header_id",
                        column: x => x.headerId,
                        principalTable: "dropdown_dataset_header",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "form_questionnaire",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    formId = table.Column<int>(type: "int(11)", nullable: false),
                    question = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    type = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    isRequired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_questionnaire_form",
                        column: x => x.formId,
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "form_response",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    formId = table.Column<int>(type: "int(11)", nullable: false),
                    response = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    identifier = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_form_response",
                        column: x => x.formId,
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "ikhtibaar_marksheet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ikhtibaarId = table.Column<int>(type: "int(11)", nullable: false),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    marks = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    totalMarks = table.Column<float>(type: "float", nullable: true),
                    remarks = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    type = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    attempts = table.Column<int>(type: "int(11)", nullable: false),
                    hasAttempted = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    mukhtabir = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    category = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_ikhtibaar_marksheet",
                        column: x => x.ikhtibaarId,
                        principalTable: "ikhtibaar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_unicode_ci");

            migrationBuilder.CreateTable(
                name: "ikhtibaar_questionnaire",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ikhtibaarId = table.Column<int>(type: "int(11)", nullable: false),
                    question = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    weightage = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'1'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_questionnaire_ikhtebaar",
                        column: x => x.ikhtibaarId,
                        principalTable: "ikhtibaar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_bill_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    billId = table.Column<int>(type: "int(11)", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    status = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_bill_logs_bill_master",
                        column: x => x.billId,
                        principalTable: "mz_expense_bill_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_student_budget_issue_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    remark = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdBy = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    estimateStudentId = table.Column<int>(type: "int(11)", nullable: false),
                    isConcerning = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    arazState = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_student_budget_issue_araz",
                        column: x => x.estimateStudentId,
                        principalTable: "mz_expense_estimate_student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_bill_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    billId = table.Column<int>(type: "int(11)", nullable: false),
                    itemId = table.Column<int>(type: "int(11)", nullable: false),
                    quantity = table.Column<float>(type: "float", nullable: true),
                    amountPerPc = table.Column<float>(type: "float", nullable: true),
                    remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    gstPercentage = table.Column<float>(type: "float", nullable: true),
                    gstAmount = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_bill_item_bill_item_master",
                        column: x => x.itemId,
                        principalTable: "mz_expense_procurement_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bill_item_bill_master",
                        column: x => x.billId,
                        principalTable: "mz_expense_bill_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_procurement_item_baseitem",
                columns: table => new
                {
                    itemId = table.Column<int>(type: "int(11)", nullable: false),
                    baseItemId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.itemId, x.baseItemId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_baseitem_item",
                        column: x => x.itemId,
                        principalTable: "mz_expense_procurement_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_item_baseitem",
                        column: x => x.baseItemId,
                        principalTable: "mz_expense_procurement_baseitem",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_off_module_exception",
                columns: table => new
                {
                    moduleId = table.Column<int>(type: "int(11)", nullable: false),
                    itsId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.moduleId, x.itsId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_module_on_off_id",
                        column: x => x.moduleId,
                        principalTable: "mz_on_off_modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "platform_module",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    pageId = table.Column<int>(type: "int(11)", nullable: false),
                    buttonId = table.Column<int>(type: "int(11)", nullable: false),
                    isDefault = table.Column<sbyte>(type: "tinyint(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_platform_module_button",
                        column: x => x.buttonId,
                        principalTable: "platform_button",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_platform_module_page",
                        column: x => x.pageId,
                        principalTable: "platform_page",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "platform_menu_map",
                columns: table => new
                {
                    mainRole = table.Column<int>(type: "int(11)", nullable: false),
                    subRole = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.mainRole, x.subRole })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_main_platform_role",
                        column: x => x.mainRole,
                        principalTable: "platform_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sub_platform_role",
                        column: x => x.subRole,
                        principalTable: "platform_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "platform_role_module",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int(11)", nullable: false),
                    moduleId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.roleId, x.moduleId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_paltform_role_module_r",
                        column: x => x.roleId,
                        principalTable: "platform_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_platform_role_module_mid",
                        column: x => x.moduleId,
                        principalTable: "platform_module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "branch_user",
                columns: table => new
                {
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    emailId = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    lastLoggedIn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.itsId);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "qism_al_tahfeez",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    itsId = table.Column<int>(type: "int(11)", nullable: true),
                    emailId = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    password = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    isActive = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_qism_al_tahfeez_branch_user",
                        column: x => x.itsId,
                        principalTable: "branch_user",
                        principalColumn: "itsId");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "platform_user_module",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int(11)", nullable: false),
                    moduleId = table.Column<int>(type: "int(11)", nullable: false),
                    qismId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.userId, x.moduleId, x.qismId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                    table.ForeignKey(
                        name: "fk_paltform_user_module_kg",
                        column: x => x.userId,
                        principalTable: "branch_user",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_platform_user_module_mid",
                        column: x => x.moduleId,
                        principalTable: "platform_module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_platform_user_module_qism",
                        column: x => x.qismId,
                        principalTable: "qism_al_tahfeez",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "platform_user_role",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int(11)", nullable: false),
                    roleId = table.Column<int>(type: "int(11)", nullable: false),
                    qismId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.userId, x.roleId, x.qismId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                    table.ForeignKey(
                        name: "fk_paltform_user_role_kg",
                        column: x => x.userId,
                        principalTable: "branch_user",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_platform_user_role_mid",
                        column: x => x.roleId,
                        principalTable: "platform_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_platform_user_role_qism",
                        column: x => x.qismId,
                        principalTable: "qism_al_tahfeez",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "venue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int").Annotation("SqlServer:Identity", "1, 1").Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CampVenue = table.Column<string>(type: "varchar(1000)", nullable: false)
                        .Annotation("MySql:Charset", "cp1256")
                        .Annotation("MySql:Collation", "cp1256_general_ci"),
                    CampId = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:Charset", "cp1256")
                        .Annotation("MySql:Collation", "cp1256_general_ci"),
                    ActiveStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CashBalance = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:Charset", "cp1256")
                        .Annotation("MySql:Collation", "cp1256_general_ci"),
                    qismTahfeez = table.Column<string>(type: "varchar(500)", nullable: false)
                        .Annotation("MySql:Charset", "cp1256")
                        .Annotation("MySql:Collation", "cp1256_general_ci"),
                    ho = table.Column<string>(type: "varchar(500)", nullable: false)
                        .Annotation("MySql:Charset", "cp1256")
                        .Annotation("MySql:Collation", "cp1256_general_ci"),
                    displayName = table.Column<string>(type: "varchar(500)", nullable: false)
                        .Annotation("MySql:Charset", "cp1256")
                        .Annotation("MySql:Collation", "cp1256_general_ci"),
                    qismId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_venue", x => x.Id);
                    table.ForeignKey(
                        name: "fk_venue_qism",
                        column: x => x.qismId,
                        principalTable: "qism_al_tahfeez",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "dept_venue",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    masterDeptId = table.Column<int>(type: "int(11)", nullable: false),
                    deptId = table.Column<int>(type: "int(11)", nullable: false),
                    venueId = table.Column<int>(type: "int(11)", nullable: false),
                    masterDeptName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    deptName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    venueName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    status = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    tag = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    qismId = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_dept_venue_deptid",
                        column: x => x.deptId,
                        principalTable: "department",
                        principalColumn: "deptId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_dept_venue_masterdid",
                        column: x => x.masterDeptId,
                        principalTable: "masterdepartment",
                        principalColumn: "masterDeptId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_dept_venue_qism_al_tahfeez",
                        column: x => x.qismId,
                        principalTable: "qism_al_tahfeez",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_dept_venue_venue",
                        column: x => x.venueId,
                        principalTable: "venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "khidmat_guzaar",
                columns: table => new
                {
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    photo = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    krNo = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    fullName = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    fullNameArabic = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    c_codeMobile = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    mobileNo = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    c_codeWhatsapp = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    whatsappNo = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    emailAddress = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    officialEmailAddress = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    watan = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    watanArabic = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    watanAdress = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    muqam = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    muqamArabic = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    dojGregorian = table.Column<DateTime>(type: "datetime", nullable: true),
                    dojHijri = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    dobGregorian = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    dobHijri = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    bloodGroup = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    currentAddress = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    jaman = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    maritalStatus = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    mafsuhiyatYear = table.Column<int>(type: "int(11)", nullable: true),
                    activeStatus = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    nationality = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    its_idaras = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    its_preferredIdara = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    mz_idara = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    dawat_title = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    jamaat = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    jamiat = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    age = table.Column<int>(type: "int(11)", nullable: true),
                    panCardNo = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    haddiyatYear = table.Column<int>(type: "int(11)", nullable: true),
                    domicileParent = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    domicileAddressParents = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    personalHouseStatus = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    personalHouseType = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    personalHouseArea = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    personalHouseAddress = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    photoBase64 = table.Column<string>(type: "text", nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    workTypeId = table.Column<int>(type: "int(11)", nullable: true),
                    employeeType = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    RfId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    EduQualification = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    MzId = table.Column<int>(type: "int(11)", nullable: true),
                    OthDegree = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    EduCompletion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    isMumin = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    designation = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    subDesignation = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    salaryCode = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    mauze = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.itsId);
                    table.ForeignKey(
                        name: "fk_venue_khidmatguzaar",
                        column: x => x.mauze,
                        principalTable: "venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                },
                comment: "		")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "dept_venue_baseitem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: false),
                    baseItemId = table.Column<int>(type: "int(11)", nullable: false),
                    tag = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    hasItemBlock = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'1'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_dept_venue_baseitem_dept_venue",
                        column: x => x.deptVenueId,
                        principalTable: "dept_venue",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_deptvenue_procurement_baseitem",
                        column: x => x.baseItemId,
                        principalTable: "mz_expense_procurement_baseitem",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "holiday_allocation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    holidayId = table.Column<int>(type: "int(11)", nullable: false),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: false),
                    employeeType = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_holiday_allocation_calender",
                        column: x => x.holidayId,
                        principalTable: "hijri_calender",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_holiday_allocation_deptvenue",
                        column: x => x.deptVenueId,
                        principalTable: "dept_venue",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_budget_araz",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: false),
                    baseItemId = table.Column<int>(type: "int(11)", nullable: false),
                    itemId = table.Column<int>(type: "int(11)", nullable: false),
                    amountPerUom = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'1'"),
                    quantity = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'1'"),
                    uom = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    justification = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    remarks_admin = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    updatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    updatedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    financialYear = table.Column<int>(type: "int(11)", nullable: false),
                    stage = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, defaultValueSql: "'Initiated'", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    consumedAmount = table.Column<float>(type: "float", nullable: true),
                    consumedQty = table.Column<float>(type: "float", nullable: true),
                    transferedAmount = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_budget_araz_baseItem",
                        column: x => x.baseItemId,
                        principalTable: "mz_expense_procurement_baseitem",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_budget_araz_deptvenue",
                        column: x => x.deptVenueId,
                        principalTable: "dept_venue",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_budget_araz_items",
                        column: x => x.itemId,
                        principalTable: "mz_expense_procurement_item",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "qism_al_tahfeez_user_deptvenue",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int(11)", nullable: false),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.userId, x.deptVenueId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_branch_user_qism_deptvenue",
                        column: x => x.userId,
                        principalTable: "branch_user",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_deptvenue_qism_user",
                        column: x => x.deptVenueId,
                        principalTable: "dept_venue",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "registrationform_dropdown_set",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    programId = table.Column<int>(type: "int(11)", nullable: false),
                    venueId = table.Column<int>(type: "int(11)", nullable: false),
                    subprogramId = table.Column<int>(type: "int(11)", nullable: false),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: false),
                    activeStatus = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    qismId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_pset_dept_venue",
                        column: x => x.deptVenueId,
                        principalTable: "dept_venue",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pset_program",
                        column: x => x.programId,
                        principalTable: "registrationform_programs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pset_qism_al_tahfeez",
                        column: x => x.qismId,
                        principalTable: "qism_al_tahfeez",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pset_sub_program",
                        column: x => x.subprogramId,
                        principalTable: "registrationform_subprograms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pset_venue",
                        column: x => x.venueId,
                        principalTable: "venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "employee_academic_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    trNo = table.Column<int>(type: "int(11)", nullable: true),
                    farigDarajah = table.Column<int>(type: "int(11)", nullable: true),
                    farigYear = table.Column<int>(type: "int(11)", nullable: true),
                    aljameaDegree = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    hifzSanadYear = table.Column<int>(type: "int(11)", nullable: true),
                    wafdTrainingMasoolIts = table.Column<int>(type: "int(11)", nullable: true),
                    wafdTrainingMushrifIts = table.Column<int>(type: "int(11)", nullable: true),
                    maqaraatTeacherIts = table.Column<int>(type: "int(11)", nullable: true),
                    wafdClassId = table.Column<int>(type: "int(11)", nullable: true),
                    category = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    batchId = table.Column<int>(type: "int(11)", nullable: true),
                    hifzStatus = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "itsId",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "employee_bank_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    bankName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    bankAccountNumber = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    bankAccountName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    ifsc = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    bankBranch = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    bankAccountType = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    chequeAttachment = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "employeeIts",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "employee_dept_salary",
                columns: table => new
                {
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: false),
                    salaryTypeId = table.Column<int>(type: "int(11)", nullable: false),
                    srno = table.Column<int>(type: "int(11)", nullable: true),
                    workingMin = table.Column<int>(type: "int(11)", nullable: true),
                    hasSalary = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    salaryAmount = table.Column<float>(type: "float", nullable: true),
                    isHijriSalary = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.itsId, x.deptVenueId, x.salaryTypeId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
                    table.ForeignKey(
                        name: "deptVenueSalaryId",
                        column: x => x.deptVenueId,
                        principalTable: "dept_venue",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "empSalaryItsId",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "salaryTypeId",
                        column: x => x.salaryTypeId,
                        principalTable: "salary_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "employee_e_attendence",
                columns: table => new
                {
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    entryMorning = table.Column<DateTime>(type: "datetime", nullable: true),
                    exitMorning = table.Column<DateTime>(type: "datetime", nullable: true),
                    entryEvening = table.Column<DateTime>(type: "datetime", nullable: true),
                    exitEvening = table.Column<DateTime>(type: "datetime", nullable: true),
                    extraHour = table.Column<int>(type: "int(11)", nullable: true),
                    logJson = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.itsId, x.date })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_employee_attendence",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "employee_family_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    FatherName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    FatherIts = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    MotherName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    MotherIts = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    SpouseName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    SpouseIts = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "empId",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "employee_khidmat_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    mahad_khidmatYear = table.Column<int>(type: "int(11)", nullable: true),
                    khidmatYear = table.Column<int>(type: "int(11)", nullable: true),
                    khidmatMauzeHouseStatus = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    khdimatMauzeHouseType = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    tayeenYear = table.Column<int>(type: "int(11)", nullable: true),
                    tayeenMonth = table.Column<int>(type: "int(11)", nullable: true),
                    khidmatMonth = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "khidmatguzarIts",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "employee_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    updatedby = table.Column<int>(type: "int(11)", nullable: false),
                    updatedon = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "targetId",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "employee_passport_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    passportName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    passportNo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    dateOfIssue = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    dateOfExpiry = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    placeOfIssue = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    passportPlaceOfBirth = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    dobPassport = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    passportCopy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "citizenIts",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "employee_salary",
                columns: table => new
                {
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    grossSalary = table.Column<int>(type: "int(11)", nullable: false),
                    rentAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    marriageAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    mumbaiAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    conveyanceAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    extraAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    professionTax = table.Column<int>(type: "int(11)", nullable: true),
                    tds = table.Column<int>(type: "int(11)", nullable: true),
                    qardanHasanah = table.Column<int>(type: "int(11)", nullable: true),
                    marafiqKhairiyah = table.Column<int>(type: "int(11)", nullable: true),
                    sabeel = table.Column<int>(type: "int(11)", nullable: true),
                    bqhs = table.Column<int>(type: "int(11)", nullable: true),
                    isHijriAllowence = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    lessDeduction = table.Column<int>(type: "int(11)", nullable: true),
                    installmentDeduction_Qardan = table.Column<int>(type: "int(11)", nullable: true),
                    husainiQardanHasanah = table.Column<int>(type: "int(11)", nullable: true),
                    isHusainiQardan = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    mohammediQardanHasanah = table.Column<int>(type: "int(11)", nullable: true),
                    isMahadSalary = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    fmbAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    fmbDeduction = table.Column<int>(type: "int(11)", nullable: true),
                    currency = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.itsId);
                    table.ForeignKey(
                        name: "empSalary",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_budget_smart_goals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: false),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    category = table.Column<string>(type: "varchar(75)", maxLength: 75, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    specific = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    measearable = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    attainable = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    relevant = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    timeStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    timeEnd = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    remarks_admin = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    updatedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    updatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    financialYear = table.Column<int>(type: "int(11)", nullable: false),
                    stage = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, defaultValueSql: "'Initiated'", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_budget_smart_deptvenue",
                        column: x => x.deptVenueId,
                        principalTable: "dept_venue",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_budget_smart_kg",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "nisaab_alumni",
                columns: table => new
                {
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    jamea = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    degree = table.Column<string>(type: "varchar(75)", maxLength: 75, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    farigYear = table.Column<int>(type: "int(11)", nullable: true),
                    farigDarajah = table.Column<int>(type: "int(11)", nullable: true),
                    batchId = table.Column<int>(type: "int(11)", nullable: true),
                    hafizAtFarig = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    kgIts = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.itsId);
                    table.ForeignKey(
                        name: "fk_alumni_mz_kg",
                        column: x => x.kgIts,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_alumni_mz_student",
                        column: x => x.itsId,
                        principalTable: "mz_student",
                        principalColumn: "itsID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "salary_allocation_gegorian",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    salary = table.Column<int>(type: "int(11)", nullable: false),
                    rentAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    marriageAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    convenienceAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    mumbaiAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    fmbAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    lessDeduction = table.Column<int>(type: "int(11)", nullable: true),
                    extraAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    ctc = table.Column<int>(type: "int(11)", nullable: false),
                    professionTax = table.Column<int>(type: "int(11)", nullable: true),
                    tds = table.Column<int>(type: "int(11)", nullable: true),
                    qardanHasanah = table.Column<int>(type: "int(11)", nullable: true),
                    sabeel = table.Column<int>(type: "int(11)", nullable: true),
                    marafiqKhairiyah = table.Column<int>(type: "int(11)", nullable: true),
                    currency = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValueSql: "'INR'", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    fmbDeduction = table.Column<int>(type: "int(11)", nullable: true),
                    bqhs = table.Column<int>(type: "int(11)", nullable: true),
                    mohammedi_qardanHasanah = table.Column<int>(type: "int(11)", nullable: true),
                    husaini_qardanHasanah = table.Column<int>(type: "int(11)", nullable: true),
                    installmentDeduction_Qardan = table.Column<int>(type: "int(11)", nullable: true),
                    netEarnings = table.Column<int>(type: "int(11)", nullable: false),
                    month = table.Column<int>(type: "int(11)", nullable: false),
                    year = table.Column<int>(type: "int(11)", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    createdBy = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    packageId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_salary_allocation_g_exp_package",
                        column: x => x.packageId,
                        principalTable: "payroll_salary_packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_salary_allocation_g_kh",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "salary_allocation_hijri",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    salary = table.Column<int>(type: "int(11)", nullable: false),
                    rentAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    marriageAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    convenienceAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    mumbaiAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    fmbAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    lessDeduction = table.Column<int>(type: "int(11)", nullable: true),
                    extraAllowance = table.Column<int>(type: "int(11)", nullable: true),
                    ctc = table.Column<int>(type: "int(11)", nullable: false),
                    professionTax = table.Column<int>(type: "int(11)", nullable: true),
                    tds = table.Column<int>(type: "int(11)", nullable: true),
                    qardanHasanah = table.Column<int>(type: "int(11)", nullable: true),
                    sabeel = table.Column<int>(type: "int(11)", nullable: true),
                    marafiqKhairiyah = table.Column<int>(type: "int(11)", nullable: true),
                    currency = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValueSql: "'INR'", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    fmbDeduction = table.Column<int>(type: "int(11)", nullable: true),
                    bqhs = table.Column<int>(type: "int(11)", nullable: true),
                    mohammedi_qardanHasanah = table.Column<int>(type: "int(11)", nullable: true),
                    husaini_qardanHasanah = table.Column<int>(type: "int(11)", nullable: true),
                    installmentDeduction_Qardan = table.Column<int>(type: "int(11)", nullable: true),
                    netEarnings = table.Column<int>(type: "int(11)", nullable: false),
                    month = table.Column<int>(type: "int(11)", nullable: false),
                    year = table.Column<int>(type: "int(11)", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    createdBy = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    paymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    packageId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_salary_allocation_exp_package",
                        column: x => x.packageId,
                        principalTable: "payroll_salary_packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_salary_allocation_h_kh",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "training_class",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    className = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    masoolIts = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_class_masool_kg",
                        column: x => x.masoolIts,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_maqaraat_session",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    teacherItsId = table.Column<int>(type: "int(11)", nullable: false),
                    dayId = table.Column<int>(type: "int(11)", nullable: true),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    isEvaluated = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    reason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    juz = table.Column<int>(type: "int(11)", nullable: true),
                    acedemicYear = table.Column<int>(type: "int(11)", nullable: true),
                    pages = table.Column<int>(type: "int(11)", nullable: true),
                    sessionDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_maqaraat_session_kg",
                        column: x => x.teacherItsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_maqaraat_teacher",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    days = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_maqaraat_teacher_kg",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_budget_araz_transfer_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fromArazId = table.Column<int>(type: "int(11)", nullable: false),
                    toArazId = table.Column<int>(type: "int(11)", nullable: false),
                    amount = table.Column<int>(type: "int(11)", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    createdBy = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    remarks = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    trabferModel = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_budget_araz_from",
                        column: x => x.fromArazId,
                        principalTable: "mz_expense_budget_araz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_budget_araz_to",
                        column: x => x.toArazId,
                        principalTable: "mz_expense_budget_araz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_budget_issue_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    remark = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdBy = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    budgetArazId = table.Column<int>(type: "int(11)", nullable: false),
                    isConcerning = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    arazState = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_budget_issue_araz",
                        column: x => x.budgetArazId,
                        principalTable: "mz_expense_budget_araz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "qism_al_tahfeez_user_pset",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int(11)", nullable: false),
                    psetId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.userId, x.psetId })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_branch_user_pset_b",
                        column: x => x.userId,
                        principalTable: "branch_user",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_branch_user_pset_p",
                        column: x => x.psetId,
                        principalTable: "registrationform_dropdown_set",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "mz_expense_budget_smart_issue_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    remark = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdBy = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    smartGoalId = table.Column<int>(type: "int(11)", nullable: false),
                    isConcerning = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_budget_smart_issue_araz",
                        column: x => x.smartGoalId,
                        principalTable: "mz_expense_budget_smart_goals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "salary_generation_gegorgian",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    quantity = table.Column<int>(type: "int(11)", nullable: false),
                    netSalary = table.Column<int>(type: "int(11)", nullable: true),
                    month = table.Column<int>(type: "int(11)", nullable: false),
                    year = table.Column<int>(type: "int(11)", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: false),
                    allocationId = table.Column<int>(type: "int(11)", nullable: false),
                    salaryType = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "empengsalarygeneration",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_salary_gen_g_salary_type",
                        column: x => x.salaryType,
                        principalTable: "salary_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_salary_gen_gegorian_allocation",
                        column: x => x.allocationId,
                        principalTable: "salary_allocation_gegorian",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_salary_gen_gegorian_dept",
                        column: x => x.deptVenueId,
                        principalTable: "dept_venue",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "salary_generation_hijri",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int(11)", nullable: false),
                    quantity = table.Column<int>(type: "int(11)", nullable: false),
                    netSalary = table.Column<int>(type: "int(11)", nullable: true),
                    month = table.Column<int>(type: "int(11)", nullable: false),
                    year = table.Column<int>(type: "int(11)", nullable: false),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256"),
                    deptVenueId = table.Column<int>(type: "int(11)", nullable: false),
                    allocationId = table.Column<int>(type: "int(11)", nullable: false),
                    salaryType = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "empsalarygeneration",
                        column: x => x.itsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_salary_gen_h_salary_type",
                        column: x => x.salaryType,
                        principalTable: "salary_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_salary_gen_hijri_allocation",
                        column: x => x.allocationId,
                        principalTable: "salary_allocation_hijri",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sdv",
                        column: x => x.deptVenueId,
                        principalTable: "dept_venue",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "training_class_student",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    classId = table.Column<int>(type: "int(11)", nullable: false),
                    studentITS = table.Column<int>(type: "int(11)", nullable: false),
                    rank = table.Column<int>(type: "int(11)", nullable: true),
                    prevRank = table.Column<int>(type: "int(11)", nullable: true),
                    mauze = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    academicYear = table.Column<int>(type: "int(11)", nullable: false),
                    marks = table.Column<int>(type: "int(11)", nullable: true),
                    percentage = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_class_student_kg_student",
                        column: x => x.studentITS,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_class_student_training_class",
                        column: x => x.classId,
                        principalTable: "training_class",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "training_class_subject_teacher",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    classId = table.Column<int>(type: "int(11)", nullable: false),
                    subjectId = table.Column<int>(type: "int(11)", nullable: false),
                    teacherITS = table.Column<int>(type: "int(11)", nullable: false),
                    status = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, defaultValueSql: "'Not Active'", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    acedemicYear = table.Column<int>(type: "int(11)", nullable: true),
                    createdOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<int>(type: "int(11)", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_cst_class",
                        column: x => x.classId,
                        principalTable: "training_class",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cst_subject",
                        column: x => x.subjectId,
                        principalTable: "training_subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cst_teacher",
                        column: x => x.teacherITS,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "wafdprofile_maqaraat_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sessionId = table.Column<int>(type: "int(11)", nullable: false),
                    studentItsId = table.Column<int>(type: "int(11)", nullable: false),
                    marks = table.Column<int>(type: "int(11)", nullable: true),
                    isPresent = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    absentReason = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "cp1256_general_ci")
                        .Annotation("MySql:CharSet", "cp1256")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_maqaarat_data_kg",
                        column: x => x.studentItsId,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_maqaraat_data_maqaraat_session",
                        column: x => x.sessionId,
                        principalTable: "wafdprofile_maqaraat_session",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.CreateTable(
                name: "training_student_subject_marksheet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cstId = table.Column<int>(type: "int(11)", nullable: false),
                    studentITS = table.Column<int>(type: "int(11)", nullable: false),
                    answers = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    status = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, defaultValueSql: "'Not Atempted'", collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    marks = table.Column<int>(type: "int(11)", nullable: true),
                    acedemicYear = table.Column<int>(type: "int(11)", nullable: false),
                    gradedBy = table.Column<int>(type: "int(11)", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_student_marksheet_kg",
                        column: x => x.gradedBy,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_student_marksheet_student",
                        column: x => x.studentITS,
                        principalTable: "khidmat_guzaar",
                        principalColumn: "itsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_student_marksheet_subject_cst",
                        column: x => x.cstId,
                        principalTable: "training_class_subject_teacher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");
            migrationBuilder.CreateTable(
                name: "reports_names",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");
            migrationBuilder.CreateTable(
                name: "bmi_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    itsId = table.Column<int>(type: "int", nullable: true),
                    height_in_cemtimeter = table.Column<float>(type: "float", nullable: true),
                    weight_in_kilogram = table.Column<float>(type: "float", nullable: true),
                    createdOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    bmi = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");
            migrationBuilder.CreateTable(
                name: "currency_converter_new",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fromCurrencyName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    toCurrencyName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    value = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "emailId_UNIQUE",
                table: "branch_user",
                column: "emailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_dept_venue_deptid_idx",
                table: "dept_venue",
                column: "deptId");

            migrationBuilder.CreateIndex(
                name: "fk_dept_venue_masterdid_idx",
                table: "dept_venue",
                column: "masterDeptId");

            migrationBuilder.CreateIndex(
                name: "fk_dept_venue_qism_al_tahfeez_idx",
                table: "dept_venue",
                column: "qismId");

            migrationBuilder.CreateIndex(
                name: "fk_dept_venue_venue_idx",
                table: "dept_venue",
                column: "venueId");

            migrationBuilder.CreateIndex(
                name: "fk_dept_venue_baseitem_dept_venue",
                table: "dept_venue_baseitem",
                column: "deptVenueId");

            migrationBuilder.CreateIndex(
                name: "fk_deptvenue_procurement_baseitem_idx",
                table: "dept_venue_baseitem",
                column: "baseItemId");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "dropdown_dataset_header",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_options_header_id_idx",
                table: "dropdown_dataset_options",
                column: "headerId");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE1",
                table: "dropdown_dataset_options",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "itsId_idx",
                table: "employee_academic_details",
                column: "itsId");

            migrationBuilder.CreateIndex(
                name: "employeeIts_idx",
                table: "employee_bank_details",
                column: "itsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "deptVenueSalaryId_idx",
                table: "employee_dept_salary",
                column: "deptVenueId");

            migrationBuilder.CreateIndex(
                name: "salaryTypeId_idx",
                table: "employee_dept_salary",
                column: "salaryTypeId");

            migrationBuilder.CreateIndex(
                name: "srno_UNIQUE",
                table: "employee_dept_salary",
                column: "srno",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "empId_UNIQUE",
                table: "employee_family_details",
                column: "itsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "itsId_UNIQUE",
                table: "employee_family_details",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "khidmatguzarIts_idx",
                table: "employee_khidmat_details",
                column: "itsId");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE2",
                table: "employee_logs",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "targetId_idx",
                table: "employee_logs",
                column: "itsId");

            migrationBuilder.CreateIndex(
                name: "citizenIts_idx",
                table: "employee_passport_details",
                column: "itsId");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE3",
                table: "employee_passport_details",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "itsId_UNIQUE1",
                table: "employee_salary",
                column: "itsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_questionnaire_form_idx",
                table: "form_questionnaire",
                column: "formId");

            migrationBuilder.CreateIndex(
                name: "fk_form_response",
                table: "form_response",
                column: "formId");

            migrationBuilder.CreateIndex(
                name: "fk_holiday_allocation_calender_idx",
                table: "holiday_allocation",
                column: "holidayId");

            migrationBuilder.CreateIndex(
                name: "fk_holiday_allocation_deptvenue_idx",
                table: "holiday_allocation",
                column: "deptVenueId");

            migrationBuilder.CreateIndex(
                name: "Seondary",
                table: "holiday_allocation",
                columns: new[] { "holidayId", "deptVenueId", "employeeType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Secondary",
                table: "ikhtibaar_marksheet",
                columns: new[] { "ikhtibaarId", "itsId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_questionnaire_ikhtebaar_idx",
                table: "ikhtibaar_questionnaire",
                column: "ikhtibaarId");

            migrationBuilder.CreateIndex(
                name: "fk_venue_khidmatguzaar_idx",
                table: "khidmat_guzaar",
                column: "mauze");

            migrationBuilder.CreateIndex(
                name: "fk_bill_item_bill_item_master_idx",
                table: "mz_expense_bill_item",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "fk_bill_item_bill_master_idx",
                table: "mz_expense_bill_item",
                column: "billId");

            migrationBuilder.CreateIndex(
                name: "fk_bill_logs_bill_master_idx",
                table: "mz_expense_bill_logs",
                column: "billId");

            migrationBuilder.CreateIndex(
                name: "fk_budget_araz_baseItem_idx",
                table: "mz_expense_budget_araz",
                column: "baseItemId");

            migrationBuilder.CreateIndex(
                name: "fk_budget_araz_items_idx",
                table: "mz_expense_budget_araz",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "fk_budget_deptvenue_idx",
                table: "mz_expense_budget_araz",
                column: "deptVenueId");

            migrationBuilder.CreateIndex(
                name: "secondary",
                table: "mz_expense_budget_araz",
                columns: new[] { "deptVenueId", "baseItemId", "itemId", "financialYear" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_budget_araz_from_idx",
                table: "mz_expense_budget_araz_transfer_logs",
                column: "fromArazId");

            migrationBuilder.CreateIndex(
                name: "fk_budget_araz_to_idx",
                table: "mz_expense_budget_araz_transfer_logs",
                column: "toArazId");

            migrationBuilder.CreateIndex(
                name: "fk_budget_issue_araz_idx",
                table: "mz_expense_budget_issue_logs",
                column: "budgetArazId");

            migrationBuilder.CreateIndex(
                name: "fk_budget_deptvenue_idx1",
                table: "mz_expense_budget_smart_goals",
                column: "deptVenueId");

            migrationBuilder.CreateIndex(
                name: "fk_budget_smart_kg_idx",
                table: "mz_expense_budget_smart_goals",
                column: "itsId");

            migrationBuilder.CreateIndex(
                name: "fk_budget_smart_issue_araz_idx",
                table: "mz_expense_budget_smart_issue_logs",
                column: "smartGoalId");

            migrationBuilder.CreateIndex(
                name: "fk_item_baseitem_idx",
                table: "mz_expense_procurement_item_baseitem",
                column: "baseItemId");

            migrationBuilder.CreateIndex(
                name: "fk_student_budget_issue_araz_idx",
                table: "mz_expense_student_budget_issue_logs",
                column: "estimateStudentId");

            migrationBuilder.CreateIndex(
                name: "itsID_UNIQUE",
                table: "mz_student",
                column: "itsID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_alumni_mz_kg_idx",
                table: "nisaab_alumni",
                column: "kgIts");

            migrationBuilder.CreateIndex(
                name: "indexed",
                table: "password_reset_requests",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "Secondary1",
                table: "password_reset_requests",
                column: "UniqueKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_sub_platform_role_idx",
                table: "platform_menu_map",
                column: "subRole");

            migrationBuilder.CreateIndex(
                name: "fk_platform_module_button_idx",
                table: "platform_module",
                column: "buttonId");

            migrationBuilder.CreateIndex(
                name: "fk_platform_module_page_idx",
                table: "platform_module",
                column: "pageId");

            migrationBuilder.CreateIndex(
                name: "fk_platform_role_module_mid_idx",
                table: "platform_role_module",
                column: "moduleId");

            migrationBuilder.CreateIndex(
                name: "fk_paltform_user_module_kg_idx",
                table: "platform_user_module",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "fk_platform_user_module_mid_idx",
                table: "platform_user_module",
                column: "moduleId");

            migrationBuilder.CreateIndex(
                name: "fk_platform_user_module_qism_idx",
                table: "platform_user_module",
                column: "qismId");

            migrationBuilder.CreateIndex(
                name: "fk_paltform_user_role_kg_idx",
                table: "platform_user_role",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "fk_platform_user_role_mid_idx",
                table: "platform_user_role",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "fk_platform_user_role_qism_idx",
                table: "platform_user_role",
                column: "qismId");

            migrationBuilder.CreateIndex(
                name: "emailId_UNIQUE1",
                table: "qism_al_tahfeez",
                column: "emailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "itsId_UNIQUE2",
                table: "qism_al_tahfeez",
                column: "itsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_deptvenue_qism_user_idx",
                table: "qism_al_tahfeez_user_deptvenue",
                column: "deptVenueId");

            migrationBuilder.CreateIndex(
                name: "fk_branch_user_pset_p_idx",
                table: "qism_al_tahfeez_user_pset",
                column: "psetId");

            migrationBuilder.CreateIndex(
                name: "fk_pset_dept_venue_idx",
                table: "registrationform_dropdown_set",
                column: "deptVenueId");

            migrationBuilder.CreateIndex(
                name: "fk_pset_program_idx",
                table: "registrationform_dropdown_set",
                column: "programId");

            migrationBuilder.CreateIndex(
                name: "fk_pset_qism_al_tahfeez_idx",
                table: "registrationform_dropdown_set",
                column: "qismId");

            migrationBuilder.CreateIndex(
                name: "fk_pset_sub_program_idx",
                table: "registrationform_dropdown_set",
                column: "subprogramId");

            migrationBuilder.CreateIndex(
                name: "fk_pset_venue_idx",
                table: "registrationform_dropdown_set",
                column: "venueId");

            migrationBuilder.CreateIndex(
                name: "fk_salary_allocation_g_exp_package_idx",
                table: "salary_allocation_gegorian",
                column: "packageId");

            migrationBuilder.CreateIndex(
                name: "secondary1",
                table: "salary_allocation_gegorian",
                columns: new[] { "itsId", "month", "year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_salary_allocation_exp_package_idx",
                table: "salary_allocation_hijri",
                column: "packageId");

            migrationBuilder.CreateIndex(
                name: "fk_sallary_allocation_h_kh_idx",
                table: "salary_allocation_hijri",
                column: "itsId");

            migrationBuilder.CreateIndex(
                name: "empengsalarygeneration_idx",
                table: "salary_generation_gegorgian",
                column: "itsId");

            migrationBuilder.CreateIndex(
                name: "fk_salary_gen_g_salary_type_idx",
                table: "salary_generation_gegorgian",
                column: "salaryType");

            migrationBuilder.CreateIndex(
                name: "fk_salary_gen_gegorian_allocation_idx",
                table: "salary_generation_gegorgian",
                column: "allocationId");

            migrationBuilder.CreateIndex(
                name: "fk_salary_gen_gegorian_dept",
                table: "salary_generation_gegorgian",
                column: "deptVenueId");

            migrationBuilder.CreateIndex(
                name: "Secondary2",
                table: "salary_generation_gegorgian",
                columns: new[] { "itsId", "month", "year", "deptVenueId", "salaryType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "empsalarygeneration_idx",
                table: "salary_generation_hijri",
                column: "itsId");

            migrationBuilder.CreateIndex(
                name: "fk_salary_gen_h_salary_type_idx",
                table: "salary_generation_hijri",
                column: "salaryType");

            migrationBuilder.CreateIndex(
                name: "fk_salary_gen_hijri_allocation_idx",
                table: "salary_generation_hijri",
                column: "allocationId");

            migrationBuilder.CreateIndex(
                name: "sdv_idx",
                table: "salary_generation_hijri",
                column: "deptVenueId");

            migrationBuilder.CreateIndex(
                name: "Secondary3",
                table: "salary_generation_hijri",
                columns: new[] { "itsId", "deptVenueId", "salaryType", "month", "year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_class_masool_kg_idx",
                table: "training_class",
                column: "masoolIts");

            migrationBuilder.CreateIndex(
                name: "fk_class_student_kg_student_idx",
                table: "training_class_student",
                column: "studentITS");

            migrationBuilder.CreateIndex(
                name: "fk_class_student_training_class_idx",
                table: "training_class_student",
                column: "classId");

            migrationBuilder.CreateIndex(
                name: "fk_cst_class_idx",
                table: "training_class_subject_teacher",
                column: "classId");

            migrationBuilder.CreateIndex(
                name: "fk_cst_subject_idx",
                table: "training_class_subject_teacher",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "fk_cst_teacher_idx",
                table: "training_class_subject_teacher",
                column: "teacherITS");

            migrationBuilder.CreateIndex(
                name: "secondary_key",
                table: "training_class_subject_teacher",
                columns: new[] { "classId", "subjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_student_marksheet_kg_idx",
                table: "training_student_subject_marksheet",
                column: "gradedBy");

            migrationBuilder.CreateIndex(
                name: "fk_student_marksheet_student_idx",
                table: "training_student_subject_marksheet",
                column: "studentITS");

            migrationBuilder.CreateIndex(
                name: "fk_student_marksheet_subject_cst_idx",
                table: "training_student_subject_marksheet",
                column: "cstId");

            migrationBuilder.CreateIndex(
                name: "Secondary4",
                table: "training_student_subject_marksheet",
                columns: new[] { "cstId", "studentITS", "acedemicYear" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_user_deptvenue_baseitem_idx",
                table: "user_dept-venue_baseitem",
                column: "baseItemId");

            migrationBuilder.CreateIndex(
                name: "fk_user_deptvenue_dept_idx",
                table: "user_dept-venue_baseitem",
                column: "dept_venueId");

            migrationBuilder.CreateIndex(
                name: "fk_venue_qism_idx",
                table: "venue",
                column: "qismId");

            migrationBuilder.CreateIndex(
                name: "fk_maqaarat_data_kg_idx",
                table: "wafdprofile_maqaraat_data",
                column: "studentItsId");

            migrationBuilder.CreateIndex(
                name: "fk_maqaraat_data_maqaraat_session_idx",
                table: "wafdprofile_maqaraat_data",
                column: "sessionId");

            migrationBuilder.CreateIndex(
                name: "fk_maqaraat_session_kg_idx",
                table: "wafdprofile_maqaraat_session",
                column: "teacherItsId");

            migrationBuilder.CreateIndex(
                name: "secondary2",
                table: "wafdprofile_maqaraat_session",
                columns: new[] { "teacherItsId", "sessionDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_maqaraat_teacher_kg_idx",
                table: "wafdprofile_maqaraat_teacher",
                column: "itsId");

            migrationBuilder.AddForeignKey(
                name: "fk_branch_user_employee",
                table: "branch_user",
                column: "itsId",
                principalTable: "khidmat_guzaar",
                principalColumn: "itsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_branch_user_employee",
                table: "branch_user");

            migrationBuilder.DropTable(
                name: "acedemicyear_data");

            migrationBuilder.DropTable(
                name: "enayat_medical_billentry");

            migrationBuilder.DropTable(
                name: "azwaaj_minentry");

            migrationBuilder.DropTable(
                name: "branch_userdept_venue");

            migrationBuilder.DropTable(
                name: "current_counter");

            migrationBuilder.DropTable(
                name: "campfasal_kutub");

            migrationBuilder.DropTable(
                name: "branch_userregistrationform_dropdown_set");

            migrationBuilder.DropTable(
                name: "dept_venue_baseitem");

            migrationBuilder.DropTable(
                name: "dropdown_dataset_options");

            migrationBuilder.DropTable(
                name: "employee_academic_details");

            migrationBuilder.DropTable(
                name: "employee_bank_details");

            migrationBuilder.DropTable(
                name: "employee_dept_salary");

            migrationBuilder.DropTable(
                name: "employee_e_attendence");

            migrationBuilder.DropTable(
                name: "employee_family_details");

            migrationBuilder.DropTable(
                name: "employee_khidmat_details");

            migrationBuilder.DropTable(
                name: "employee_logs");

            migrationBuilder.DropTable(
                name: "employee_passport_details");

            migrationBuilder.DropTable(
                name: "employee_salary");

            migrationBuilder.DropTable(
                name: "export_category");

            migrationBuilder.DropTable(
                name: "export_type");

            migrationBuilder.DropTable(
                name: "export_type_displayheader");

            migrationBuilder.DropTable(
                name: "form_questionnaire");



            migrationBuilder.DropTable(
                name: "mz_faculty_loginlogs");

            migrationBuilder.DropTable(
                name: "mz_loginlogs");

            migrationBuilder.DropTable(
                name: "form_response");

            migrationBuilder.DropTable(
                name: "global_constant");

            migrationBuilder.DropTable(
                name: "hijri_months");

            migrationBuilder.DropTable(
                name: "holiday_allocation");

            migrationBuilder.DropTable(
                name: "holiday_calender");

            migrationBuilder.DropTable(
                name: "holiday_hijri_miqaat");

            migrationBuilder.DropTable(
                name: "ikhtibaar_marksheet");

            migrationBuilder.DropTable(
                name: "ikhtibaar_questionnaire");

            migrationBuilder.DropTable(
                name: "kg_faimalydetails");

            migrationBuilder.DropTable(
                name: "kg_faimalydetails_its");

            migrationBuilder.DropTable(
                name: "kg_identitycards");

            migrationBuilder.DropTable(
                name: "kg_self_assessment");

            migrationBuilder.DropTable(
                name: "kg_venue_worktype");

            migrationBuilder.DropTable(
                name: "kg_worktype");

            migrationBuilder.DropTable(
                name: "module");

            migrationBuilder.DropTable(
                name: "module_rights");

            migrationBuilder.DropTable(
                name: "mz_expense_bank_transaction");

            migrationBuilder.DropTable(
                name: "mz_expense_bill_item");

            migrationBuilder.DropTable(
                name: "mz_expense_bill_logs");

            migrationBuilder.DropTable(
                name: "mz_expense_bills_package");

            migrationBuilder.DropTable(
                name: "mz_expense_budget_araz_transfer_logs");

            migrationBuilder.DropTable(
                name: "mz_expense_budget_issue_logs");

            migrationBuilder.DropTable(
                name: "mz_expense_budget_smart_issue_logs");

            migrationBuilder.DropTable(
                name: "mz_expense_budget_transfer_logs");

            migrationBuilder.DropTable(
                name: "mz_expense_deptvenue_cash_wallet");

            migrationBuilder.DropTable(
                name: "mz_expense_online_payment_users");

            migrationBuilder.DropTable(
                name: "mz_expense_procurement_baseitemmz_expense_procurement_item");

            migrationBuilder.DropTable(
                name: "mz_expense_procurement_item_baseitem");

            migrationBuilder.DropTable(
                name: "mz_expense_sanctioned_budget");

            migrationBuilder.DropTable(
                name: "mz_expense_student_budget_issue_logs");

            migrationBuilder.DropTable(
                name: "mz_expense_vendor_master");

            migrationBuilder.DropTable(
                name: "mz_expense_vendor_payment");

            migrationBuilder.DropTable(
                name: "mz_expense_vendor_transaction");

            migrationBuilder.DropTable(
                name: "mz_expense_vendor_wallet");

            migrationBuilder.DropTable(
                name: "mz_fee_collection_center");

            migrationBuilder.DropTable(
                name: "mz_kg_wajebaat_araz");

            migrationBuilder.DropTable(
                name: "mz_off_module_exception");

            migrationBuilder.DropTable(
                name: "mz_pset_elqgrpid_mapping");

            migrationBuilder.DropTable(
                name: "mz_receipt_payment_mode_rights");

            migrationBuilder.DropTable(
                name: "mz_receipt_payment_modes");

            migrationBuilder.DropTable(
                name: "mz_student_ewallet");

            migrationBuilder.DropTable(
                name: "mz_student_fee_allotment");

            migrationBuilder.DropTable(
                name: "mz_student_fee_excluding_list");

            migrationBuilder.DropTable(
                name: "mz_student_fee_transaction");

            migrationBuilder.DropTable(
                name: "mz_student_feecategory");

            migrationBuilder.DropTable(
                name: "mz_student_feecategory_pset");

            migrationBuilder.DropTable(
                name: "mz_student_receipt");

            migrationBuilder.DropTable(
                name: "nisaab_alumni");

            migrationBuilder.DropTable(
                name: "nisaab_classes");

            migrationBuilder.DropTable(
                name: "nisaab_classes_monitor");

            migrationBuilder.DropTable(
                name: "nisaab_jadwal");

            migrationBuilder.DropTable(
                name: "nisaab_periods");

            migrationBuilder.DropTable(
                name: "nisaabtalabat_results");

            migrationBuilder.DropTable(
                name: "password_reset_requests");

            migrationBuilder.DropTable(
                name: "personality_type_details");

            migrationBuilder.DropTable(
                name: "platform_menu_map");

            migrationBuilder.DropTable(
                name: "platform_moduleplatform_role");

            migrationBuilder.DropTable(
                name: "platform_role_module");

            migrationBuilder.DropTable(
                name: "platform_roleplatform_role");

            migrationBuilder.DropTable(
                name: "platform_user_module");

            migrationBuilder.DropTable(
                name: "platform_user_role");

            migrationBuilder.DropTable(
                name: "printableformat_report");

            migrationBuilder.DropTable(
                name: "printableformat_report_rights");

            migrationBuilder.DropTable(
                name: "qism_al_tahfeez_role");

            migrationBuilder.DropTable(
                name: "qism_al_tahfeez_role_module");

            migrationBuilder.DropTable(
                name: "qism_al_tahfeez_role_right");

            migrationBuilder.DropTable(
                name: "qism_al_tahfeez_user");

            migrationBuilder.DropTable(
                name: "qism_al_tahfeez_user_deptvenue");

            migrationBuilder.DropTable(
                name: "qism_al_tahfeez_api_logs");

            migrationBuilder.DropTable(
                name: "qism_al_tahfeez_login_logs");

            migrationBuilder.DropTable(
                name: "qismal_tahfeez_student");

            migrationBuilder.DropTable(
                name: "qism_al_tahfeez_user_pset");

            migrationBuilder.DropTable(
                name: "rights");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "role_module");

            migrationBuilder.DropTable(
                name: "role_rights");

            migrationBuilder.DropTable(
                name: "salary_generation_gegorgian");

            migrationBuilder.DropTable(
                name: "salary_generation_hijri");

            migrationBuilder.DropTable(
                name: "salary_querylogs");

            migrationBuilder.DropTable(
                name: "training_class_student");

            migrationBuilder.DropTable(
                name: "training_student_subject_marksheet");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "user_dept-venue_baseitem");

            migrationBuilder.DropTable(
                name: "user_deptvenue");

            migrationBuilder.DropTable(
                name: "userdeptassociation");

            migrationBuilder.DropTable(
                name: "useritemassociation");

            migrationBuilder.DropTable(
                name: "wafd_fieldofinterest");

            migrationBuilder.DropTable(
                name: "wafd_languageproficiency");

            migrationBuilder.DropTable(
                name: "wafd_mahad_past_mawaze");

            migrationBuilder.DropTable(
                name: "wafd_otheridara_mawaze");

            migrationBuilder.DropTable(
                name: "wafd_physicalfitness");

            migrationBuilder.DropTable(
                name: "wafdprofile_dropdown_authoredcategory");

            migrationBuilder.DropTable(
                name: "wafdprofile_dropdown_category");

            migrationBuilder.DropTable(
                name: "wafdprofile_dropdown_degree");

            migrationBuilder.DropTable(
                name: "wafdprofile_dropdown_fieldofinterest");

            migrationBuilder.DropTable(
                name: "wafdprofile_dropdown_mode");

            migrationBuilder.DropTable(
                name: "wafdprofile_dropdown_titlecategory");

            migrationBuilder.DropTable(
                name: "wafdprofile_dropdown_workshopcategory");

            migrationBuilder.DropTable(
                name: "wafdprofile_english_assessment");

            migrationBuilder.DropTable(
                name: "wafdprofile_maqaraat_data");

            migrationBuilder.DropTable(
                name: "wafdprofile_maqaraat_teacher");

            migrationBuilder.DropTable(
                name: "wafdprofile_qualification_new");

            migrationBuilder.DropTable(
                name: "wafdprofile_qualification_stage_degree");

            migrationBuilder.DropTable(
                name: "wafdprofile_workshop_data");

            migrationBuilder.DropTable(
                name: "wafdprofile_workshops_category_subcategory");

            migrationBuilder.DropTable(
                name: "yellowreceipt");

            migrationBuilder.DropTable(
                name: "dropdown_dataset_header");

            migrationBuilder.DropTable(
                name: "form");

            migrationBuilder.DropTable(
                name: "hijri_calender");

            migrationBuilder.DropTable(
                name: "ikhtibaar");

            migrationBuilder.DropTable(
                name: "mz_expense_bill_master");

            migrationBuilder.DropTable(
                name: "mz_expense_budget_araz");

            migrationBuilder.DropTable(
                name: "mz_expense_budget_smart_goals");

            migrationBuilder.DropTable(
                name: "mz_expense_estimate_student");

            migrationBuilder.DropTable(
                name: "mz_on_off_modules");

            migrationBuilder.DropTable(
                name: "mz_student");

            migrationBuilder.DropTable(
                name: "platform_module");

            migrationBuilder.DropTable(
                name: "platform_role");

            migrationBuilder.DropTable(
                name: "registrationform_dropdown_set");

            migrationBuilder.DropTable(
                name: "salary_allocation_gegorian");

            migrationBuilder.DropTable(
                name: "salary_type");

            migrationBuilder.DropTable(
                name: "salary_allocation_hijri");

            migrationBuilder.DropTable(
                name: "training_class_subject_teacher");

            migrationBuilder.DropTable(
                name: "wafdprofile_maqaraat_session");

            migrationBuilder.DropTable(
                name: "mz_expense_procurement_baseitem");

            migrationBuilder.DropTable(
                name: "mz_expense_procurement_item");

            migrationBuilder.DropTable(
                name: "platform_button");

            migrationBuilder.DropTable(
                name: "platform_page");

            migrationBuilder.DropTable(
                name: "dept_venue");

            migrationBuilder.DropTable(
                name: "registrationform_programs");

            migrationBuilder.DropTable(
                name: "registrationform_subprograms");

            migrationBuilder.DropTable(
                name: "payroll_salary_packages");

            migrationBuilder.DropTable(
                name: "training_class");

            migrationBuilder.DropTable(
                name: "training_subject");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "masterdepartment");

            migrationBuilder.DropTable(
                name: "khidmat_guzaar");

            migrationBuilder.DropTable(
                name: "venue");

            migrationBuilder.DropTable(
                name: "qism_al_tahfeez");

            migrationBuilder.DropTable(
                name: "branch_user");

            migrationBuilder.DropTable(
                name: "reports_names");

            migrationBuilder.DropTable(
                name: "bmi_data");
            migrationBuilder.DropTable(
                name: "currency_converter_new");
        }
    }
}
