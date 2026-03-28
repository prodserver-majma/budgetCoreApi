using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

/// <summary>
///
/// </summary>
[Index("mauze", Name = "fk_venue_khidmatguzaar_idx")]
public partial class khidmat_guzaar
{
    [Column(TypeName = "int(11)")]
    public int? id { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? photo { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? krNo { get; set; }        //to be removed

    [Key]
    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Required]
    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? fullName { get; set; }

    [Column(TypeName = "text")]
    public string? fullNameArabic { get; set; }

    [StringLength(45)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? c_codeMobile { get; set; }

    [Required]
    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? mobileNo { get; set; }

    [StringLength(45)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? c_codeWhatsapp { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? whatsappNo { get; set; }

    [Required]
    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? emailAddress { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? officialEmailAddress { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? watan { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? watanArabic { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? watanAdress { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? muqam { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? muqamArabic { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? dojGregorian { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? doeGregorian { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? dojHijri { get; set; }

    [Required]
    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? dobGregorian { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? dobHijri { get; set; }

    [StringLength(45)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? bloodGroup { get; set; }

    [Column(TypeName = "text")]
    public string? currentAddress { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? jaman { get; set; }       //to be removed

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? maritalStatus { get; set; }

    [Column(TypeName = "int(11)")]
    public int? mafsuhiyatYear { get; set; }

    public bool? activeStatus { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? nationality { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? its_idaras { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? its_preferredIdara { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? mz_idara { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? dawat_title { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? jamaat { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? jamiat { get; set; }

    [Column(TypeName = "int(11)")]
    public int? age { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? panCardNo { get; set; }      //to be removed

    [Column(TypeName = "int(11)")]
    public int? haddiyatYear { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? domicileParent { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? domicileAddressParents { get; set; }

    [StringLength(45)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? personalHouseStatus { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? personalHouseType { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? personalHouseArea { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? personalHouseAddress { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_general_ci")]
    public string? photoBase64 { get; set; }

    [Column(TypeName = "int(11)")]
    public int? workTypeId { get; set; }    //to be removed

    [Required]
    [StringLength(45)]
    public string? employeeType { get; set; }

    [StringLength(45)]
    public string? workType { get; set; }

    [StringLength(100)]
    public string? RfId { get; set; }

    [StringLength(100)]
    public string? EduQualification { get; set; }   //to be removed

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedOn { get; set; }

    [StringLength(100)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "int(11)")]
    public int? MzId { get; set; }     //to be removed

    [StringLength(100)]
    public string? OthDegree { get; set; }  //to be removed

    [StringLength(100)]
    public string? EduCompletion { get; set; }  //to be removed

    public bool? isMumin { get; set; }

    [StringLength(200)]
    public string? designation { get; set; }

    [StringLength(200)]
    public string? salaryCalender { get; set; }

    [StringLength(200)]
    public string? subDesignation { get; set; } //to be removed

    [StringLength(45)]
    public string? salaryCode { get; set; }  //to be removed

    [StringLength(45)]
    public string? password { get; set; }

    [StringLength(45)]
    public string category { get; set; }

    [Column(TypeName = "int(11)")]
    public int? batchId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? mauze { get; set; }

    [Required]
    [StringLength(1)]
    public string? gender { get; set; }


    [InverseProperty("its")]
    public virtual ICollection<azwaaj_minentry> azwaaj_minentry { get; set; } = new List<azwaaj_minentry>();

    [InverseProperty("its")]
    public virtual branch_user branch_user { get; set; }

    [InverseProperty("its")]
    public virtual employee_academic_details employee_academic_details { get; set; }

    [InverseProperty("employee")]
    public virtual ICollection<employee_bank_details> employee_bank_details { get; set; } = new List<employee_bank_details>();

    [InverseProperty("its")]
    public virtual ICollection<employee_dept_salary>? employee_dept_salary { get; set; } = new List<employee_dept_salary>();

    [InverseProperty("its")]
    public virtual ICollection<employee_e_attendence> employee_e_attendence { get; set; } = new List<employee_e_attendence>();

    [InverseProperty("its")]
    public virtual employee_family_details employee_family_details { get; set; }

    [InverseProperty("its")]
    public virtual ICollection<employee_khidmat_details> employee_khidmat_details { get; set; } = new List<employee_khidmat_details>();

    [InverseProperty("its")]
    public virtual ICollection<employee_logs> employee_logs { get; set; } = new List<employee_logs>();

    [InverseProperty("its")]
    public virtual ICollection<employee_passport_details> employee_passport_details { get; set; } = new List<employee_passport_details>();

    [InverseProperty("its")]
    public virtual employee_salary employee_salary { get; set; }

    [ForeignKey("mauze")]
    [InverseProperty("khidmat_guzaar")]
    public virtual venue mauzeNavigation { get; set; }

    [InverseProperty("its")]
    public virtual ICollection<mz_expense_budget_smart_goals> mz_expense_budget_smart_goals { get; set; } = new List<mz_expense_budget_smart_goals>();

    [InverseProperty("its")]
    public virtual ICollection<mzlm_leave_application> mzlm_leave_application { get; set; } = new List<mzlm_leave_application>();

    [InverseProperty("createdByNavigation")]
    public virtual ICollection<mzlm_leave_logs> mzlm_leave_logs { get; set; } = new List<mzlm_leave_logs>();

    [InverseProperty("createdByNavigation")]
    public virtual ICollection<mzlm_leave_package_logs> mzlm_leave_package_logs { get; set; } = new List<mzlm_leave_package_logs>();

    [InverseProperty("kgItsNavigation")]
    public virtual ICollection<nisaab_alumni> nisaab_alumni { get; set; } = new List<nisaab_alumni>();

    [InverseProperty("its")]
    public virtual ICollection<salary_allocation_gegorian> salary_allocation_gegorian { get; set; } = new List<salary_allocation_gegorian>();

    [InverseProperty("its")]
    public virtual ICollection<salary_allocation_hijri> salary_allocation_hijri { get; set; } = new List<salary_allocation_hijri>();

    [InverseProperty("its")]
    public virtual ICollection<salary_generation_gegorgian> salary_generation_gegorgian { get; set; } = new List<salary_generation_gegorgian>();

    [InverseProperty("its")]
    public virtual ICollection<salary_generation_hijri> salary_generation_hijri { get; set; } = new List<salary_generation_hijri>();

    [InverseProperty("masoolItsNavigation")]
    public virtual ICollection<training_class> training_class { get; set; } = new List<training_class>();

    [InverseProperty("studentITSNavigation")]
    public virtual ICollection<training_class_student> training_class_student { get; set; } = new List<training_class_student>();

    [InverseProperty("teacherITSNavigation")]
    public virtual ICollection<training_class_subject_teacher> training_class_subject_teacher { get; set; } = new List<training_class_subject_teacher>();

    [InverseProperty("gradedByNavigation")]
    public virtual ICollection<training_student_subject_marksheet> training_student_subject_marksheetgradedByNavigation { get; set; } = new List<training_student_subject_marksheet>();

    [InverseProperty("studentITSNavigation")]
    public virtual ICollection<training_student_subject_marksheet> training_student_subject_marksheetstudentITSNavigation { get; set; } = new List<training_student_subject_marksheet>();

    [InverseProperty("studentIts")]
    public virtual ICollection<wafdprofile_maqaraat_data> wafdprofile_maqaraat_data { get; set; } = new List<wafdprofile_maqaraat_data>();

    [InverseProperty("teacherIts")]
    public virtual ICollection<wafdprofile_maqaraat_session> wafdprofile_maqaraat_session { get; set; } = new List<wafdprofile_maqaraat_session>();

    [InverseProperty("its")]
    public virtual ICollection<wafdprofile_maqaraat_teacher> wafdprofile_maqaraat_teacher { get; set; } = new List<wafdprofile_maqaraat_teacher>();

    [InverseProperty("employee")]
    public virtual ICollection<venue_transfer_approval> venue_transfer_approval_emplyee { get; set; } = new List<venue_transfer_approval>();

    [InverseProperty("requestedBy")]
    public virtual ICollection<venue_transfer_approval> venue_transfer_approval_requested_by { get; set; } = new List<venue_transfer_approval>();

    [InverseProperty("reviewedBy")]
    public virtual ICollection<venue_transfer_approval> venue_transfer_approval_reviewed_by { get; set; } = new List<venue_transfer_approval>();

    [InverseProperty("editedBy")]
    public virtual ICollection<edit_table_column_logs> edit_table_column_logs_editted_by { get; set; } = new List<edit_table_column_logs>();

    [InverseProperty("employee")]
    public virtual ICollection<user_deptvenue> user_deptvenues { get; set; } = new List<user_deptvenue>();

    [InverseProperty("employee")]
    public virtual ICollection<user_venue> user_venues { get; set; } = new List<user_venue>();

    [InverseProperty("its")]
    public virtual ICollection<kg_venue_transfer_history> kg_venue_transfer_history { get; set; } = new List<kg_venue_transfer_history>();

    [InverseProperty("its")]
    public virtual ICollection<wafdprofile_qualification_new> qualification { get; set; } = new List<wafdprofile_qualification_new>();
}
