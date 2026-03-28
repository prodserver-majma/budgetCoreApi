using mahadalzahrawebapi.Mappings.Training;

namespace mahadalzahrawebapi.Mappings
{
    public class khidmat_guzaarDTO
    {
        public int id { get; set; }

        public string? photo { get; set; }

        public string? krNo { get; set; }

        public int itsId { get; set; }

        public string? fullName { get; set; }

        public string? fullNameArabic { get; set; }

        public string? c_codeMobile { get; set; }

        public string? mobileNo { get; set; }

        public string? c_codeWhatsapp { get; set; }

        public string? whatsappNo { get; set; }

        public string? emailAddress { get; set; }

        public string? officialEmailAddress { get; set; }

        public string? watan { get; set; }

        public string? watanArabic { get; set; }

        public string? watanAdress { get; set; }

        public string? muqam { get; set; }

        public string? muqamArabic { get; set; }

        public DateTime? dojGregorian { get; set; }
        public DateTime? doeGregorian { get; set; }

        public string? dojHijri { get; set; }

        public string? dobGregorian { get; set; }

        public string? dobHijri { get; set; }

        public string? bloodGroup { get; set; }

        public string? currentAddress { get; set; }

        public string? jaman { get; set; }

        public string? maritalStatus { get; set; }

        public int? mafsuhiyatYear { get; set; }

        public bool? activeStatus { get; set; }

        public string? nationality { get; set; }

        public string? its_idaras { get; set; }

        public string? its_preferredIdara { get; set; }

        public string? mz_idara { get; set; }

        public string? dawat_title { get; set; }

        public string? jamaat { get; set; }

        public string? jamiat { get; set; }

        public int? age { get; set; }

        public string? panCardNo { get; set; }

        public int? haddiyatYear { get; set; }

        public string? domicileParent { get; set; }

        public string? domicileAddressParents { get; set; }

        public string? personalHouseStatus { get; set; }

        public string? personalHouseType { get; set; }

        public string? personalHouseArea { get; set; }

        public string? personalHouseAddress { get; set; }

        public string? photoBase64 { get; set; }

        public int? workTypeId { get; set; }

        public string? employeeType { get; set; }

        public string? RfId { get; set; }

        public string? EduQualification { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public int? MzId { get; set; }

        public string? OthDegree { get; set; }

        public string? EduCompletion { get; set; }

        public bool? isMumin { get; set; }

        public string? designation { get; set; }
        public string? salaryCalender { get; set; }
        public string? subDesignation { get; set; }

        public string? salaryCode { get; set; }

        public int? mauze { get; set; }

        public virtual branch_user_dto branch_user { get; set; }

        public virtual ICollection<employee_academic_detail_dto> employee_academic_details { get; set; } = new List<employee_academic_detail_dto>();

        public virtual employee_bank_detail_dto employee_bank_detail { get; set; }

        public virtual ICollection<employee_dept_salary_dto> employee_dept_salaries { get; set; } = new List<employee_dept_salary_dto>();

        public virtual ICollection<employee_e_attendence_dto> employee_e_attendences { get; set; } = new List<employee_e_attendence_dto>();

        public virtual employee_family_detail_dto employee_family_detail { get; set; }

        public virtual ICollection<employee_khidmat_detail_dto> employee_khidmat_details { get; set; } = new List<employee_khidmat_detail_dto>();

        public virtual ICollection<employee_log_dto> employee_logs { get; set; } = new List<employee_log_dto>();

        public virtual ICollection<employee_passport_detail_dto> employee_passport_details { get; set; } = new List<employee_passport_detail_dto>();

        public virtual employee_salary_dto employee_salary { get; set; }

        public virtual venue_dto mauzeNavigation { get; set; }

        public virtual ICollection<mz_expense_budget_smart_goal_dto> mz_expense_budget_smart_goals { get; set; } = new List<mz_expense_budget_smart_goal_dto>();

        public virtual ICollection<nisaab_alumnus_dto> nisaab_alumni { get; set; } = new List<nisaab_alumnus_dto>();

        public virtual ICollection<salary_allocation_gegorian_dto> salary_allocation_gegorians { get; set; } = new List<salary_allocation_gegorian_dto>();

        public virtual ICollection<salary_allocation_hijri_dto> salary_allocation_hijris { get; set; } = new List<salary_allocation_hijri_dto>();

        public virtual ICollection<salary_generation_gegorgian_dto> salary_generation_gegorgians { get; set; } = new List<salary_generation_gegorgian_dto>();

        public virtual ICollection<salary_generation_hijri_dto> salary_generation_hijris { get; set; } = new List<salary_generation_hijri_dto>();

        public virtual ICollection<training_class_student_dto> training_class_students { get; set; } = new List<training_class_student_dto>();

        public virtual ICollection<training_class_subject_teacher_dto> training_class_subject_teachers { get; set; } = new List<training_class_subject_teacher_dto>();

        public virtual ICollection<training_class_dto> training_classes { get; set; } = new List<training_class_dto>();

        public virtual ICollection<training_student_subject_marksheet_dto> training_student_subject_marksheetgradedByNavigations { get; set; } = new List<training_student_subject_marksheet_dto>();

        public virtual ICollection<training_student_subject_marksheet_dto> training_student_subject_marksheetstudentITSNavigations { get; set; } = new List<training_student_subject_marksheet_dto>();

        public virtual ICollection<wafdprofile_maqaraat_datum_dto> wafdprofile_maqaraat_data { get; set; } = new List<wafdprofile_maqaraat_datum_dto>();

        public virtual ICollection<wafdprofile_maqaraat_session_dto> wafdprofile_maqaraat_sessions { get; set; } = new List<wafdprofile_maqaraat_session_dto>();

        //public virtual ICollection<wafdprofile_maqaraat_teacher> wafdprofile_maqaraat_teachers { get; set; } = new List<wafdprofile_maqaraat_teacher_dto>();
    }
}
