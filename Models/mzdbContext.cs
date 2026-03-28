using mahadalzahrawebapi.Controllers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace mahadalzahrawebapi.Models;

public partial class mzdbContext : DbContext
{
    public mzdbContext(DbContextOptions<mzdbContext> options)
        : base(options)
    {
    }
    

    //public virtual DbSet<mz_expense_procurement_item_baseitem> mz_expense_procurement_item_baseitem { get; set; }
    public virtual DbSet<acedemicyear_data> acedemicyear_data { get; set; }

    public virtual DbSet<azwaaj_minentry> azwaaj_minentry { get; set; }

    public virtual DbSet<branch_user> branch_user { get; set; }
    public virtual DbSet<bmi_data> bmi_data { get; set; }

    public virtual DbSet<bankmaster> bankmaster { get; set; }

    public virtual DbSet<current_counter> current_counter { get; set; }

    public virtual DbSet<contenttypewithextention_data> contenttypewithextention_data { get; set; }

    public virtual DbSet<currency_converter_new> currency_converter_new { get; set; }

    public virtual DbSet<campfasal_kutub> campfasal_kutub { get; set; }

    public virtual DbSet<department> department { get; set; }

    public virtual DbSet<dept_venue> dept_venue { get; set; }

    public virtual DbSet<dept_venue_baseitem> dept_venue_baseitem { get; set; }

    public virtual DbSet<dropdown_dataset_header> dropdown_dataset_header { get; set; }

    public virtual DbSet<dropdown_dataset_options> dropdown_dataset_options { get; set; }

    public virtual DbSet<edit_table_column_logs> edit_table_column_logs { get; set; }

    public virtual DbSet<employee_academic_details> employee_academic_details { get; set; }

    public virtual DbSet<employee_bank_details> employee_bank_details { get; set; }

    public virtual DbSet<employee_dept_salary> employee_dept_salary { get; set; }

    public virtual DbSet<employee_e_attendence> employee_e_attendence { get; set; }

    public virtual DbSet<employee_family_details> employee_family_details { get; set; }

    public virtual DbSet<employee_khidmat_details> employee_khidmat_details { get; set; }

    public virtual DbSet<employee_logs> employee_logs { get; set; }

    public virtual DbSet<employee_passport_details> employee_passport_details { get; set; }

    public virtual DbSet<employee_salary> employee_salary { get; set; }

    public virtual DbSet<enayat_medical_billentry> enayat_medical_billentry { get; set; }

    public virtual DbSet<enayat_scholarship_billentry> enayat_scholarship_billentry { get; set; }

    public virtual DbSet<export_category> export_category { get; set; }

    public virtual DbSet<export_type> export_type { get; set; }

    public virtual DbSet<export_type_displayheader> export_type_displayheader { get; set; }

    public virtual DbSet<fitness_activity> fitness_activity { get; set; }

    public virtual DbSet<global_constant> global_constant { get; set; }

    public virtual DbSet<hijri_calender> hijri_calender { get; set; }

    public virtual DbSet<hijri_months> hijri_months { get; set; }
    public virtual DbSet<greg_months> greg_months { get; set; }

    public virtual DbSet<holiday_allocation> holiday_allocation { get; set; }

    public virtual DbSet<holiday_calender> holiday_calender { get; set; }

    public virtual DbSet<holiday_hijri_miqaat> holiday_hijri_miqaat { get; set; }

    public virtual DbSet<ikhtibaar> ikhtibaar { get; set; }

    public virtual DbSet<ikhtibaar_marksheet> ikhtibaar_marksheet { get; set; }

    public virtual DbSet<ikhtibaar_questionnaire> ikhtibaar_questionnaire { get; set; }

    public virtual DbSet<kg_faimalydetails> kg_faimalydetails { get; set; }

    public virtual DbSet<kg_faimalydetails_its> kg_faimalydetails_its { get; set; }

    public virtual DbSet<kg_identitycards> kg_identitycards { get; set; }

    public virtual DbSet<kg_self_assessment> kg_self_assessment { get; set; }

    public virtual DbSet<kg_venue_transfer_history> kg_venue_transfer_history { get; set; }

    public virtual DbSet<kg_venue_worktype> kg_venue_worktype { get; set; }

    public virtual DbSet<kg_worktype> kg_worktype { get; set; }


    public virtual DbSet<khidmat_guzaar> khidmat_guzaar { get; set; }

    public virtual DbSet<masterdepartment> masterdepartment { get; set; }

    public virtual DbSet<module> module { get; set; }

    public virtual DbSet<module_rights> module_rights { get; set; }

    public virtual DbSet<mz_expense_bank_transaction> mz_expense_bank_transaction { get; set; }

    public virtual DbSet<mz_expense_bill_item> mz_expense_bill_item { get; set; }

    public virtual DbSet<mz_expense_bill_logs> mz_expense_bill_logs { get; set; }

    public virtual DbSet<mz_expense_bill_master> mz_expense_bill_master { get; set; }

    public virtual DbSet<mz_expense_bills_package> mz_expense_bills_package { get; set; }

    public virtual DbSet<mz_expense_budget_araz> mz_expense_budget_araz { get; set; }

    public virtual DbSet<mz_expense_budget_araz_monthly> mz_expense_budget_araz_monthly { get; set; }


    public virtual DbSet<mz_expense_budget_araz_transfer_logs> mz_expense_budget_araz_transfer_logs { get; set; }

    public virtual DbSet<mz_expense_budget_issue_logs> mz_expense_budget_issue_logs { get; set; }

    public virtual DbSet<mz_expense_budget_smart_goals> mz_expense_budget_smart_goals { get; set; }

    public virtual DbSet<mz_expense_budget_smart_issue_logs> mz_expense_budget_smart_issue_logs { get; set; }

    public virtual DbSet<mz_expense_budget_transfer_logs> mz_expense_budget_transfer_logs { get; set; }

    public virtual DbSet<mz_expense_deptvenue_cash_wallet> mz_expense_deptvenue_cash_wallet { get; set; }

    public virtual DbSet<mz_expense_estimate_student> mz_expense_estimate_student { get; set; }

