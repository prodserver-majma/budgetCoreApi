using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class kg_charsetmodification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "whatsappNo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "watanArabic",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "watanAdress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "watan",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "photoBase64",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "photo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseType",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseStatus",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseArea",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseAddress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "panCardNo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "officialEmailAddress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "mz_idara",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "muqamArabic",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "muqam",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "mobileNo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "maritalStatus",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "krNo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "jamiat",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "jaman",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "its_preferredIdara",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "its_idaras",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "fullName",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "emailAddress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "domicileParent",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "domicileAddressParents",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "dojHijri",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "dobHijri",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "dobGregorian",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "dawat_title",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "c_codeWhatsapp",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "c_codeMobile",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "cp1256");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "whatsappNo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "watanArabic",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "watanAdress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "watan",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "photoBase64",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "photo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseType",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseArea",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "personalHouseAddress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "panCardNo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "officialEmailAddress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "mz_idara",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "muqamArabic",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "muqam",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "mobileNo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "maritalStatus",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "krNo",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "jamiat",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "jaman",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "its_preferredIdara",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "its_idaras",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "fullName",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "emailAddress",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "domicileParent",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "domicileAddressParents",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "dojHijri",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "dobHijri",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "dobGregorian",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "dawat_title",
                table: "khidmat_guzaar",
                type: "text",
                nullable: true,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
