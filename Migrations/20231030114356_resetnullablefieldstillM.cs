using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class resetnullablefieldstillM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "transactionId",
                table: "mz_student_receipt",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "mz_student_receipt",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode",
                table: "mz_student_receipt",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_student_receipt",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_receipt",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student_receipt",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_student_receipt",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "account",
                table: "mz_student_receipt",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_feecategory_pset",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "transactionId",
                table: "mz_student_fee_transaction",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_student_fee_transaction",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode",
                table: "mz_student_fee_transaction",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_fee_transaction",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student_fee_transaction",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentType",
                table: "mz_student_ewallet",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_student_ewallet",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_ewallet",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student_ewallet",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "vatan",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "studentWhatsapp",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "studentMobile",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "studentEmail",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "photoPath",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "photoBase64",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "nameEng",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "nameArabic",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "motherWhatsapp",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "motherMobile",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "motherEmail",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "maqaam",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "jamiat",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "idara",
                table: "mz_student",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "hifzStatus",
                table: "mz_student",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "fatherWhatsapp",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "fatherMobile",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "fatherEmail",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "elq_BranchName",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "dobHijri",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "dobGregorian",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "mz_student",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_receipt_payment_modes",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mz_receipt_payment_modes",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_on_off_modules",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "mz_loginlogs",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "deviceDetails",
                table: "mz_loginlogs",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "wajebaatType",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                defaultValueSql: "'Wajebaat Niyat'",
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldDefaultValueSql: "'Wajebaat Niyat'")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "userRemarks",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "updatedBy",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "stage",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                defaultValueSql: "'Initialized'",
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldDefaultValueSql: "'Initialized'")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "officeRemarks",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "draftNo",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "displayCurrency",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_fee_collection_center",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mz_fee_collection_center",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "mz_faculty_loginlogs",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "deviceDetails",
                table: "mz_faculty_loginlogs",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentType",
                table: "mz_expense_vendor_wallet",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_expense_vendor_wallet",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_expense_vendor_wallet",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_vendor_wallet",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "transactionId",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "chequeDate",
                table: "mz_expense_vendor_payment",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "whatsappNo",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "state",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "phoneNo",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "panCardNo",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "mobileNo",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ifscCode",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "gstNumber",
                table: "mz_expense_vendor_master",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "mz_expense_vendor_master",
                type: "varchar(60)",
                maxLength: 60,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "accountNo",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "accountName",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "mz_expense_student_budget_issue_logs",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_student_budget_issue_logs",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "arazState",
                table: "mz_expense_student_budget_issue_logs",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "uom",
                table: "mz_expense_procurement_item",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "mz_expense_procurement_item",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_procurement_item",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_procurement_baseitem",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_online_payment_users",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ifsc",
                table: "mz_expense_online_payment_users",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_expense_online_payment_users",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "accNum",
                table: "mz_expense_online_payment_users",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "accName",
                table: "mz_expense_online_payment_users",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks_admin",
                table: "mz_expense_estimate_student",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_expense_estimate_student",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_estimate_student",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentType",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "mz_expense_budget_smart_issue_logs",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_budget_smart_issue_logs",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "updatedBy",
                table: "mz_expense_budget_smart_goals",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "specific",
                table: "mz_expense_budget_smart_goals",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks_admin",
                table: "mz_expense_budget_smart_goals",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "relevant",
                table: "mz_expense_budget_smart_goals",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "measearable",
                table: "mz_expense_budget_smart_goals",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "mz_expense_budget_smart_goals",
                type: "varchar(75)",
                maxLength: 75,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(75)",
                oldMaxLength: 75)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "attainable",
                table: "mz_expense_budget_smart_goals",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "trabferModel",
                table: "mz_expense_budget_araz_transfer_logs",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "updatedBy",
                table: "mz_expense_budget_araz",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "uom",
                table: "mz_expense_budget_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks_admin",
                table: "mz_expense_budget_araz",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "justification",
                table: "mz_expense_budget_araz",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_budget_araz",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_bills_package",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mz_expense_bills_package",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "mz_expense_bill_master",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_ifsc",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_BankName",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_AccNum",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_AccName",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode_User",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode_Admin",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentFrom_BankName",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "billNo",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_expense_bill_item",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "typeName",
                table: "kg_worktype",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "kg_worktype",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "weakness",
                table: "kg_self_assessment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "strength",
                table: "kg_self_assessment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "roleModel",
                table: "kg_self_assessment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "personalitytype",
                table: "kg_self_assessment",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "personalityReport",
                table: "kg_self_assessment",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "longTermGoal",
                table: "kg_self_assessment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "changeAboutYourself",
                table: "kg_self_assessment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "alternativeCareerPath",
                table: "kg_self_assessment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "aboutYourSelf",
                table: "kg_self_assessment",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "nameOnCard",
                table: "kg_identitycards",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "kg_identitycards",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cardType",
                table: "kg_identitycards",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cardNumber",
                table: "kg_identitycards",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "attachment",
                table: "kg_identitycards",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "relation",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "occupation",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "idara",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "hifzStatus",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "kg_faimalydetails_its",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "age",
                table: "kg_faimalydetails_its",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "relation",
                table: "kg_faimalydetails",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "occupation",
                table: "kg_faimalydetails",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "kg_faimalydetails",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "kg_faimalydetails",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "kg_faimalydetails",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "idara",
                table: "kg_faimalydetails",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "hifzStatus",
                table: "kg_faimalydetails",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "kg_faimalydetails",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "age",
                table: "kg_faimalydetails",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "ikhtibaar_marksheet",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "ikhtibaar_marksheet",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "mukhtabir",
                table: "ikhtibaar_marksheet",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "marks",
                table: "ikhtibaar_marksheet",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "ikhtibaar_marksheet",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "response",
                table: "form_response",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "identifier",
                table: "form_response",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "form_questionnaire",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "form_questionnaire",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "setting",
                table: "form",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "form",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "displayName",
                table: "export_type_displayheader",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "actualName",
                table: "export_type_displayheader",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "export_type",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "fileName",
                table: "export_type",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "export_type",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "fieldDisplayName",
                table: "export_category",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "fieldActualName",
                table: "export_category",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "categoryName",
                table: "export_category",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "employee_salary",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "placeOfIssue",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "passportPlaceOfBirth",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "passportNo",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "passportName",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "passportCopy",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "dobPassport",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "dateOfIssue",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "dateOfExpiry",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "khidmatMauzeHouseStatus",
                table: "employee_khidmat_details",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "khdimatMauzeHouseType",
                table: "employee_khidmat_details",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "SpouseName",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "SpouseIts",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "MotherName",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "MotherIts",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "FatherName",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "FatherIts",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "logJson",
                table: "employee_e_attendence",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ifsc",
                table: "employee_bank_details",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "chequeAttachment",
                table: "employee_bank_details",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "employee_bank_details",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankBranch",
                table: "employee_bank_details",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankAccountType",
                table: "employee_bank_details",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankAccountNumber",
                table: "employee_bank_details",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankAccountName",
                table: "employee_bank_details",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "tag",
                table: "dept_venue_baseitem",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "venueName",
                table: "dept_venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "tag",
                table: "dept_venue",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "dept_venue",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "masterDeptName",
                table: "dept_venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "deptName",
                table: "dept_venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "tag",
                table: "department",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "azwaaj_minentry",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "acedemicName",
                table: "acedemicyear_data",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "mz_student_receipt",
                keyColumn: "transactionId",
                keyValue: null,
                column: "transactionId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "transactionId",
                table: "mz_student_receipt",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_receipt",
                keyColumn: "status",
                keyValue: null,
                column: "status",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "mz_student_receipt",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_receipt",
                keyColumn: "paymentMode",
                keyValue: null,
                column: "paymentMode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode",
                table: "mz_student_receipt",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_receipt",
                keyColumn: "note",
                keyValue: null,
                column: "note",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_student_receipt",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_receipt",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_receipt",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_receipt",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student_receipt",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_receipt",
                keyColumn: "bankName",
                keyValue: null,
                column: "bankName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_student_receipt",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_receipt",
                keyColumn: "account",
                keyValue: null,
                column: "account",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "account",
                table: "mz_student_receipt",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_feecategory_pset",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_feecategory_pset",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_fee_transaction",
                keyColumn: "transactionId",
                keyValue: null,
                column: "transactionId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "transactionId",
                table: "mz_student_fee_transaction",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_fee_transaction",
                keyColumn: "remarks",
                keyValue: null,
                column: "remarks",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_student_fee_transaction",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_fee_transaction",
                keyColumn: "paymentMode",
                keyValue: null,
                column: "paymentMode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode",
                table: "mz_student_fee_transaction",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_fee_transaction",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_fee_transaction",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_fee_transaction",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student_fee_transaction",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_ewallet",
                keyColumn: "paymentType",
                keyValue: null,
                column: "paymentType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentType",
                table: "mz_student_ewallet",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_ewallet",
                keyColumn: "note",
                keyValue: null,
                column: "note",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_student_ewallet",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_ewallet",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_ewallet",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student_ewallet",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student_ewallet",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "vatan",
                keyValue: null,
                column: "vatan",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "vatan",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "studentWhatsapp",
                keyValue: null,
                column: "studentWhatsapp",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "studentWhatsapp",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "studentMobile",
                keyValue: null,
                column: "studentMobile",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "studentMobile",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "studentEmail",
                keyValue: null,
                column: "studentEmail",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "studentEmail",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "photoPath",
                keyValue: null,
                column: "photoPath",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "photoPath",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "photoBase64",
                keyValue: null,
                column: "photoBase64",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "photoBase64",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "nationality",
                keyValue: null,
                column: "nationality",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "nameEng",
                keyValue: null,
                column: "nameEng",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "nameEng",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "nameArabic",
                keyValue: null,
                column: "nameArabic",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "nameArabic",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "motherWhatsapp",
                keyValue: null,
                column: "motherWhatsapp",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "motherWhatsapp",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "motherMobile",
                keyValue: null,
                column: "motherMobile",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "motherMobile",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "motherEmail",
                keyValue: null,
                column: "motherEmail",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "motherEmail",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "maqaam",
                keyValue: null,
                column: "maqaam",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "maqaam",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "jamiat",
                keyValue: null,
                column: "jamiat",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "jamiat",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "jamaat",
                keyValue: null,
                column: "jamaat",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "idara",
                keyValue: null,
                column: "idara",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "idara",
                table: "mz_student",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "hifzStatus",
                keyValue: null,
                column: "hifzStatus",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "hifzStatus",
                table: "mz_student",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "gender",
                keyValue: null,
                column: "gender",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "fatherWhatsapp",
                keyValue: null,
                column: "fatherWhatsapp",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "fatherWhatsapp",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "fatherMobile",
                keyValue: null,
                column: "fatherMobile",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "fatherMobile",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "fatherEmail",
                keyValue: null,
                column: "fatherEmail",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "fatherEmail",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "elq_BranchName",
                keyValue: null,
                column: "elq_BranchName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "elq_BranchName",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "dobHijri",
                keyValue: null,
                column: "dobHijri",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "dobHijri",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "dobGregorian",
                keyValue: null,
                column: "dobGregorian",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "dobGregorian",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "bloodGroup",
                keyValue: null,
                column: "bloodGroup",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_student",
                keyColumn: "address",
                keyValue: null,
                column: "address",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "mz_student",
                type: "text",
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "mz_receipt_payment_modes",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_receipt_payment_modes",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_receipt_payment_modes",
                keyColumn: "description",
                keyValue: null,
                column: "description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mz_receipt_payment_modes",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_on_off_modules",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_on_off_modules",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_loginlogs",
                keyColumn: "ipAddress",
                keyValue: null,
                column: "ipAddress",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "mz_loginlogs",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_loginlogs",
                keyColumn: "deviceDetails",
                keyValue: null,
                column: "deviceDetails",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "deviceDetails",
                table: "mz_loginlogs",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_kg_wajebaat_araz",
                keyColumn: "wajebaatType",
                keyValue: null,
                column: "wajebaatType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "wajebaatType",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValueSql: "'Wajebaat Niyat'",
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldDefaultValueSql: "'Wajebaat Niyat'")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_kg_wajebaat_araz",
                keyColumn: "userRemarks",
                keyValue: null,
                column: "userRemarks",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "userRemarks",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_kg_wajebaat_araz",
                keyColumn: "updatedBy",
                keyValue: null,
                column: "updatedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "updatedBy",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_kg_wajebaat_araz",
                keyColumn: "stage",
                keyValue: null,
                column: "stage",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "stage",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValueSql: "'Initialized'",
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldDefaultValueSql: "'Initialized'")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_kg_wajebaat_araz",
                keyColumn: "officeRemarks",
                keyValue: null,
                column: "officeRemarks",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "officeRemarks",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_kg_wajebaat_araz",
                keyColumn: "draftNo",
                keyValue: null,
                column: "draftNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "draftNo",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_kg_wajebaat_araz",
                keyColumn: "displayCurrency",
                keyValue: null,
                column: "displayCurrency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "displayCurrency",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_kg_wajebaat_araz",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_kg_wajebaat_araz",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_kg_wajebaat_araz",
                keyColumn: "bankName",
                keyValue: null,
                column: "bankName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_kg_wajebaat_araz",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_fee_collection_center",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_fee_collection_center",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_fee_collection_center",
                keyColumn: "description",
                keyValue: null,
                column: "description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mz_fee_collection_center",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_faculty_loginlogs",
                keyColumn: "ipAddress",
                keyValue: null,
                column: "ipAddress",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "mz_faculty_loginlogs",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_faculty_loginlogs",
                keyColumn: "deviceDetails",
                keyValue: null,
                column: "deviceDetails",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "deviceDetails",
                table: "mz_faculty_loginlogs",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_wallet",
                keyColumn: "paymentType",
                keyValue: null,
                column: "paymentType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentType",
                table: "mz_expense_vendor_wallet",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_wallet",
                keyColumn: "note",
                keyValue: null,
                column: "note",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_expense_vendor_wallet",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_wallet",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_expense_vendor_wallet",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_wallet",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_vendor_wallet",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_payment",
                keyColumn: "transactionId",
                keyValue: null,
                column: "transactionId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "transactionId",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_payment",
                keyColumn: "status",
                keyValue: null,
                column: "status",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_payment",
                keyColumn: "paymentMode",
                keyValue: null,
                column: "paymentMode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_payment",
                keyColumn: "note",
                keyValue: null,
                column: "note",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_payment",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_payment",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_vendor_payment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_payment",
                keyColumn: "chequeDate",
                keyValue: null,
                column: "chequeDate",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "chequeDate",
                table: "mz_expense_vendor_payment",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "whatsappNo",
                keyValue: null,
                column: "whatsappNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "whatsappNo",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "type",
                keyValue: null,
                column: "type",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "state",
                keyValue: null,
                column: "state",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "state",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "phoneNo",
                keyValue: null,
                column: "phoneNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "phoneNo",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "panCardNo",
                keyValue: null,
                column: "panCardNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "panCardNo",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "mobileNo",
                keyValue: null,
                column: "mobileNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "mobileNo",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "ifscCode",
                keyValue: null,
                column: "ifscCode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ifscCode",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "gstNumber",
                keyValue: null,
                column: "gstNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "gstNumber",
                table: "mz_expense_vendor_master",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "email",
                keyValue: null,
                column: "email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "mz_expense_vendor_master",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "city",
                keyValue: null,
                column: "city",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "bankName",
                keyValue: null,
                column: "bankName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "address",
                keyValue: null,
                column: "address",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "accountNo",
                keyValue: null,
                column: "accountNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "accountNo",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_vendor_master",
                keyColumn: "accountName",
                keyValue: null,
                column: "accountName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "accountName",
                table: "mz_expense_vendor_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_student_budget_issue_logs",
                keyColumn: "remark",
                keyValue: null,
                column: "remark",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "mz_expense_student_budget_issue_logs",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_student_budget_issue_logs",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_student_budget_issue_logs",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_student_budget_issue_logs",
                keyColumn: "arazState",
                keyValue: null,
                column: "arazState",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "arazState",
                table: "mz_expense_student_budget_issue_logs",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_procurement_item",
                keyColumn: "uom",
                keyValue: null,
                column: "uom",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "uom",
                table: "mz_expense_procurement_item",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_procurement_item",
                keyColumn: "type",
                keyValue: null,
                column: "type",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "mz_expense_procurement_item",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_procurement_item",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_procurement_item",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_procurement_baseitem",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_procurement_baseitem",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_online_payment_users",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_online_payment_users",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_online_payment_users",
                keyColumn: "ifsc",
                keyValue: null,
                column: "ifsc",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ifsc",
                table: "mz_expense_online_payment_users",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_online_payment_users",
                keyColumn: "bankName",
                keyValue: null,
                column: "bankName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_expense_online_payment_users",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_online_payment_users",
                keyColumn: "accNum",
                keyValue: null,
                column: "accNum",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "accNum",
                table: "mz_expense_online_payment_users",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_online_payment_users",
                keyColumn: "accName",
                keyValue: null,
                column: "accName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "accName",
                table: "mz_expense_online_payment_users",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_estimate_student",
                keyColumn: "remarks_admin",
                keyValue: null,
                column: "remarks_admin",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "remarks_admin",
                table: "mz_expense_estimate_student",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_estimate_student",
                keyColumn: "remarks",
                keyValue: null,
                column: "remarks",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_expense_estimate_student",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_estimate_student",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_estimate_student",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_deptvenue_cash_wallet",
                keyColumn: "paymentType",
                keyValue: null,
                column: "paymentType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentType",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_deptvenue_cash_wallet",
                keyColumn: "note",
                keyValue: null,
                column: "note",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_deptvenue_cash_wallet",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_deptvenue_cash_wallet",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_smart_issue_logs",
                keyColumn: "remark",
                keyValue: null,
                column: "remark",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "mz_expense_budget_smart_issue_logs",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_smart_issue_logs",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_budget_smart_issue_logs",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_smart_goals",
                keyColumn: "updatedBy",
                keyValue: null,
                column: "updatedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "updatedBy",
                table: "mz_expense_budget_smart_goals",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_smart_goals",
                keyColumn: "specific",
                keyValue: null,
                column: "specific",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "specific",
                table: "mz_expense_budget_smart_goals",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_smart_goals",
                keyColumn: "remarks_admin",
                keyValue: null,
                column: "remarks_admin",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "remarks_admin",
                table: "mz_expense_budget_smart_goals",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_smart_goals",
                keyColumn: "relevant",
                keyValue: null,
                column: "relevant",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "relevant",
                table: "mz_expense_budget_smart_goals",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_smart_goals",
                keyColumn: "measearable",
                keyValue: null,
                column: "measearable",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "measearable",
                table: "mz_expense_budget_smart_goals",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_smart_goals",
                keyColumn: "category",
                keyValue: null,
                column: "category",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "mz_expense_budget_smart_goals",
                type: "varchar(75)",
                maxLength: 75,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(75)",
                oldMaxLength: 75,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_smart_goals",
                keyColumn: "attainable",
                keyValue: null,
                column: "attainable",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "attainable",
                table: "mz_expense_budget_smart_goals",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_araz_transfer_logs",
                keyColumn: "trabferModel",
                keyValue: null,
                column: "trabferModel",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "trabferModel",
                table: "mz_expense_budget_araz_transfer_logs",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_araz",
                keyColumn: "updatedBy",
                keyValue: null,
                column: "updatedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "updatedBy",
                table: "mz_expense_budget_araz",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_araz",
                keyColumn: "uom",
                keyValue: null,
                column: "uom",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "uom",
                table: "mz_expense_budget_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_araz",
                keyColumn: "remarks_admin",
                keyValue: null,
                column: "remarks_admin",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "remarks_admin",
                table: "mz_expense_budget_araz",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_araz",
                keyColumn: "justification",
                keyValue: null,
                column: "justification",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "justification",
                table: "mz_expense_budget_araz",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_budget_araz",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_budget_araz",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bills_package",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_bills_package",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bills_package",
                keyColumn: "description",
                keyValue: null,
                column: "description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mz_expense_bills_package",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bill_master",
                keyColumn: "status",
                keyValue: null,
                column: "status",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "mz_expense_bill_master",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bill_master",
                keyColumn: "paymentTo_ifsc",
                keyValue: null,
                column: "paymentTo_ifsc",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_ifsc",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bill_master",
                keyColumn: "paymentTo_BankName",
                keyValue: null,
                column: "paymentTo_BankName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_BankName",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bill_master",
                keyColumn: "paymentTo_AccNum",
                keyValue: null,
                column: "paymentTo_AccNum",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_AccNum",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bill_master",
                keyColumn: "paymentTo_AccName",
                keyValue: null,
                column: "paymentTo_AccName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_AccName",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bill_master",
                keyColumn: "paymentMode_User",
                keyValue: null,
                column: "paymentMode_User",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode_User",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bill_master",
                keyColumn: "paymentMode_Admin",
                keyValue: null,
                column: "paymentMode_Admin",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode_Admin",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bill_master",
                keyColumn: "paymentFrom_BankName",
                keyValue: null,
                column: "paymentFrom_BankName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "paymentFrom_BankName",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bill_master",
                keyColumn: "createdBy",
                keyValue: null,
                column: "createdBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bill_master",
                keyColumn: "billNo",
                keyValue: null,
                column: "billNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "billNo",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "mz_expense_bill_item",
                keyColumn: "remarks",
                keyValue: null,
                column: "remarks",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_expense_bill_item",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_worktype",
                keyColumn: "typeName",
                keyValue: null,
                column: "typeName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "typeName",
                table: "kg_worktype",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_worktype",
                keyColumn: "description",
                keyValue: null,
                column: "description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "kg_worktype",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_self_assessment",
                keyColumn: "weakness",
                keyValue: null,
                column: "weakness",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "weakness",
                table: "kg_self_assessment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_self_assessment",
                keyColumn: "strength",
                keyValue: null,
                column: "strength",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "strength",
                table: "kg_self_assessment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_self_assessment",
                keyColumn: "roleModel",
                keyValue: null,
                column: "roleModel",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "roleModel",
                table: "kg_self_assessment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_self_assessment",
                keyColumn: "personalitytype",
                keyValue: null,
                column: "personalitytype",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "personalitytype",
                table: "kg_self_assessment",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_self_assessment",
                keyColumn: "personalityReport",
                keyValue: null,
                column: "personalityReport",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "personalityReport",
                table: "kg_self_assessment",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_self_assessment",
                keyColumn: "longTermGoal",
                keyValue: null,
                column: "longTermGoal",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "longTermGoal",
                table: "kg_self_assessment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_self_assessment",
                keyColumn: "changeAboutYourself",
                keyValue: null,
                column: "changeAboutYourself",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "changeAboutYourself",
                table: "kg_self_assessment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_self_assessment",
                keyColumn: "alternativeCareerPath",
                keyValue: null,
                column: "alternativeCareerPath",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "alternativeCareerPath",
                table: "kg_self_assessment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_self_assessment",
                keyColumn: "aboutYourSelf",
                keyValue: null,
                column: "aboutYourSelf",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "aboutYourSelf",
                table: "kg_self_assessment",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_identitycards",
                keyColumn: "nameOnCard",
                keyValue: null,
                column: "nameOnCard",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "nameOnCard",
                table: "kg_identitycards",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_identitycards",
                keyColumn: "country",
                keyValue: null,
                column: "country",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "kg_identitycards",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_identitycards",
                keyColumn: "cardType",
                keyValue: null,
                column: "cardType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "cardType",
                table: "kg_identitycards",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_identitycards",
                keyColumn: "cardNumber",
                keyValue: null,
                column: "cardNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "cardNumber",
                table: "kg_identitycards",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_identitycards",
                keyColumn: "attachment",
                keyValue: null,
                column: "attachment",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "attachment",
                table: "kg_identitycards",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails_its",
                keyColumn: "relation",
                keyValue: null,
                column: "relation",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "relation",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails_its",
                keyColumn: "occupation",
                keyValue: null,
                column: "occupation",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "occupation",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails_its",
                keyColumn: "nationality",
                keyValue: null,
                column: "nationality",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails_its",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails_its",
                keyColumn: "jamaat",
                keyValue: null,
                column: "jamaat",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails_its",
                keyColumn: "idara",
                keyValue: null,
                column: "idara",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "idara",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails_its",
                keyColumn: "hifzStatus",
                keyValue: null,
                column: "hifzStatus",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "hifzStatus",
                table: "kg_faimalydetails_its",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails_its",
                keyColumn: "bloodGroup",
                keyValue: null,
                column: "bloodGroup",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "kg_faimalydetails_its",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails_its",
                keyColumn: "age",
                keyValue: null,
                column: "age",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "age",
                table: "kg_faimalydetails_its",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails",
                keyColumn: "relation",
                keyValue: null,
                column: "relation",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "relation",
                table: "kg_faimalydetails",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails",
                keyColumn: "occupation",
                keyValue: null,
                column: "occupation",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "occupation",
                table: "kg_faimalydetails",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails",
                keyColumn: "nationality",
                keyValue: null,
                column: "nationality",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "kg_faimalydetails",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "kg_faimalydetails",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails",
                keyColumn: "jamaat",
                keyValue: null,
                column: "jamaat",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "kg_faimalydetails",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails",
                keyColumn: "idara",
                keyValue: null,
                column: "idara",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "idara",
                table: "kg_faimalydetails",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails",
                keyColumn: "hifzStatus",
                keyValue: null,
                column: "hifzStatus",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "hifzStatus",
                table: "kg_faimalydetails",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails",
                keyColumn: "bloodGroup",
                keyValue: null,
                column: "bloodGroup",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "kg_faimalydetails",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "kg_faimalydetails",
                keyColumn: "age",
                keyValue: null,
                column: "age",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "age",
                table: "kg_faimalydetails",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "ikhtibaar_marksheet",
                keyColumn: "type",
                keyValue: null,
                column: "type",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "ikhtibaar_marksheet",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "ikhtibaar_marksheet",
                keyColumn: "remarks",
                keyValue: null,
                column: "remarks",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "ikhtibaar_marksheet",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "ikhtibaar_marksheet",
                keyColumn: "mukhtabir",
                keyValue: null,
                column: "mukhtabir",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "mukhtabir",
                table: "ikhtibaar_marksheet",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "ikhtibaar_marksheet",
                keyColumn: "marks",
                keyValue: null,
                column: "marks",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "marks",
                table: "ikhtibaar_marksheet",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "ikhtibaar_marksheet",
                keyColumn: "category",
                keyValue: null,
                column: "category",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "ikhtibaar_marksheet",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "form_response",
                keyColumn: "response",
                keyValue: null,
                column: "response",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "response",
                table: "form_response",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "form_response",
                keyColumn: "identifier",
                keyValue: null,
                column: "identifier",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "identifier",
                table: "form_response",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "form_questionnaire",
                keyColumn: "type",
                keyValue: null,
                column: "type",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "form_questionnaire",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "form_questionnaire",
                keyColumn: "description",
                keyValue: null,
                column: "description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "form_questionnaire",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "form",
                keyColumn: "setting",
                keyValue: null,
                column: "setting",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "setting",
                table: "form",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "form",
                keyColumn: "description",
                keyValue: null,
                column: "description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "form",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "export_type_displayheader",
                keyColumn: "displayName",
                keyValue: null,
                column: "displayName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "displayName",
                table: "export_type_displayheader",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "export_type_displayheader",
                keyColumn: "actualName",
                keyValue: null,
                column: "actualName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "actualName",
                table: "export_type_displayheader",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "export_type",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "export_type",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "export_type",
                keyColumn: "fileName",
                keyValue: null,
                column: "fileName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "fileName",
                table: "export_type",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "export_type",
                keyColumn: "description",
                keyValue: null,
                column: "description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "export_type",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "export_category",
                keyColumn: "fieldDisplayName",
                keyValue: null,
                column: "fieldDisplayName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "fieldDisplayName",
                table: "export_category",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "export_category",
                keyColumn: "fieldActualName",
                keyValue: null,
                column: "fieldActualName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "fieldActualName",
                table: "export_category",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "export_category",
                keyColumn: "categoryName",
                keyValue: null,
                column: "categoryName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "categoryName",
                table: "export_category",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_salary",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "employee_salary",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_passport_details",
                keyColumn: "placeOfIssue",
                keyValue: null,
                column: "placeOfIssue",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "placeOfIssue",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_passport_details",
                keyColumn: "passportPlaceOfBirth",
                keyValue: null,
                column: "passportPlaceOfBirth",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "passportPlaceOfBirth",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_passport_details",
                keyColumn: "passportNo",
                keyValue: null,
                column: "passportNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "passportNo",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_passport_details",
                keyColumn: "passportName",
                keyValue: null,
                column: "passportName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "passportName",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_passport_details",
                keyColumn: "passportCopy",
                keyValue: null,
                column: "passportCopy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "passportCopy",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_passport_details",
                keyColumn: "dobPassport",
                keyValue: null,
                column: "dobPassport",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "dobPassport",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_passport_details",
                keyColumn: "dateOfIssue",
                keyValue: null,
                column: "dateOfIssue",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "dateOfIssue",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_passport_details",
                keyColumn: "dateOfExpiry",
                keyValue: null,
                column: "dateOfExpiry",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "dateOfExpiry",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_khidmat_details",
                keyColumn: "khidmatMauzeHouseStatus",
                keyValue: null,
                column: "khidmatMauzeHouseStatus",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "khidmatMauzeHouseStatus",
                table: "employee_khidmat_details",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_khidmat_details",
                keyColumn: "khdimatMauzeHouseType",
                keyValue: null,
                column: "khdimatMauzeHouseType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "khdimatMauzeHouseType",
                table: "employee_khidmat_details",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_family_details",
                keyColumn: "SpouseName",
                keyValue: null,
                column: "SpouseName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "SpouseName",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_family_details",
                keyColumn: "SpouseIts",
                keyValue: null,
                column: "SpouseIts",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "SpouseIts",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_family_details",
                keyColumn: "MotherName",
                keyValue: null,
                column: "MotherName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MotherName",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_family_details",
                keyColumn: "MotherIts",
                keyValue: null,
                column: "MotherIts",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MotherIts",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_family_details",
                keyColumn: "FatherName",
                keyValue: null,
                column: "FatherName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "FatherName",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_family_details",
                keyColumn: "FatherIts",
                keyValue: null,
                column: "FatherIts",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "FatherIts",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_e_attendence",
                keyColumn: "logJson",
                keyValue: null,
                column: "logJson",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "logJson",
                table: "employee_e_attendence",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_bank_details",
                keyColumn: "ifsc",
                keyValue: null,
                column: "ifsc",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ifsc",
                table: "employee_bank_details",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_bank_details",
                keyColumn: "chequeAttachment",
                keyValue: null,
                column: "chequeAttachment",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "chequeAttachment",
                table: "employee_bank_details",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_bank_details",
                keyColumn: "bankName",
                keyValue: null,
                column: "bankName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "employee_bank_details",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_bank_details",
                keyColumn: "bankBranch",
                keyValue: null,
                column: "bankBranch",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bankBranch",
                table: "employee_bank_details",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_bank_details",
                keyColumn: "bankAccountType",
                keyValue: null,
                column: "bankAccountType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bankAccountType",
                table: "employee_bank_details",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_bank_details",
                keyColumn: "bankAccountNumber",
                keyValue: null,
                column: "bankAccountNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bankAccountNumber",
                table: "employee_bank_details",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "employee_bank_details",
                keyColumn: "bankAccountName",
                keyValue: null,
                column: "bankAccountName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bankAccountName",
                table: "employee_bank_details",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "dept_venue_baseitem",
                keyColumn: "tag",
                keyValue: null,
                column: "tag",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "tag",
                table: "dept_venue_baseitem",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "dept_venue",
                keyColumn: "venueName",
                keyValue: null,
                column: "venueName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "venueName",
                table: "dept_venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "dept_venue",
                keyColumn: "tag",
                keyValue: null,
                column: "tag",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "tag",
                table: "dept_venue",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "dept_venue",
                keyColumn: "status",
                keyValue: null,
                column: "status",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "dept_venue",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "dept_venue",
                keyColumn: "masterDeptName",
                keyValue: null,
                column: "masterDeptName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "masterDeptName",
                table: "dept_venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "dept_venue",
                keyColumn: "deptName",
                keyValue: null,
                column: "deptName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "deptName",
                table: "dept_venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "department",
                keyColumn: "tag",
                keyValue: null,
                column: "tag",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "tag",
                table: "department",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "azwaaj_minentry",
                keyColumn: "description",
                keyValue: null,
                column: "description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "azwaaj_minentry",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "acedemicyear_data",
                keyColumn: "acedemicName",
                keyValue: null,
                column: "acedemicName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "acedemicName",
                table: "acedemicyear_data",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");
        }
    }
}