    public virtual DbSet<mz_expense_estimate_student_monthly> mz_expense_estimate_student_monthly { get; set; }

    public virtual DbSet<mz_expense_school_section_class> mz_expense_school_section_class { get; set; }

    public virtual DbSet<user_school_section_class> user_school_section_class { get; set; }

    public virtual DbSet<mz_expense_online_payment_users> mz_expense_online_payment_users { get; set; }

    public virtual DbSet<mz_expense_procurement_baseitem> mz_expense_procurement_baseitem { get; set; }

    public virtual DbSet<mz_expense_procurement_item> mz_expense_procurement_item { get; set; }

    public virtual DbSet<mz_expense_sanctioned_budget> mz_expense_sanctioned_budget { get; set; }

    public virtual DbSet<mz_expense_student_budget_issue_logs> mz_expense_student_budget_issue_logs { get; set; }

    public virtual DbSet<mz_expense_vendor_master> mz_expense_vendor_master { get; set; }

    public virtual DbSet<mz_expense_vendor_payment> mz_expense_vendor_payment { get; set; }

    public virtual DbSet<mz_expense_vendor_transaction> mz_expense_vendor_transaction { get; set; }

    public virtual DbSet<mz_expense_vendor_wallet> mz_expense_vendor_wallet { get; set; }

    public virtual DbSet<mz_faculty_loginlogs> mz_faculty_loginlogs { get; set; }

    public virtual DbSet<mz_fee_collection_center> mz_fee_collection_center { get; set; }

    public virtual DbSet<mz_kg_wajebaat_araz> mz_kg_wajebaat_araz { get; set; }

    public virtual DbSet<mz_loginlogs> mz_loginlogs { get; set; }

    public virtual DbSet<mz_off_module_exception> mz_off_module_exception { get; set; }

    public virtual DbSet<mz_on_off_modules> mz_on_off_modules { get; set; }

    public virtual DbSet<mz_pset_elqgrpid_mapping> mz_pset_elqgrpid_mapping { get; set; }

    public virtual DbSet<mz_receipt_payment_mode_rights> mz_receipt_payment_mode_rights { get; set; }

    public virtual DbSet<mz_receipt_payment_modes> mz_receipt_payment_modes { get; set; }

    public virtual DbSet<mz_student> mz_student { get; set; }

    public virtual DbSet<mz_student_ewallet> mz_student_ewallet { get; set; }

    public virtual DbSet<mz_student_fee_allotment> mz_student_fee_allotment { get; set; }

    public virtual DbSet<mz_student_fee_excluding_list> mz_student_fee_excluding_list { get; set; }

    public virtual DbSet<mz_student_fee_transaction> mz_student_fee_transaction { get; set; }

    public virtual DbSet<mz_student_feecategory> mz_student_feecategory { get; set; }

    public virtual DbSet<mz_student_feecategory_pset> mz_student_feecategory_pset { get; set; }

    public virtual DbSet<mz_student_feecategory_pset_monthly> mz_student_feecategory_pset_monthly {get;set;}

    public virtual DbSet<mz_student_receipt> mz_student_receipt { get; set; }

    public virtual DbSet<mzlm_leave_application> mzlm_leave_application { get; set; }

    public virtual DbSet<mzlm_leave_category> mzlm_leave_category { get; set; }

    public virtual DbSet<mzlm_leave_logs> mzlm_leave_logs { get; set; }

    public virtual DbSet<mzlm_leave_package> mzlm_leave_package { get; set; }

    public virtual DbSet<mzlm_leave_package_logs> mzlm_leave_package_logs { get; set; }

    public virtual DbSet<mzlm_leave_stage> mzlm_leave_stage { get; set; }

    public virtual DbSet<mzlm_leave_type> mzlm_leave_type { get; set; }

    public virtual DbSet<nisaab_alumni> nisaab_alumni { get; set; }

    public virtual DbSet<nisaab_classes> nisaab_classes { get; set; }

    public virtual DbSet<nisaab_classes_monitor> nisaab_classes_monitor { get; set; }

    public virtual DbSet<nisaab_jadwal> nisaab_jadwal { get; set; }

    public virtual DbSet<nisaab_periods> nisaab_periods { get; set; }

    public virtual DbSet<nisaab_student_logs> nisaab_student_logs { get; set; }

    public virtual DbSet<nisaabtalabat_results> nisaabtalabat_results { get; set; }

    public virtual DbSet<notification_email_template> notification_email_template { get; set; }

    public virtual DbSet<password_reset_requests> password_reset_requests { get; set; }

    public virtual DbSet<payroll_salary_packages> payroll_salary_packages { get; set; }

    public virtual DbSet<platform_button> platform_button { get; set; }

    public virtual DbSet<platform_module> platform_module { get; set; }

    public virtual DbSet<platform_page> platform_page { get; set; }

    public virtual DbSet<platform_role> platform_role { get; set; }

    public virtual DbSet<platform_user_module> platform_user_module { get; set; }

    public virtual DbSet<platform_user_role> platform_user_role { get; set; }

    public virtual DbSet<printableformat_report> printableformat_report { get; set; }

    public virtual DbSet<printableformat_report_rights> printableformat_report_rights { get; set; }

    public virtual DbSet<qism_al_tahfeez> qism_al_tahfeez { get; set; }

    public virtual DbSet<qism_al_tahfeez_api_logs> qism_al_tahfeez_api_logs { get; set; }

    public virtual DbSet<qism_al_tahfeez_login_logs> qism_al_tahfeez_login_logs { get; set; }

    public virtual DbSet<qism_al_tahfeez_role> qism_al_tahfeez_role { get; set; }

    public virtual DbSet<qism_al_tahfeez_role_module> qism_al_tahfeez_role_module { get; set; }

    public virtual DbSet<qism_al_tahfeez_role_right> qism_al_tahfeez_role_right { get; set; }

    public virtual DbSet<qism_al_tahfeez_user> qism_al_tahfeez_user { get; set; }

    public virtual DbSet<qism_al_tahfeez_user_deptvenue> qism_al_tahfeez_user_deptvenue { get; set; }

