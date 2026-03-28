using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class stabblelizeDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

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
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

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
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

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
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

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
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

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
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "khidmat_guzaar",
                keyColumn: "whatsappNo",
                keyValue: null,
                column: "whatsappNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "whatsappNo",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "watanArabic",
                keyValue: null,
                column: "watanArabic",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "watanArabic",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "watanAdress",
                keyValue: null,
                column: "watanAdress",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "watanAdress",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "watan",
                keyValue: null,
                column: "watan",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "watan",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "subDesignation",
                keyValue: null,
                column: "subDesignation",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "subDesignation",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "salaryCode",
                keyValue: null,
                column: "salaryCode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "salaryCode",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "photoBase64",
                keyValue: null,
                column: "photoBase64",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "photoBase64",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "photo",
                keyValue: null,
                column: "photo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "photo",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "personalHouseType",
                keyValue: null,
                column: "personalHouseType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseType",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "personalHouseStatus",
                keyValue: null,
                column: "personalHouseStatus",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseStatus",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "khidmat_guzaar",
                keyColumn: "personalHouseArea",
                keyValue: null,
                column: "personalHouseArea",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseArea",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "personalHouseAddress",
                keyValue: null,
                column: "personalHouseAddress",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseAddress",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "panCardNo",
                keyValue: null,
                column: "panCardNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "panCardNo",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "officialEmailAddress",
                keyValue: null,
                column: "officialEmailAddress",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "officialEmailAddress",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "nationality",
                keyValue: null,
                column: "nationality",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "mz_idara",
                keyValue: null,
                column: "mz_idara",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "mz_idara",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "muqamArabic",
                keyValue: null,
                column: "muqamArabic",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "muqamArabic",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "muqam",
                keyValue: null,
                column: "muqam",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "muqam",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "mobileNo",
                keyValue: null,
                column: "mobileNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "mobileNo",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "maritalStatus",
                keyValue: null,
                column: "maritalStatus",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "maritalStatus",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "krNo",
                keyValue: null,
                column: "krNo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "krNo",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "jamiat",
                keyValue: null,
                column: "jamiat",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "jamiat",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "jaman",
                keyValue: null,
                column: "jaman",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "jaman",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "jamaat",
                keyValue: null,
                column: "jamaat",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "its_preferredIdara",
                keyValue: null,
                column: "its_preferredIdara",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "its_preferredIdara",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "its_idaras",
                keyValue: null,
                column: "its_idaras",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "its_idaras",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "fullNameArabic",
                keyValue: null,
                column: "fullNameArabic",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "fullNameArabic",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "fullName",
                keyValue: null,
                column: "fullName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "fullName",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "emailAddress",
                keyValue: null,
                column: "emailAddress",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "emailAddress",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "domicileParent",
                keyValue: null,
                column: "domicileParent",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "domicileParent",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "domicileAddressParents",
                keyValue: null,
                column: "domicileAddressParents",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "domicileAddressParents",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "dojHijri",
                keyValue: null,
                column: "dojHijri",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "dojHijri",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "dobHijri",
                keyValue: null,
                column: "dobHijri",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "dobHijri",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "dobGregorian",
                keyValue: null,
                column: "dobGregorian",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "dobGregorian",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "designation",
                keyValue: null,
                column: "designation",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "designation",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "dawat_title",
                keyValue: null,
                column: "dawat_title",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "dawat_title",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "currentAddress",
                keyValue: null,
                column: "currentAddress",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currentAddress",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "c_codeWhatsapp",
                keyValue: null,
                column: "c_codeWhatsapp",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "c_codeWhatsapp",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "khidmat_guzaar",
                keyColumn: "c_codeMobile",
                keyValue: null,
                column: "c_codeMobile",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "c_codeMobile",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "khidmat_guzaar",
                keyColumn: "bloodGroup",
                keyValue: null,
                column: "bloodGroup",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.UpdateData(
                table: "khidmat_guzaar",
                keyColumn: "RfId",
                keyValue: null,
                column: "RfId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "RfId",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "OthDegree",
                keyValue: null,
                column: "OthDegree",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "OthDegree",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "EduQualification",
                keyValue: null,
                column: "EduQualification",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "EduQualification",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "EduCompletion",
                keyValue: null,
                column: "EduCompletion",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "EduCompletion",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                keyColumn: "CreatedBy",
                keyValue: null,
                column: "CreatedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "khidmat_guzaar",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldMaxLength: 500,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "ho",
                table: "venue",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "displayName",
                table: "venue",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "venue",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "CampVenue",
                table: "venue",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "CampId",
                table: "venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "whatsappNo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "watanArabic",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "watanAdress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "watan",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "subDesignation",
                table: "khidmat_guzaar",
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
                name: "salaryCode",
                table: "khidmat_guzaar",
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
                name: "photoBase64",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "photo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseType",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseStatus",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseArea",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseAddress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "panCardNo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "officialEmailAddress",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "mz_idara",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "muqamArabic",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "muqam",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "mobileNo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "maritalStatus",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "krNo",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "jaman",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "its_preferredIdara",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "its_idaras",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "fullNameArabic",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "fullName",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "emailAddress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "domicileParent",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "domicileAddressParents",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "dojHijri",
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
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
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "designation",
                table: "khidmat_guzaar",
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
                name: "dawat_title",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "currentAddress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "c_codeWhatsapp",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "c_codeMobile",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "RfId",
                table: "khidmat_guzaar",
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
                name: "OthDegree",
                table: "khidmat_guzaar",
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
                name: "EduQualification",
                table: "khidmat_guzaar",
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
                name: "EduCompletion",
                table: "khidmat_guzaar",
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
                name: "CreatedBy",
                table: "khidmat_guzaar",
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
        }
    }
}
