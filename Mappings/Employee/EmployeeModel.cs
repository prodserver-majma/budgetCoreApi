namespace mahadalzahrawebapi.Mappings
{
    public class EmployeeVenueHistoryModel
    {
        public int id { get; set; }
        public int itsId { get; set; }
        public string name { get; set; }
        public string pastVenueName { get; set; }
        public string newVenueName { get; set; }
        public int pastVenueId { get; set; }
        public int newVenueId { get; set; }
        public DateOnly transferDate { get; set; }
        public string editedByName { get; set; }
        public int editedBy { get; set; }
    }
    public class EmployeeExtraDetailsModel
    {
        public string? mauze { get; set; }
        public string? trainingDarajah { get; set; }
        public string? qismAlTahfeez { get; set; }
        public string? latestQualification { get; set; }
        public int khidmatDuration { get; set; }
        public string? khidmatDurationString { get; set; }
        public int netSalary { get; set; }
        public int estimatedSalary { get; set; }
        public int maxTimeBasedWazifa { get; set; }
        public int workingHours { get; set; }
        public string? salaryType { get; set; }
        public string? salaryCalender { get; set; }
        public string? status { get; set; }
        public string? idaraColor { get; set; }
        public bool? mauzeChanged { get; set; }

        public int? spouseITS { get; set; }
        public string? spouseName { get; set; }
        public int? childCount { get; set; }
    }
    public class EmployeeModel
    {
        public EmployeeBasicDetailsModel? basicDetails { get; set; }
        public EmployeePassportDetailsModel? passportDetails { get; set; }
        public EmployeeKidmatDetailsModel? khidmatDetails { get; set; }
        public List<EmployeeBankDetailsModel>? bankDetails { get; set; }
        public EmployeeAcademicDetailsModel? academicDetails { get; set; }
        public EmployeeSalaryDetailsModel? employeeSalary { get; set; }
        public EmployeeSelfAssessmentModel? selfAssessment { get; set; }
        public List<EmployeeDeptSalaryModel>? deptSalaries { get; set; }
        public EmployeeFamilyDetailsModel? familyDetails { get; set; }
        public EmployeeExtraDetailsModel? extraDetails { get; set; }
        public bool select { get; set; }
    }

    public class employee_model_dto
    {
        public EmployeeBasicDetailsModel basicDetails { get; set; }
        public employee_passport_detail_dto passportDetails { get; set; }
        public employee_khidmat_detail_dto khidmatDetails { get; set; }
        public employee_bank_detail_dto bankDetails { get; set; }
        public employee_academic_detail_dto academicDetails { get; set; }
        public employee_salary_dto employeeSalary { get; set; }
        public EmployeeSelfAssessmentModel selfAssessment { get; set; }
        public List<employee_dept_salary_dto> deptSalaries { get; set; }
        public employee_family_detail_dto familyDetails { get; set; }
        public EmployeeExtraDetailsModel extraDetails { get; set; }
        public bool select { get; set; }

    }

    public class KGIdentitycards
    {
        public int id { get; set; }

        public string? cardType { get; set; }

        public string? country { get; set; }

        public string? nameOnCard { get; set; }

        public string? cardNumber { get; set; }

        public int? itsId { get; set; }

        public string? attachment { get; set; }
        public string? frontAttachName { get; set; }

        public string? back_attachment { get; set; }
        public string? backAttachName { get; set; }
    }

    public class WafdMawazeModel
    {
        public int id { get; set; }
        public string khidmatMainType { get; set; }
        public string khidmatSubType { get; set; }
        public int? hijriYear { get; set; }
        public string mozeName { get; set; }
        public bool display { get; set; }
        public bool display_subType { get; set; }

        public int mainTypeCount { get; set; }
        public int subTypeCount { get; set; }

        public int rowSpan { get; set; }
        public int colSpan { get; set; }


        public int? itsId { get; set; }
    }

    public class fitnessActivity
    {
        public int? id { get; set; }
        public string? activityName { get; set; }
        public string? venue { get; set; }
        public int? hours { get; set; }
        public int? minutes { get; set; }
        public int? itsId { get; set; }
        public DateTime? createdOn { get; set; }
        public string? attachmentFile { get; set; }
        public string? attachmentFileName { get; set; }
        public string? routine { get; set; }
        public int? academicYear { get; set; }
    }

    public class WafdprofileQualification
    {
        public int id { get; set; }
        public int? itsid { get; set; }
        public string? country { get; set; }
        public string? mediumOfEducation { get; set; }
        public string? stage { get; set; }
        public string? degree { get; set; }
        public string? institutionName { get; set; }
        public string? status { get; set; }
        public string? pursuingYear { get; set; }
        public string? year { get; set; }
        public string? attachment { get; set; }
        public string? attachmentfilename { get; set; }
    }

    public class WajebaatArazModel
    {
        public int id { get; set; }

        public int? itsId { get; set; }

        public int? hijriYear { get; set; }

        public int? niyyatAmount { get; set; }

        public float? takhmeenAmount { get; set; }

        public int? paidAmount { get; set; }

        public string? currency { get; set; }

        public string? bankName { get; set; }

        public string? draftNo { get; set; }

        public DateTime? draftDate { get; set; }

        public DateTime? createdOn { get; set; }

        public string? createdBy { get; set; }

        public DateTime? updatedOn { get; set; }

        public string? updatedBy { get; set; }

        public string? userRemarks { get; set; }

        public string? officeRemarks { get; set; }

        public string? displayCurrency { get; set; }

        public float? currencyRate { get; set; }

        public string? wajebaatType { get; set; }

        public string? stage { get; set; }
        public DateTime? verifiedOn { get; set; }
    }

    public class WafdLanguageProficiency
    {
        public int id { get; set; }

        public int? itsId { get; set; }

        public string? language { get; set; }

        public int? selfRanking { get; set; }

        public string? speaking { get; set; }

        public string? reading { get; set; }

        public string? writing { get; set; }

        public string? listening { get; set; }
    }


    public class dataRequestModel
    {
        public string ITS { get; set; }
        public string Full_name { get; set; }
        public string Misaal_name { get; set; }
        public string Email { get; set; }
        public string Official_Email { get; set; }
        public string Mobile { get; set; }
        public string Whatsapp { get; set; }
        public string Jamaat { get; set; }
        public string Jamiyat { get; set; }
        public string Schoolname { get; set; }
        public string Designation { get; set; }
        public string Tile { get; set; }
        public string Gender { get; set; }
        public string Total_khidmat_muddat { get; set; }
        public string Current_mauze_muddat { get; set; }
        public string age { get; set; }
        public string watan { get; set; }
        public string idara { get; set; }
        public string current_wazifa { get; set; }
        public string jamea_sanad { get; set; }
        public string farig_daraja { get; set; }
        public string hifz_status { get; set; }
        public string jamaatid { get; set; }
        public string jamiyat_type { get; set; }
    }
}