    public virtual DbSet<registrationform_dropdown_set> registrationform_dropdown_set { get; set; }

    public virtual DbSet<registrationform_programs> registrationform_programs { get; set; }

    public virtual DbSet<registrationform_subprograms> registrationform_subprograms { get; set; }

    public virtual DbSet<rights> rights { get; set; }

    public virtual DbSet<reports_names> reports_names { get; set; }
    public virtual DbSet<reports_rights> reports_rights { get; set; }

    public virtual DbSet<role> role { get; set; }

    public virtual DbSet<role_module> role_module { get; set; }

    public virtual DbSet<role_rights> role_rights { get; set; }

    public virtual DbSet<salary_allocation_gegorian> salary_allocation_gegorian { get; set; }

    public virtual DbSet<salary_allocation_hijri> salary_allocation_hijri { get; set; }

    public virtual DbSet<salary_generation_gegorgian> salary_generation_gegorgian { get; set; }

    public virtual DbSet<salary_generation_hijri> salary_generation_hijri { get; set; }

    public virtual DbSet<salary_querylogs> salary_querylogs { get; set; }

    public virtual DbSet<salary_type> salary_type { get; set; }

    public virtual DbSet<student_registration_rights> student_registration_rights { get; set; }

    public virtual DbSet<training_class> training_class { get; set; }

    public virtual DbSet<training_class_student> training_class_student { get; set; }

    public virtual DbSet<training_class_subject_teacher> training_class_subject_teacher { get; set; }

    public virtual DbSet<training_student_subject_marksheet> training_student_subject_marksheet { get; set; }

    public virtual DbSet<training_subject> training_subject { get; set; }

    public virtual DbSet<user> user { get; set; }

    public virtual DbSet<user_dept_venue_baseitem> user_dept_venue_baseitem { get; set; }

    public virtual DbSet<user_deptvenue> user_deptvenue { get; set; }

    public virtual DbSet<userdeptassociation> userdeptassociation { get; set; }

    public virtual DbSet<useritemassociation> useritemassociation { get; set; }

    public virtual DbSet<venue> venue { get; set; }

    public virtual DbSet<venue_transfer_approval> venue_transfer_approval { get; set; }

    public virtual DbSet<wafd_fieldofinterest> wafd_fieldofinterest { get; set; }

    public virtual DbSet<wafd_languageproficiency> wafd_languageproficiency { get; set; }

    public virtual DbSet<wafd_mahad_past_mawaze> wafd_mahad_past_mawaze { get; set; }

    public virtual DbSet<wafd_otheridara_mawaze> wafd_otheridara_mawaze { get; set; }

    public virtual DbSet<wafd_physicalfitness> wafd_physicalfitness { get; set; }

    public virtual DbSet<wafdulhuffaz_khidmat_mawaze> wafdulhuffaz_khidmat_mawaze { get; set; }

    public virtual DbSet<wafdprofile_dropdown_authoredcategory> wafdprofile_dropdown_authoredcategory { get; set; }

    public virtual DbSet<wafdprofile_dropdown_category> wafdprofile_dropdown_category { get; set; }

    public virtual DbSet<wafdprofile_dropdown_degree> wafdprofile_dropdown_degree { get; set; }

    public virtual DbSet<wafdprofile_dropdown_fieldofinterest> wafdprofile_dropdown_fieldofinterest { get; set; }

    public virtual DbSet<wafdprofile_dropdown_mode> wafdprofile_dropdown_mode { get; set; }

    public virtual DbSet<wafdprofile_dropdown_titlecategory> wafdprofile_dropdown_titlecategory { get; set; }

    public virtual DbSet<wafdprofile_dropdown_workshopcategory> wafdprofile_dropdown_workshopcategory { get; set; }

    public virtual DbSet<wafdprofile_english_assessment> wafdprofile_english_assessment { get; set; }

    public virtual DbSet<wafdprofile_maqaraat_data> wafdprofile_maqaraat_data { get; set; }

    public virtual DbSet<wafdprofile_maqaraat_session> wafdprofile_maqaraat_session { get; set; }

    public virtual DbSet<wafdprofile_maqaraat_teacher> wafdprofile_maqaraat_teacher { get; set; }

    public virtual DbSet<wafdprofile_qualification_new> wafdprofile_qualification_new { get; set; }

    public virtual DbSet<wafdprofile_qualification_stage_degree> wafdprofile_qualification_stage_degree { get; set; }

    public virtual DbSet<wafdprofile_workshop_data> wafdprofile_workshop_data { get; set; }

    public virtual DbSet<wafdprofile_workshops_category_subcategory> wafdprofile_workshops_category_subcategory { get; set; }

