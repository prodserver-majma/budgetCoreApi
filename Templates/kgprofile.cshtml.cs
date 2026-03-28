using mahadalzahrawebapi.Mappings;

namespace mahadalzahrawebapi.Templates
{
    public class KgprofileList
    {
        public List<KgprofileModel> kgprofileModels { get; set; }

        public suppressDetails suppressDetails { get; set; }

    }
    public class KgprofileModel
    {
        // Basic Details
        public string qismLogoBase64 { get; set; }
        public string kgprofileimgBase64 { get; set; }
        public string QismAlTahfeez { get; set; }
        public string CurrentMauze { get; set; }
        public string Idara { get; set; }
        public string MZCategory { get; set; }
        public string Age { get; set; }
        public string MaritalStatus { get; set; }
        public string DawatTitle { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Nationality { get; set; }
        public string Jamaat { get; set; }
        public string Jamiat { get; set; }
        public string Watan { get; set; }
        public string DomicileParents { get; set; }

        // Contact Details
        public string MobileNo { get; set; }
        public string WhatsAppNo { get; set; }
        public string PrimaryEmail { get; set; }

        // Academic Details
        public string Category { get; set; }
        public string FarigDarajah { get; set; }
        public string FarigYear { get; set; }
        public string AljameaDegree { get; set; }
        public string HifzStatus { get; set; }
        public string TrainingDarajah { get; set; }
        public string BatchId { get; set; }
        public string LatestQualification { get; set; }

        // Khidmat Details
        public string KhidmatYear { get; set; }
        public string KhidmatInMZ { get; set; }
        public string TayeenInMZ { get; set; }
        public string TayeenDuration { get; set; }
        public string KhidmatDuration { get; set; }

        // House Details
        public string HouseStatus { get; set; }
        public string HouseType { get; set; }
        public string HouseAddress { get; set; }

        // Family Details
        public List<Kg_profile_FamilyMember> FamilyMembers { get; set; }

        // Qualification Details
        public List<Kg_profile_Qualification> Qualifications { get; set; }

        // Courses & Workshops
        public List<Kg_profile_CourseWorkshop> CoursesWorkshops { get; set; }

        // Language Proficiency
        public List<Kg_profile_LanguageProficiency> LanguageProficiencies { get; set; }

        // Field of Interest
        public List<Kg_profile_FieldOfInterest> FieldOfInterest { get; set; }

        // About ME
        public string AboutMe { get; set; }

        // Strengths & Weaknesses
        public string Strengths { get; set; }
        public string Weaknesses { get; set; }

        // Aspirations
        public string LongTermGoals { get; set; }
        public string ChangeInMyself { get; set; }
        public string AlternateCareerPath { get; set; }
        public string RoleModel { get; set; }

        // Performance
        public List<Kg_profile_Performance> Performances { get; set; }

        // Past MZ Mawaze
        public List<Kg_profile_Mawaze> PastMZMawaze { get; set; }

        // Other Idara Mawaze
        public List<Kg_profile_Mawaze> OtherIdaraMawaze { get; set; }

        // Khidmat Mawaze
        public List<Kg_profile_KhidmatMawaze> KhidmatMawaze { get; set; }

        // Salary Details
        public EmployeeSalaryDetailsModel salarydetails { get; set; }
        public string TotalAllowance { get; set; }
        public string TotalDeduction { get; set; }
        public string NetSalary { get; set; }


    }

    public class Kg_profile_FamilyMember
    {
        public string Relation { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string Jamaat { get; set; }
    }

    public class Kg_profile_Qualification
    {
        public string Year { get; set; }
        public string StandardDegree { get; set; }
        public string Status { get; set; }
        public string PDYear { get; set; }
        public string Institution { get; set; }
        public string Country { get; set; }
        public string Medium { get; set; }
    }

    public class Kg_profile_CourseWorkshop
    {
        public int Year { get; set; }
        public string Type { get; set; }
        public string Mode { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Topic { get; set; }
    }

    public class Kg_profile_LanguageProficiency
    {
        public string Language { get; set; }
        public string Speaking { get; set; }
        public string Reading { get; set; }
        public string Writing { get; set; }
        public string Listening { get; set; }
        public string Total { get; set; }
    }

    public class Kg_profile_Performance
    {
        public String Year { get; set; }
        public string CommentedByITS { get; set; }
        public string CommentedBy { get; set; }
        public string KGVenue { get; set; }
        public string Remarks { get; set; }
        public string AreaOfImprovement { get; set; }
        public string Marking { get; set; }
    }

    public class Kg_profile_Mawaze
    {
        public string Year { get; set; }
        public string Location { get; set; }
        public string khidmat { get; set; }
        public bool isOtherIdara { get; set; }
    }

    public class Kg_profile_KhidmatMawaze
    {
        public string Event { get; set; }
        public string KhidmatNature { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
    }

    public class Kg_profile_FieldOfInterest
    {
        public string Field { get; set; }
        public string Interest { get; set; }
    }
}



