using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class makePackageIdOptionalAndAddYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterTable(
                name: "yellowreceipt")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_workshops_category_subcategory")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_workshop_data")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_qualification_stage_degree")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_qualification_new")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_maqaraat_teacher")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_maqaraat_session")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_maqaraat_data")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_workshopcategory")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_titlecategory")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_mode")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_fieldofinterest")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_degree")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_category")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_authoredcategory")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafd_physicalfitness")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafd_otheridara_mawaze")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafd_mahad_past_mawaze")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafd_languageproficiency")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "wafd_fieldofinterest")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "venue")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "useritemassociation")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "userdeptassociation")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "user_deptvenue")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "user_dept-venue_baseitem")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "salary_querylogs")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "salary_generation_hijri")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "salary_generation_gegorgian")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "role_rights")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "role_module")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "role")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "rights")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "registrationform_subprograms")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "registrationform_programs")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "registrationform_dropdown_set")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "qismal_tahfeez_student")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_user_pset")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_user_deptvenue")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_user")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_role_right")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_role_module")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_role")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_login_logs")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_api_logs")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "printableformat_report_rights")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "printableformat_report")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "nisaab_periods")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "nisaab_jadwal")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "nisaab_classes_monitor")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "nisaab_classes")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_receipt")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_feecategory_pset")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_feecategory")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_fee_transaction")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_fee_allotment")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_ewallet")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_receipt_payment_modes")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_receipt_payment_mode_rights")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_pset_elqgrpid_mapping")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_on_off_modules")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_loginlogs")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_kg_wajebaat_araz",
                comment: "		",
                oldComment: "		")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_fee_collection_center")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_faculty_loginlogs")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_expense_vendor_master")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_expense_procurement_item")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_expense_procurement_baseitem")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "module_rights")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "module")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "masterdepartment")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "kg_worktype")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "kg_venue_worktype")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "kg_self_assessment")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "kg_identitycards")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "kg_faimalydetails_its")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "kg_faimalydetails")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "ikhtibaar_marksheet")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_unicode_ci");

            migrationBuilder.AlterTable(
                name: "hijri_months")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "hijri_calender")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "export_type_displayheader")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "export_type")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "export_category")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "employee_salary")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "employee_passport_details")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "employee_logs")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "employee_khidmat_details")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "employee_family_details")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "employee_bank_details")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "employee_academic_details")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "latin1")
                .OldAnnotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.AlterTable(
                name: "dropdown_dataset_options")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "dropdown_dataset_header")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "dept_venue_baseitem")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "dept_venue")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "department")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "azwaaj_minentry")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterTable(
                name: "acedemicyear_data")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "subCategory",
                table: "wafdprofile_workshops_category_subcategory",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "wafdprofile_workshops_category_subcategory",
                type: "text",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "cp1256")
                .OldAnnotation("Relational:Collation", "cp1256_general_ci");


            migrationBuilder.AddColumn<int>(
                name: "toYear",
                table: "mzlm_leave_application",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "fromYear",
                table: "mzlm_leave_application",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_azwaaj_entry_kg_vf",
                table: "azwaaj_minentry");

            migrationBuilder.DropForeignKey(
                name: "fk_salary_gen_gegorgian_dept",
                table: "salary_generation_gegorgian");

            migrationBuilder.DropForeignKey(
                name: "fk_salary_gen_h_salary_type",
                table: "salary_generation_hijri");

            migrationBuilder.DropIndex(
                name: "Secondary3",
                table: "salary_generation_hijri");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "mzlm_leave_package");

            migrationBuilder.DropIndex(
                name: "fk_azwaaj_entry_kg_vf_idx",
                table: "azwaaj_minentry");

            migrationBuilder.DropColumn(
                name: "degreeNum",
                table: "nisaab_alumni");

            migrationBuilder.DropColumn(
                name: "fromYear",
                table: "mzlm_leave_application");

            migrationBuilder.RenameIndex(
                name: "fk_salary_gen_h_salary_type",
                table: "salary_generation_hijri",
                newName: "fk_salary_gen_h_salary_type_idx");

            migrationBuilder.RenameIndex(
                name: "fk_salary_gen_gegorgian_dept_idx1",
                table: "salary_generation_gegorgian",
                newName: "fk_salary_gen_gegorian_allocation_idx");

            migrationBuilder.RenameIndex(
                name: "fk_salary_gen_gegorgian_dept_idx",
                table: "salary_generation_gegorgian",
                newName: "fk_salary_gen_gegorian_dept");

            migrationBuilder.RenameIndex(
                name: "fk_salary_allocation_h_kh",
                table: "salary_allocation_hijri",
                newName: "fk_sallary_allocation_h_kh_idx");

            migrationBuilder.RenameColumn(
                name: "toYear",
                table: "mzlm_leave_application",
                newName: "poackageId");

            migrationBuilder.AlterTable(
                name: "yellowreceipt")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_workshops_category_subcategory")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_qualification_stage_degree")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_qualification_new")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_maqaraat_teacher")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_maqaraat_session")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_maqaraat_data")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_workshopcategory")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_titlecategory")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_mode")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_fieldofinterest")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_degree")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_category")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafdprofile_dropdown_authoredcategory")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafd_physicalfitness")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafd_otheridara_mawaze")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafd_mahad_past_mawaze")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafd_languageproficiency")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "wafd_fieldofinterest")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "venue")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "useritemassociation")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "userdeptassociation")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "user_deptvenue")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "user_dept-venue_baseitem")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "salary_querylogs")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "salary_generation_hijri")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "salary_generation_gegorgian")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "role_rights")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "role_module")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "role")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "rights")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "registrationform_subprograms")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "registrationform_programs")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "registrationform_dropdown_set")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "qismal_tahfeez_student")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_user_pset")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_user_deptvenue")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_user")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_role_right")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_role_module")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_role")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_login_logs")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "qism_al_tahfeez_api_logs")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "printableformat_report_rights")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "printableformat_report")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "nisaab_periods")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "nisaab_jadwal")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "nisaab_classes_monitor")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "nisaab_classes")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_receipt")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_feecategory_pset")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_feecategory")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_fee_transaction")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_fee_allotment")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_student_ewallet")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_receipt_payment_modes")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_receipt_payment_mode_rights")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_pset_elqgrpid_mapping")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_on_off_modules")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_loginlogs")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_kg_wajebaat_araz",
                comment: "		",
                oldComment: "		")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_fee_collection_center")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_faculty_loginlogs")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_expense_vendor_master")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_expense_procurement_item")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "mz_expense_procurement_baseitem")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "module_rights")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "module")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "masterdepartment")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "kg_worktype")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "kg_venue_worktype")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "kg_self_assessment")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "kg_identitycards")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "kg_faimalydetails_its")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "kg_faimalydetails")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "ikhtibaar_marksheet")
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_unicode_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "hijri_months")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "hijri_calender")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "export_type_displayheader")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "export_type")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "export_category")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "employee_salary")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "employee_passport_details")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "employee_logs")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "employee_khidmat_details")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "employee_family_details")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "employee_bank_details")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "employee_academic_details")
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "dropdown_dataset_options")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "dropdown_dataset_header")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "dept_venue_baseitem")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "dept_venue")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "department")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "azwaaj_minentry")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterTable(
                name: "acedemicyear_data")
                .Annotation("MySql:CharSet", "cp1256")
                .Annotation("Relational:Collation", "cp1256_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "whatsappNo",
                table: "yellowreceipt",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "yellowreceipt",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValueSql: "'Paid'",
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldDefaultValueSql: "'Pending'")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "purpose",
                table: "yellowreceipt",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "yellowreceipt",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "yellowreceipt",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMode",
                table: "yellowreceipt",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "PaidAt",
                table: "yellowreceipt",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "yellowreceipt",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "yellowreceipt",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ChequeNo",
                table: "yellowreceipt",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "yellowreceipt",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Account",
                table: "yellowreceipt",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "subCategory",
                table: "wafdprofile_workshops_category_subcategory",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "wafdprofile_workshops_category_subcategory",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "stage",
                table: "wafdprofile_qualification_stage_degree",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "degree",
                table: "wafdprofile_qualification_stage_degree",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "year",
                table: "wafdprofile_qualification_new",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "wafdprofile_qualification_new",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "stage",
                table: "wafdprofile_qualification_new",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "pursuingYear",
                table: "wafdprofile_qualification_new",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "mediumOfEducation",
                table: "wafdprofile_qualification_new",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "institutionName",
                table: "wafdprofile_qualification_new",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "degree",
                table: "wafdprofile_qualification_new",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "wafdprofile_qualification_new",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "attachment",
                table: "wafdprofile_qualification_new",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "days",
                table: "wafdprofile_maqaraat_teacher",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "wafdprofile_maqaraat_teacher",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "reason",
                table: "wafdprofile_maqaraat_session",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "wafdprofile_maqaraat_session",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "absentReason",
                table: "wafdprofile_maqaraat_data",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "userName",
                table: "wafdprofile_english_assessment",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "wafdprofile_english_assessment",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "examLink",
                table: "wafdprofile_english_assessment",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "wafdprofile_dropdown_workshopcategory",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "wafdprofile_dropdown_titlecategory",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "wafdprofile_dropdown_mode",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "wafdprofile_dropdown_fieldofinterest",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "wafdprofile_dropdown_degree",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "wafdprofile_dropdown_category",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "wafdprofile_dropdown_authoredcategory",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "sports",
                table: "wafd_physicalfitness",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "mauze",
                table: "wafd_otheridara_mawaze",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "khidmatNature",
                table: "wafd_otheridara_mawaze",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "program",
                table: "wafd_mahad_past_mawaze",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "mauze",
                table: "wafd_mahad_past_mawaze",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "writing",
                table: "wafd_languageproficiency",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "speaking",
                table: "wafd_languageproficiency",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "reading",
                table: "wafd_languageproficiency",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "listening",
                table: "wafd_languageproficiency",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "language",
                table: "wafd_languageproficiency",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "selfRanking",
                table: "wafd_fieldofinterest",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "fieldofInterest",
                table: "wafd_fieldofinterest",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Idara",
                table: "userdeptassociation",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "userdeptassociation",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentCode",
                table: "userdeptassociation",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "userdeptassociation",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "loginStatus",
                table: "user",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "user",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "EmailId",
                table: "user",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "salary_querylogs",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "salary_querylogs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "salary_querylogs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "salary_generation_hijri",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "salary_generation_gegorgian",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "roleName",
                table: "role",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "role",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "rightsName",
                table: "rights",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "registrationform_subprograms",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "registrationform_programs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "studentName",
                table: "qismal_tahfeez_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "qismal_tahfeez_student",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "mobile2",
                table: "qismal_tahfeez_student",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "mobile1",
                table: "qismal_tahfeez_student",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "qismal_tahfeez_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "inActive_Reason",
                table: "qismal_tahfeez_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "hdob",
                table: "qismal_tahfeez_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "qismal_tahfeez_student",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "feeStatus",
                table: "qismal_tahfeez_student",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "emailId",
                table: "qismal_tahfeez_student",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "dob",
                table: "qismal_tahfeez_student",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "qismal_tahfeez_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "qismal_tahfeez_student",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "qismal_tahfeez_student",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "qism_al_tahfeez_role",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "qism_al_tahfeez_role",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "qism_al_tahfeez_login_logs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "qism_al_tahfeez_login_logs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "deviceDetails",
                table: "qism_al_tahfeez_login_logs",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "referrer",
                table: "qism_al_tahfeez_api_logs",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "loginName",
                table: "qism_al_tahfeez_api_logs",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "qism_al_tahfeez_api_logs",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "httpRequestType",
                table: "qism_al_tahfeez_api_logs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "deviceDetails",
                table: "qism_al_tahfeez_api_logs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "apiWithParameter",
                table: "qism_al_tahfeez_api_logs",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "apiString",
                table: "qism_al_tahfeez_api_logs",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "qism_al_tahfeez",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "qism_al_tahfeez",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "emailId",
                table: "qism_al_tahfeez",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "reportName",
                table: "printableformat_report",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "fileName",
                table: "printableformat_report",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "printableformat_report",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "payroll_salary_packages",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "payroll_salary_packages",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "timing",
                table: "nisaab_periods",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "periodName",
                table: "nisaab_periods",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "nisaab_classes",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "div",
                table: "nisaab_classes",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "className",
                table: "nisaab_classes",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "minApplicationDate",
                table: "mzlm_leave_category",
                type: "int(11)",
                nullable: false,
                defaultValueSql: "'3'",
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<string>(
                name: "transactionId",
                table: "mz_student_receipt",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "mz_student_receipt",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode",
                table: "mz_student_receipt",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_student_receipt",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_receipt",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student_receipt",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_student_receipt",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "account",
                table: "mz_student_receipt",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_feecategory_pset",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "categoryName",
                table: "mz_student_feecategory",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "transactionId",
                table: "mz_student_fee_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_student_fee_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode",
                table: "mz_student_fee_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_fee_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student_fee_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student_fee_excluding_list",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_student_fee_allotment",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "reason",
                table: "mz_student_fee_allotment",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_fee_allotment",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student_fee_allotment",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentType",
                table: "mz_student_ewallet",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_student_ewallet",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_student_ewallet",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student_ewallet",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "vatan",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "studentWhatsapp",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "studentMobile",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "studentEmail",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "photoPath",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "photoBase64",
                table: "mz_student",
                type: "varchar(10000)",
                maxLength: 10000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "nameEng",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "nameArabic",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "motherWhatsapp",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "motherMobile",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "motherEmail",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "maqaam",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "jamiat",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "fatherWhatsapp",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "fatherMobile",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "fatherEmail",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "elq_BranchName",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "dobHijri",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "dobGregorian",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "mz_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "cp1256_general_ci")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "cp1256");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_receipt_payment_modes",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mz_receipt_payment_modes",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_on_off_modules",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "mz_loginlogs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "deviceDetails",
                table: "mz_loginlogs",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "wajebaatType",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValueSql: "'Wajebaat Niyat'",
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldDefaultValueSql: "'Wajebaat Niyat'")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "userRemarks",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "updatedBy",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "stage",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValueSql: "'Initialized'",
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldDefaultValueSql: "'Initialized'")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "officeRemarks",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "draftNo",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "displayCurrency",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_kg_wajebaat_araz",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_fee_collection_center",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mz_fee_collection_center",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ipAddress",
                table: "mz_faculty_loginlogs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "deviceDetails",
                table: "mz_faculty_loginlogs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentType",
                table: "mz_expense_vendor_wallet",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_expense_vendor_wallet",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_expense_vendor_wallet",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_vendor_wallet",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "transactionId",
                table: "mz_expense_vendor_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_expense_vendor_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode",
                table: "mz_expense_vendor_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_expense_vendor_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_vendor_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "transactionId",
                table: "mz_expense_vendor_payment",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "mz_expense_vendor_payment",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode",
                table: "mz_expense_vendor_payment",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_expense_vendor_payment",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_expense_vendor_payment",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_vendor_payment",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "whatsappNo",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "state",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "phoneNo",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "panCardNo",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "mobileNo",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ifscCode",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "gstNumber",
                table: "mz_expense_vendor_master",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "mz_expense_vendor_master",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "mz_expense_vendor_master",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "accountNo",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "accountName",
                table: "mz_expense_vendor_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "mz_expense_student_budget_issue_logs",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "updatedBy",
                table: "mz_expense_sanctioned_budget",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "uom",
                table: "mz_expense_procurement_item",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "mz_expense_procurement_item",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_procurement_item",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_procurement_baseitem",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<bool>(
                name: "isIncome",
                table: "mz_expense_procurement_baseitem",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_online_payment_users",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "ifsc",
                table: "mz_expense_online_payment_users",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_expense_online_payment_users",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "accNum",
                table: "mz_expense_online_payment_users",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "accName",
                table: "mz_expense_online_payment_users",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks_admin",
                table: "mz_expense_estimate_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_expense_estimate_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_estimate_student",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentType",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_deptvenue_cash_wallet",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_expense_budget_transfer_logs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_budget_transfer_logs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "updatedBy",
                table: "mz_expense_budget_smart_goals",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks_admin",
                table: "mz_expense_budget_smart_goals",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "mz_expense_budget_issue_logs",
                type: "varchar(250)",
                maxLength: 250,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "updatedBy",
                table: "mz_expense_budget_araz",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks_admin",
                table: "mz_expense_budget_araz",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "justification",
                table: "mz_expense_budget_araz",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_budget_araz",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mz_expense_bills_package",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mz_expense_bills_package",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_ifsc",
                table: "mz_expense_bill_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_BankName",
                table: "mz_expense_bill_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_AccNum",
                table: "mz_expense_bill_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentTo_AccName",
                table: "mz_expense_bill_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode_User",
                table: "mz_expense_bill_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode_Admin",
                table: "mz_expense_bill_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentFrom_BankName",
                table: "mz_expense_bill_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_bill_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "billNo",
                table: "mz_expense_bill_master",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "mz_expense_bill_logs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_bill_logs",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_expense_bill_item",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "mz_expense_bank_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMode",
                table: "mz_expense_bank_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "mz_expense_bank_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "mz_expense_bank_transaction",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "moduleName",
                table: "module",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "masterDeptName",
                table: "masterdepartment",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

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
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
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
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
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
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
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

            migrationBuilder.AlterColumn<string>(
                name: "employeeType",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

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
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
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
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
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
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
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
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
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
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
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
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
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
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "typeName",
                table: "kg_worktype",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "kg_worktype",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "weakness",
                table: "kg_self_assessment",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "strength",
                table: "kg_self_assessment",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "roleModel",
                table: "kg_self_assessment",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "personalitytype",
                table: "kg_self_assessment",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "personalityReport",
                table: "kg_self_assessment",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "longTermGoal",
                table: "kg_self_assessment",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "changeAboutYourself",
                table: "kg_self_assessment",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "alternativeCareerPath",
                table: "kg_self_assessment",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "aboutYourSelf",
                table: "kg_self_assessment",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "nameOnCard",
                table: "kg_identitycards",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "kg_identitycards",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cardType",
                table: "kg_identitycards",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "cardNumber",
                table: "kg_identitycards",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "attachment",
                table: "kg_identitycards",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "relation",
                table: "kg_faimalydetails_its",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "occupation",
                table: "kg_faimalydetails_its",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "kg_faimalydetails_its",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "kg_faimalydetails_its",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "kg_faimalydetails_its",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "idara",
                table: "kg_faimalydetails_its",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "hifzStatus",
                table: "kg_faimalydetails_its",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "kg_faimalydetails_its",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "age",
                table: "kg_faimalydetails_its",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "relation",
                table: "kg_faimalydetails",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "occupation",
                table: "kg_faimalydetails",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "nationality",
                table: "kg_faimalydetails",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "kg_faimalydetails",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "jamaat",
                table: "kg_faimalydetails",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "idara",
                table: "kg_faimalydetails",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "hifzStatus",
                table: "kg_faimalydetails",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bloodGroup",
                table: "kg_faimalydetails",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "age",
                table: "kg_faimalydetails",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "ikhtibaar_marksheet",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
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
                nullable: false,
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
                nullable: false,
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
                nullable: false,
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
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "hijriMonthName",
                table: "hijri_months",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "displayName",
                table: "export_type_displayheader",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "actualName",
                table: "export_type_displayheader",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "export_type",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "fileName",
                table: "export_type",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "export_type",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "fieldDisplayName",
                table: "export_category",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "fieldActualName",
                table: "export_category",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "categoryName",
                table: "export_category",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                table: "employee_salary",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "placeOfIssue",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "passportPlaceOfBirth",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "passportNo",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "passportName",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "passportCopy",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "dobPassport",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "dateOfIssue",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "dateOfExpiry",
                table: "employee_passport_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "employee_logs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "khidmatMauzeHouseStatus",
                table: "employee_khidmat_details",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "khdimatMauzeHouseType",
                table: "employee_khidmat_details",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "SpouseName",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "SpouseIts",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "MotherName",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "MotherIts",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "FatherName",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "FatherIts",
                table: "employee_family_details",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<bool>(
                name: "isHijriSalary",
                table: "employee_dept_salary",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<string>(
                name: "ifsc",
                table: "employee_bank_details",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "chequeAttachment",
                table: "employee_bank_details",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankName",
                table: "employee_bank_details",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankBranch",
                table: "employee_bank_details",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankAccountType",
                table: "employee_bank_details",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankAccountNumber",
                table: "employee_bank_details",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "bankAccountName",
                table: "employee_bank_details",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "hifzStatus",
                table: "employee_academic_details",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "employee_academic_details",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "aljameaDegree",
                table: "employee_academic_details",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                collation: "latin1_swedish_ci",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "dropdown_dataset_options",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "dropdown_dataset_header",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "tag",
                table: "dept_venue_baseitem",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "venueName",
                table: "dept_venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "tag",
                table: "dept_venue",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "dept_venue",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "masterDeptName",
                table: "dept_venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "deptName",
                table: "dept_venue",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "tag",
                table: "department",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "deptName",
                table: "department",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "branch_user",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "emailId",
                table: "branch_user",
                type: "varchar(255)",
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "itsid",
                table: "azwaaj_minentry",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "azwaaj_minentry",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "azwaaj_minentry",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "acedemicName",
                table: "acedemicyear_data",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "cp1256_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "cp1256")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mzlm_leave_package",
                table: "mzlm_leave_package",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "fk_user_deptvenue_baseitem_idx",
                table: "user_dept-venue_baseitem",
                column: "baseItemId");

            migrationBuilder.CreateIndex(
                name: "fk_user_deptvenue_dept_idx",
                table: "user_dept-venue_baseitem",
                column: "dept_venueId");

            migrationBuilder.CreateIndex(
                name: "Secondary3",
                table: "salary_generation_hijri",
                columns: new[] { "itsId", "deptVenueId", "salaryType", "month", "year" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_salary_gen_gegorian_dept",
                table: "salary_generation_gegorgian",
                column: "deptVenueId",
                principalTable: "dept_venue",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_salary_gen_h_salary_type",
                table: "salary_generation_hijri",
                column: "salaryType",
                principalTable: "salary_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