    public virtual DbSet<yellowreceipt> yellowreceipt { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                var isRequired = property.GetAnnotations()
                                         .Any(a => a.Name == "RequiredAttribute");

                if (property.ClrType == typeof(string) && !property.IsKey() && !isRequired)
                {
                    property.IsNullable = true;
                }
            }
        }

        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<acedemicyear_data>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<azwaaj_minentry>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.its).WithMany(p => p.azwaaj_minentry)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_azwaaj_entry_kg_vf");

            entity.HasOne(d => d.deptVenue).WithMany(p => p.azwaaj_minentry).HasConstraintName("fk_minentry_deptVenue");
            entity.HasOne(d => d.salaryType).WithMany(p => p.azwaaj_minentry).HasConstraintName("fk_minentry_salaryType");
        });

        modelBuilder.Entity<bmi_data>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<bankmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<current_counter>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<currency_converter_new>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<contenttypewithextention_data>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<campfasal_kutub>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<department>(entity =>
        {
            entity.HasKey(e => e.deptId).HasName("PRIMARY");
        });

        modelBuilder.Entity<branch_user>(entity =>
        {
            entity.HasKey(e => e.itsId).HasName("PRIMARY");

            entity.Property(e => e.itsId).ValueGeneratedNever();

            entity.HasOne(d => d.its)
                .WithOne(p => p.branch_user)
                .HasConstraintName("fk_branch_user_employee");

            entity.HasMany(d => d.deptVenue)
                .WithMany(p => p.user)
                .UsingEntity<qism_al_tahfeez_user_deptvenue>(
                    j => j
                        .HasOne(pt => pt.deptVenue)
                        .WithMany(t => t.branchuser_deptvenue)
                        .HasForeignKey(pt => pt.deptVenueId)
                        .HasConstraintName("fk_deptvenue_qism_user"),
                    j => j
                        .HasOne(pt => pt.branch_user)
                        .WithMany(t => t.branchuser_deptvenue)
                        .HasForeignKey(pt => pt.userId)
                        .HasConstraintName("fk_branch_user_qism_deptvenue"),
                    j =>
                    {
                        j.HasKey(t => new { t.userId, t.deptVenueId }).HasName("PRIMARY");
                        j.HasIndex(t => t.deptVenueId).HasDatabaseName("fk_deptvenue_qism_user_idx");
                    });

            entity.HasMany(d => d.pset)
                .WithMany(p => p.user)
                .UsingEntity<Dictionary<string, object>>(
                    "qism_al_tahfeez_user_pset",
                    r => r.HasOne<registrationform_dropdown_set>().WithMany()
                        .HasForeignKey("psetId")
                        .HasConstraintName("fk_branch_user_pset_p"),
                    l => l.HasOne<branch_user>().WithMany()
                        .HasForeignKey("userId")
                        .HasConstraintName("fk_branch_user_pset_b"),
                    j =>
                    {
                        j.HasKey("userId", "psetId").HasName("PRIMARY");
                        j.HasIndex(new[] { "psetId" }, "fk_branch_user_pset_p_idx");
                    });

            entity.HasMany(d => d.venues)
                .WithMany(p => p.branch_users)
                .UsingEntity<Dictionary<string, object>>(
                    "qism_al_tahfeez_user_venue",
                    r => r.HasOne<venue>()
                        .WithMany()
                        .HasForeignKey("venueId")
                        .HasConstraintName("fk_qism_al_tahfeez_user_venue"),
                    l => l.HasOne<branch_user>()
                        .WithMany()
                        .HasForeignKey("branchUserId")
                        .HasConstraintName("fk_qism_al_tahfeez_branch_user"),
                    j =>
                    {
                        j.HasKey("branchUserId", "venueId").HasName("PRIMARY");
                        j.IndexerProperty<int>("branchUserId").HasColumnType("int(11)");
                        j.IndexerProperty<int>("venueId").HasColumnType("int(11)");
                    });
        });

        modelBuilder.Entity<dept_venue>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.dept)
                .WithMany(p => p.dept_venue)
                .HasForeignKey(d => d.deptId)
                .HasConstraintName("fk_dept_venue_deptid");

            entity.HasOne(d => d.masterDept)
                .WithMany(p => p.dept_venue)
                .HasForeignKey(d => d.masterDeptId)
                .HasConstraintName("fk_dept_venue_masterdid");

            entity.HasOne(d => d.qism)
                .WithMany(p => p.dept_venue)
                .HasForeignKey(d => d.qismId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_dept_venue_qism_al_tahfeez");

            entity.HasMany(d => d.user)
                .WithMany(p => p.deptVenue)
                .UsingEntity<qism_al_tahfeez_user_deptvenue>(
                    j => j
                        .HasOne(pt => pt.branch_user)
                        .WithMany(t => t.branchuser_deptvenue)
                        .HasForeignKey(pt => pt.userId)
                        .HasConstraintName("fk_branch_user_qism_deptvenue"),
                    j => j
                        .HasOne(pt => pt.deptVenue)
                        .WithMany(t => t.branchuser_deptvenue)
                        .HasForeignKey(pt => pt.deptVenueId)
                        .HasConstraintName("fk_deptvenue_qism_user"),
                    j =>
                    {
                        j.HasKey(t => new { t.userId, t.deptVenueId }).HasName("PRIMARY");
                        j.HasIndex(t => t.deptVenueId).HasDatabaseName("fk_deptvenue_qism_user_idx");
                    });

            entity.HasOne(d => d.venue)
                .WithMany(p => p.dept_venue)
                .HasForeignKey(d => d.venueId)
                .HasConstraintName("fk_dept_venue_venue");
        });

        modelBuilder.Entity<qism_al_tahfeez_user_deptvenue>(entity =>
        {
            entity.HasKey(e => new { e.userId, e.deptVenueId }).HasName("PRIMARY");

            entity.HasIndex(e => e.deptVenueId).HasDatabaseName("fk_deptvenue_qism_user_idx");

            entity.Property(e => e.userId).HasColumnType("int(11)");
            entity.Property(e => e.deptVenueId).HasColumnType("int(11)");

            entity.HasOne(e => e.branch_user)
                .WithMany(b => b.branchuser_deptvenue)
                .HasForeignKey(e => e.userId)
                .HasConstraintName("fk_branch_user_qism_deptvenue");

            entity.HasOne(e => e.deptVenue)
                .WithMany(d => d.branchuser_deptvenue)
                .HasForeignKey(e => e.deptVenueId)
                .HasConstraintName("fk_deptvenue_qism_user");
        });

        modelBuilder.Entity<dept_venue_baseitem>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.hasItemBlock).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.baseItem).WithMany(p => p.dept_venue_baseitem)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_deptvenue_procurement_baseitem");

            entity.HasOne(d => d.deptVenue).WithMany(p => p.dept_venue_baseitem)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_dept_venue_baseitem_dept_venue");
        });

        modelBuilder.Entity<dropdown_dataset_header>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<dropdown_dataset_options>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.header).WithMany(p => p.dropdown_dataset_options)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_options_header_id");
        });

        modelBuilder.Entity<edit_table_column_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.editedBy).WithMany(p => p.edit_table_column_logs_editted_by).HasConstraintName("fk_edit_table_column_logs");
        });

        modelBuilder.Entity<employee_academic_details>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.its).WithOne(p => p.employee_academic_details).HasConstraintName("itsId");
        });

        modelBuilder.Entity<employee_bank_details>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.employee).WithMany(p => p.employee_bank_details).HasConstraintName("employeeIts");
        });

        modelBuilder.Entity<employee_dept_salary>(entity =>
        {
            entity.HasKey(e => new { e.itsId, e.deptVenueId, e.salaryTypeId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.HasOne(d => d.deptVenue).WithMany(p => p.employee_dept_salary).HasConstraintName("deptVenueSalaryId");

            entity.HasOne(d => d.its).WithMany(p => p.employee_dept_salary).HasConstraintName("empSalaryItsId");

            entity.HasOne(d => d.salaryType).WithMany(p => p.employee_dept_salary).HasConstraintName("salaryTypeId");
        });

        modelBuilder.Entity<employee_e_attendence>(entity =>
        {
            entity.HasKey(e => new { e.itsId, e.date })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasOne(d => d.its).WithMany(p => p.employee_e_attendence).HasConstraintName("fk_employee_attendence");
        });

        modelBuilder.Entity<employee_family_details>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.its).WithOne(p => p.employee_family_details).HasConstraintName("empId");
        });

        modelBuilder.Entity<employee_khidmat_details>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.its).WithMany(p => p.employee_khidmat_details).HasConstraintName("khidmatguzarIts");
        });

        modelBuilder.Entity<employee_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.its).WithMany(p => p.employee_logs).HasConstraintName("targetId");
        });

        modelBuilder.Entity<employee_passport_details>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.its).WithMany(p => p.employee_passport_details).HasConstraintName("citizenIts");
        });

        modelBuilder.Entity<employee_salary>(entity =>
        {
            entity.HasKey(e => e.itsId).HasName("PRIMARY");

            entity.Property(e => e.itsId).ValueGeneratedNever();

            entity.HasOne(d => d.its).WithOne(p => p.employee_salary).HasConstraintName("empSalary");
        });

        modelBuilder.Entity<enayat_medical_billentry>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            //entity.HasOne(d => d.its).WithMany(p => p.enayat_medical_billentry).HasConstraintName("fk_enayat_medical_billentry");
        });

        modelBuilder.Entity<enayat_scholarship_billentry>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            //entity.HasOne(d => d.its).WithMany(p => p.enayat_medical_billentry).HasConstraintName("fk_enayat_medical_billentry");
        });

        modelBuilder.Entity<export_category>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<export_type>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<export_type_displayheader>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<fitness_activity>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<global_constant>(entity =>
        {
            entity.HasKey(e => e.key).HasName("PRIMARY");
        });

        modelBuilder.Entity<hijri_calender>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<hijri_months>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<holiday_allocation>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.deptVenue).WithMany(p => p.holiday_allocation).HasConstraintName("fk_holiday_allocation_deptvenue");

            entity.HasOne(d => d.holiday).WithMany(p => p.holiday_allocation).HasConstraintName("fk_holiday_allocation_calender");
        });

        modelBuilder.Entity<holiday_calender>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<holiday_hijri_miqaat>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<ikhtibaar>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<ikhtibaar_marksheet>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.ikhtibaar).WithMany(p => p.ikhtibaar_marksheet).HasConstraintName("fk_ikhtibaar_marksheet");
        });

        modelBuilder.Entity<ikhtibaar_questionnaire>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.weightage).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.ikhtibaar).WithMany(p => p.ikhtibaar_questionnaire).HasConstraintName("fk_questionnaire_ikhtebaar");
        });

        modelBuilder.Entity<kg_faimalydetails>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<kg_faimalydetails_its>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<kg_identitycards>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<kg_self_assessment>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<kg_venue_transfer_history>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.its).WithMany(p => p.kg_venue_transfer_history).HasConstraintName("fk_kg_venue_tranfer_history_its");

        });

        modelBuilder.Entity<kg_venue_worktype>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<kg_worktype>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<khidmat_guzaar>(entity =>
        {
            entity.HasKey(e => e.itsId).HasName("PRIMARY");

            entity.ToTable(tb => tb.HasComment(""));

            entity.Property(e => e.itsId).ValueGeneratedNever();
            entity.Property(e => e.gender).HasDefaultValueSql("''");

        });

        modelBuilder.Entity<masterdepartment>(entity =>
        {
            entity.HasKey(e => e.masterDeptId).HasName("PRIMARY");
        });

        modelBuilder.Entity<module>(entity =>
        {
            entity.HasKey(e => e.moduleId).HasName("PRIMARY");
        });

        modelBuilder.Entity<module_rights>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_bank_transaction>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_bill_item>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.bill).WithMany(p => p.mz_expense_bill_item)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_bill_item_bill_master");

            entity.HasOne(d => d.item).WithMany(p => p.mz_expense_bill_item)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_bill_item_bill_item_master");
        });

        modelBuilder.Entity<mz_expense_bill_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.bill).WithMany(p => p.mz_expense_bill_logs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_bill_logs_bill_master");
        });

        modelBuilder.Entity<mz_expense_bill_master>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_bills_package>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_budget_araz>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.amountPerUom).HasDefaultValueSql("'1'");
            entity.Property(e => e.quantity).HasDefaultValueSql("'1'");
            entity.Property(e => e.stage).HasDefaultValueSql("'Initiated'");

            entity.HasOne(d => d.baseItem).WithMany(p => p.mz_expense_budget_araz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_budget_araz_baseItem");

            entity.HasOne(d => d.deptVenue).WithMany(p => p.mz_expense_budget_araz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_budget_araz_deptvenue");

            entity.HasOne(d => d.item).WithMany(p => p.mz_expense_budget_araz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_budget_araz_items");
        });

        modelBuilder.Entity<mz_expense_budget_araz_transfer_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.fromAraz).WithMany(p => p.mz_expense_budget_araz_transfer_logsfromAraz).HasConstraintName("fk_budget_araz_from");

            entity.HasOne(d => d.toAraz).WithMany(p => p.mz_expense_budget_araz_transfer_logstoAraz).HasConstraintName("fk_budget_araz_to");
        });

        modelBuilder.Entity<mz_expense_budget_issue_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.isConcerning).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.budgetAraz).WithMany(p => p.mz_expense_budget_issue_logs).HasConstraintName("fk_budget_issue_araz");
        });

        modelBuilder.Entity<mz_expense_budget_smart_goals>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.stage).HasDefaultValueSql("'Initiated'");

            entity.HasOne(d => d.deptVenue).WithMany(p => p.mz_expense_budget_smart_goals).HasConstraintName("fk_budget_smart_deptvenue");

            entity.HasOne(d => d.its).WithMany(p => p.mz_expense_budget_smart_goals).HasConstraintName("fk_budget_smart_kg");
        });

        modelBuilder.Entity<mz_expense_budget_smart_issue_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.isConcerning).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.smartGoal).WithMany(p => p.mz_expense_budget_smart_issue_logs).HasConstraintName("fk_budget_smart_issue_araz");
        });

        modelBuilder.Entity<mz_expense_budget_transfer_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_deptvenue_cash_wallet>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_estimate_student>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.duration).HasDefaultValueSql("'12'");
            entity.Property(e => e.stage).HasDefaultValueSql("'Initiated'");
        });

        modelBuilder.Entity<mz_expense_online_payment_users>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_procurement_baseitem>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_procurement_item>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasMany(d => d.baseItem).WithMany(p => p.item)
                .UsingEntity<Dictionary<string, object>>(
                    "mz_expense_procurement_item_baseitem",
                    r => r.HasOne<mz_expense_procurement_baseitem>().WithMany()
                        .HasForeignKey("baseItemId")
                        .HasConstraintName("fk_item_baseitem"),
                    l => l.HasOne<mz_expense_procurement_item>().WithMany()
                        .HasForeignKey("itemId")
                        .HasConstraintName("fk_baseitem_item"),
                    j =>
                    {
                        j.HasKey("itemId", "baseItemId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.HasIndex(new[] { "baseItemId" }, "fk_item_baseitem_idx");
                        j.IndexerProperty<int>("itemId").HasColumnType("int(11)");
                        j.IndexerProperty<int>("baseItemId").HasColumnType("int(11)");
                    });
        });

        //modelBuilder.Entity<mz_expense_procurement_item_baseitem>(entity =>
        //{
        //    entity.ToTable("mz_expense_procurement_item_baseitem");

        //    entity.HasKey(e => new { e.itemId, e.baseItemId }).HasName("PRIMARY");

        //    entity.Property(e => e.itemId).HasColumnType("int(11)");
        //    entity.Property(e => e.baseItemId).HasColumnType("int(11)");

        //    entity.HasOne<mz_expense_procurement_item>()
        //        .WithMany(i => i.ItemBaseItems) // Add this navigation in mz_expense_procurement_item
        //        .HasForeignKey(e => e.itemId)
        //        .HasConstraintName("fk_baseitem_item");

        //    entity.HasOne<mz_expense_procurement_baseitem>()
        //        .WithMany(b => b.ItemBaseItems) // Add this navigation in mz_expense_procurement_baseitem
        //        .HasForeignKey(e => e.baseItemId)
        //        .HasConstraintName("fk_item_baseitem");
        //});

        modelBuilder.Entity<mz_expense_sanctioned_budget>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_student_budget_issue_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.isConcerning).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.estimateStudent).WithMany(p => p.mz_expense_student_budget_issue_logs).HasConstraintName("fk_student_budget_issue_araz");
        });

        modelBuilder.Entity<mz_expense_vendor_master>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_vendor_payment>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_vendor_transaction>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_expense_vendor_wallet>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_faculty_loginlogs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_fee_collection_center>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_kg_wajebaat_araz>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable(tb => tb.HasComment("		"));

            entity.Property(e => e.stage).HasDefaultValueSql("'Initialized'");
            entity.Property(e => e.wajebaatType).HasDefaultValueSql("'Wajebaat Niyat'");
        });

        modelBuilder.Entity<mz_loginlogs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_off_module_exception>(entity =>
        {
            entity.HasKey(e => new { e.moduleId, e.itsId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasOne(d => d.module).WithMany(p => p.mz_off_module_exception).HasConstraintName("fk_module_on_off_id");
        });

        modelBuilder.Entity<mz_on_off_modules>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_pset_elqgrpid_mapping>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_receipt_payment_mode_rights>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_receipt_payment_modes>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_student>(entity =>
        {
            entity.HasKey(e => e.mz_id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_student_ewallet>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_student_fee_allotment>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_student_fee_excluding_list>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_student_fee_transaction>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_student_feecategory>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_student_feecategory_pset>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mz_student_receipt>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mzlm_leave_application>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.shiftCount).HasDefaultValueSql("'1'");
        });

        modelBuilder.Entity<mzlm_leave_category>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.consicutiveLimit).HasDefaultValueSql("'1'");
            entity.Property(e => e.isDeductable).HasDefaultValueSql("'1'");
            entity.Property(e => e.isRepeated).HasDefaultValueSql("'1'");
            entity.Property(e => e.maxAllowed).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.leaveType).WithMany(p => p.mzlm_leave_category).HasConstraintName("fk_leave_type_category");
        });

        modelBuilder.Entity<mzlm_leave_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.createdByNavigation).WithMany(p => p.mzlm_leave_logs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_mzlm_leave_updateby");

            entity.HasOne(d => d.leave).WithMany(p => p.mzlm_leave_logs).HasConstraintName("fk_mzlm_leave_application_log");

            entity.HasOne(d => d.stage).WithMany(p => p.mzlm_leave_logs).HasConstraintName("fk_mzlm_stage_id_leave_log");
        });

        modelBuilder.Entity<mzlm_leave_package>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mzlm_leave_package_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mzlm_leave_stage>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<mzlm_leave_type>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<nisaab_alumni>(entity =>
        {
            entity.HasKey(e => e.itsId).HasName("PRIMARY");

            entity.Property(e => e.itsId).ValueGeneratedNever();

            entity.HasOne(d => d.its).WithOne(p => p.nisaab_alumni)
                .HasPrincipalKey<mz_student>(p => p.itsID)
                .HasForeignKey<nisaab_alumni>(d => d.itsId)
                .HasConstraintName("fk_alumni_mz_student");

            entity.HasOne(d => d.kgItsNavigation).WithMany(p => p.nisaab_alumni)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_alumni_mz_kg");
        });

        modelBuilder.Entity<nisaab_classes>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<nisaab_classes_monitor>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<nisaab_jadwal>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<nisaab_periods>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<nisaab_student_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<nisaabtalabat_results>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<notification_email_template>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<password_reset_requests>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");
        });

        modelBuilder.Entity<payroll_salary_packages>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<platform_button>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<platform_module>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.button).WithMany(p => p.platform_module).HasConstraintName("fk_platform_module_button");

            entity.HasOne(d => d.page).WithMany(p => p.platform_module).HasConstraintName("fk_platform_module_page");
        });

        modelBuilder.Entity<platform_page>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<platform_role>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasMany(d => d.mainRole).WithMany(p => p.subRole)
                .UsingEntity<Dictionary<string, object>>(
                    "platform_menu_map",
                    r => r.HasOne<platform_role>().WithMany()
                        .HasForeignKey("mainRole")
                        .HasConstraintName("fk_main_platform_role"),
                    l => l.HasOne<platform_role>().WithMany()
                        .HasForeignKey("subRole")
                        .HasConstraintName("fk_sub_platform_role"),
                    j =>
                    {
                        j.HasKey("mainRole", "subRole")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.HasIndex(new[] { "subRole" }, "fk_sub_platform_role_idx");
                        j.IndexerProperty<int>("mainRole").HasColumnType("int(11)");
                        j.IndexerProperty<int>("subRole").HasColumnType("int(11)");
                    });

            entity.HasMany(d => d.module).WithMany(p => p.role)
                .UsingEntity<Dictionary<string, object>>(
                    "platform_role_module",
                    r => r.HasOne<platform_module>().WithMany()
                        .HasForeignKey("moduleId")
                        .HasConstraintName("fk_platform_role_module_mid"),
                    l => l.HasOne<platform_role>().WithMany()
                        .HasForeignKey("roleId")
                        .HasConstraintName("fk_paltform_role_module_r"),
                    j =>
                    {
                        j.HasKey("roleId", "moduleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.HasIndex(new[] { "moduleId" }, "fk_platform_role_module_mid_idx");
                        j.IndexerProperty<int>("roleId").HasColumnType("int(11)");
                        j.IndexerProperty<int>("moduleId").HasColumnType("int(11)");
                    });

            entity.HasMany(d => d.subRole).WithMany(p => p.mainRole)
                .UsingEntity<Dictionary<string, object>>(
                    "platform_menu_map",
                    r => r.HasOne<platform_role>().WithMany()
                        .HasForeignKey("subRole")
                        .HasConstraintName("fk_sub_platform_role"),
                    l => l.HasOne<platform_role>().WithMany()
                        .HasForeignKey("mainRole")
                        .HasConstraintName("fk_main_platform_role"),
                    j =>
                    {
                        j.HasKey("mainRole", "subRole")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.HasIndex(new[] { "subRole" }, "fk_sub_platform_role_idx");
                        j.IndexerProperty<int>("mainRole").HasColumnType("int(11)");
                        j.IndexerProperty<int>("subRole").HasColumnType("int(11)");
                    });
        });

        modelBuilder.Entity<platform_user_module>(entity =>
        {
            entity.HasKey(e => new { e.userId, e.moduleId, e.qismId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.HasOne(d => d.module).WithMany(p => p.platform_user_module).HasConstraintName("fk_platform_user_module_mid");

            entity.HasOne(d => d.qism).WithMany(p => p.platform_user_module).HasConstraintName("fk_platform_user_module_qism");

            entity.HasOne(d => d.user).WithMany(p => p.platform_user_module).HasConstraintName("fk_paltform_user_module_kg");
        });

        modelBuilder.Entity<platform_user_role>(entity =>
        {
            entity.HasKey(e => new { e.userId, e.roleId, e.qismId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.HasOne(d => d.qism).WithMany(p => p.platform_user_role).HasConstraintName("fk_platform_user_role_qism");

            entity.HasOne(d => d.role).WithMany(p => p.platform_user_role).HasConstraintName("fk_platform_user_role_mid");

            entity.HasOne(d => d.user).WithMany(p => p.platform_user_role).HasConstraintName("fk_paltform_user_role_kg");
        });

        modelBuilder.Entity<printableformat_report>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<printableformat_report_rights>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<qism_al_tahfeez>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.its).WithOne(p => p.qism_al_tahfeez).HasConstraintName("fk_qism_al_tahfeez_branch_user");
        });

        modelBuilder.Entity<qism_al_tahfeez_api_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<qism_al_tahfeez_login_logs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<qism_al_tahfeez_role>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<qism_al_tahfeez_role_module>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<qism_al_tahfeez_role_right>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<qism_al_tahfeez_user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<registrationform_dropdown_set>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.deptVenue).WithMany(p => p.registrationform_dropdown_set).HasConstraintName("fk_pset_dept_venue");

            entity.HasOne(d => d.program).WithMany(p => p.registrationform_dropdown_set).HasConstraintName("fk_pset_program");

            entity.HasOne(d => d.qism).WithMany(p => p.registrationform_dropdown_set).HasConstraintName("fk_pset_qism_al_tahfeez");

            entity.HasOne(d => d.subprogram).WithMany(p => p.registrationform_dropdown_set).HasForeignKey(d => d.subprogramId).HasConstraintName("fk_pset_sub_program");

            entity.HasOne(d => d.venue).WithMany(p => p.registrationform_dropdown_set).HasConstraintName("fk_pset_venue");
        });

        modelBuilder.Entity<registrationform_programs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<registrationform_subprograms>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<rights>(entity =>
        {
            entity.HasKey(e => e.rightsId).HasName("PRIMARY");
        });

        modelBuilder.Entity<reports_names>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<reports_rights>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<role>(entity =>
        {
            entity.HasKey(e => e.roleId).HasName("PRIMARY");
        });

        modelBuilder.Entity<role_module>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<role_rights>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<salary_allocation_gegorian>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.currency).HasDefaultValueSql("'INR'");

            entity.HasOne(d => d.its).WithMany(p => p.salary_allocation_gegorian).HasConstraintName("fk_salary_allocation_g_kh");

            entity.HasOne(d => d.package).WithMany(p => p.salary_allocation_gegorian)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_salary_allocation_g_exp_package");
        });

        modelBuilder.Entity<salary_allocation_hijri>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.currency).HasDefaultValueSql("'INR'");

            entity.HasOne(d => d.its).WithMany(p => p.salary_allocation_hijri).HasConstraintName("fk_salary_allocation_h_kh");

            entity.HasOne(d => d.package).WithMany(p => p.salary_allocation_hijri)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_salary_allocation_exp_package");
        });

        modelBuilder.Entity<salary_generation_gegorgian>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.allocation).WithMany(p => p.salary_generation_gegorgian)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_salary_gen_gegorian_allocation");

            entity.HasOne(d => d.deptVenue).WithMany(p => p.salary_generation_gegorgian).HasConstraintName("fk_salary_gen_gegorgian_dept");

            entity.HasOne(d => d.its).WithMany(p => p.salary_generation_gegorgian).HasConstraintName("empengsalarygeneration");

            entity.HasOne(d => d.salaryTypeNavigation).WithMany(p => p.salary_generation_gegorgian).HasConstraintName("fk_salary_gen_g_salary_type");
        });

        modelBuilder.Entity<salary_generation_hijri>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.allocation).WithMany(p => p.salary_generation_hijri).HasConstraintName("fk_salary_gen_hijri_allocation");

            entity.HasOne(d => d.deptVenue).WithMany(p => p.salary_generation_hijri)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sdv");

            entity.HasOne(d => d.its).WithMany(p => p.salary_generation_hijri).HasConstraintName("empsalarygeneration");

            entity.HasOne(d => d.salaryTypeNavigation).WithMany(p => p.salary_generation_hijri)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_salary_gen_h_salary_type");
        });

        modelBuilder.Entity<salary_querylogs>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<salary_type>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<student_registration_rights>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<training_class>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.masoolItsNavigation).WithMany(p => p.training_class)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_class_masool_kg");
        });

        modelBuilder.Entity<training_class_student>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d._class).WithMany(p => p.training_class_student).HasConstraintName("fk_class_student_training_class");

            entity.HasOne(d => d.studentITSNavigation).WithMany(p => p.training_class_student).HasConstraintName("fk_class_student_kg_student");
        });

        modelBuilder.Entity<training_class_subject_teacher>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.status).HasDefaultValueSql("'Not Active'");

            entity.HasOne(d => d._class).WithMany(p => p.training_class_subject_teacher).HasConstraintName("fk_cst_class");

            entity.HasOne(d => d.subject).WithMany(p => p.training_class_subject_teacher).HasConstraintName("fk_cst_subject");

            entity.HasOne(d => d.teacherITSNavigation).WithMany(p => p.training_class_subject_teacher).HasConstraintName("fk_cst_teacher");
        });

        modelBuilder.Entity<training_student_subject_marksheet>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.status).HasDefaultValueSql("'Not Atempted'");

            entity.HasOne(d => d.cst).WithMany(p => p.training_student_subject_marksheet).HasConstraintName("fk_student_marksheet_subject_cst");

            entity.HasOne(d => d.gradedByNavigation).WithMany(p => p.training_student_subject_marksheetgradedByNavigation)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_student_marksheet_kg");

            entity.HasOne(d => d.studentITSNavigation).WithMany(p => p.training_student_subject_marksheetstudentITSNavigation).HasConstraintName("fk_student_marksheet_student");
        });

        modelBuilder.Entity<training_subject>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.status).HasDefaultValueSql("'Not Active'");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<user_dept_venue_baseitem>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<user_deptvenue>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<userdeptassociation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<useritemassociation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<venue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.qism).WithMany(p => p.venue)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_venue_qism");
        });

        modelBuilder.Entity<venue_transfer_approval>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.venue).WithMany(p => p.venue_transfer_approval_current_venue).HasConstraintName("fk_venue_transfer_approval_current_venue");

            entity.HasOne(e => e.employee).WithMany(p => p.venue_transfer_approval_emplyee).HasConstraintName("fk_venue_transfer_approval_khidmatguzaar");
            entity.HasOne(e => e.requestedBy).WithMany(p => p.venue_transfer_approval_requested_by).HasConstraintName("fk_venue_transfer_approval_khidmatguzaar1");
            entity.HasOne(e => e.reviewedBy).WithMany(p => p.venue_transfer_approval_reviewed_by).HasConstraintName("fk_venue_transfer_approval_khidmatguzaar2");
        });

        modelBuilder.Entity<wafd_fieldofinterest>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafd_languageproficiency>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafd_mahad_past_mawaze>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafd_otheridara_mawaze>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafd_physicalfitness>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdulhuffaz_khidmat_mawaze>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdprofile_dropdown_authoredcategory>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdprofile_dropdown_category>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdprofile_dropdown_degree>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdprofile_dropdown_fieldofinterest>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdprofile_dropdown_mode>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdprofile_dropdown_titlecategory>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdprofile_dropdown_workshopcategory>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdprofile_english_assessment>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdprofile_maqaraat_data>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.session).WithMany(p => p.wafdprofile_maqaraat_data).HasConstraintName("fk_maqaraat_data_maqaraat_session");

            entity.HasOne(d => d.studentIts).WithMany(p => p.wafdprofile_maqaraat_data).HasConstraintName("fk_maqaarat_data_kg");
        });

        modelBuilder.Entity<wafdprofile_maqaraat_session>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.teacherIts).WithMany(p => p.wafdprofile_maqaraat_session).HasConstraintName("fk_maqaraat_session_kg");
        });

        modelBuilder.Entity<wafdprofile_maqaraat_teacher>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.its).WithMany(p => p.wafdprofile_maqaraat_teacher)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_maqaraat_teacher_kg");
        });

        modelBuilder.Entity<wafdprofile_qualification_new>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.its).WithMany(p => p.qualification).HasConstraintName("fk_qualification_kg");
        });

        modelBuilder.Entity<wafdprofile_qualification_stage_degree>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdprofile_workshop_data>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<wafdprofile_workshops_category_subcategory>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<yellowreceipt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.status).HasDefaultValueSql("'Pending'");
        });

        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
