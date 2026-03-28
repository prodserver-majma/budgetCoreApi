namespace mahadalzahrawebapi.Mappings
{
    public class EmployeeFamilyDetailsModel
    {
        public int id { get; set; }
        public int itsId { get; set; }
        public string? FatherName { get; set; } = "";
        public string? FatherIts { get; set; } = "";
        public string? MotherName { get; set; } = "";
        public string? MotherIts { get; set; } = "";
        public string? SpouseName { get; set; } = "";
        public string? SpouseIts { get; set; } = "";
    }

    public class EmployeeFamilyMembersModel
    {
        public int id { get; set; }
        public int itsId { get; set; }
        public string? name { get; set; } = "";
        public string? relation { get; set; } = "";
        public int? relationTypeId { get; set; }

        public int? age { get; set; }

        public string? jamaat { get; set; } = "";

        public string? idara { get; set; } = "";

        public string? occupation { get; set; } = "";

        public string? hifzStatus { get; set; } = "";

        public string? nationality { get; set; } = "";

        public DateOnly? dob { get; set; }

        public string bloodGroup { get; set; } = "";
    }
}
