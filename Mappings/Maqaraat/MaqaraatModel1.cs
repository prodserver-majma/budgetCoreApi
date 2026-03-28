namespace mahadalzahrawebapi.Mappings
{
    public class MaqaraatModel
    {
        public int srNo { get; set; }
        public string? photo { get; set; }
        public int id { get; set; }
        public int itsId { get; set; }
        public DateTime date { get; set; }
        public bool? isLive { get; set; }
        public bool? isSubmitted { get; set; }
        public int? pages { get; set; }

        public bool? isDaysSubmitted { get; set; }

        public string? isEvaluated { get; set; }
        public bool? isEvaluated2 { get; set; }
        public string? reason { get; set; }
        public int? marks { get; set; }
        public bool isPresent { get; set; }
        public string? absentReason { get; set; }

        public int? juz { get; set; }

        public string? name { get; set; }
        public string? darajah { get; set; }
        public string? moze { get; set; }
        public string? whatsappNumber { get; set; }
        public string? mobileNumber { get; set; }
        public string? email { get; set; }

        public string? masoolName { get; set; }
        public string? acdemicYear { get; set; }
        public string? day { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdOn { get; set; }

        public int? batchId { get; set; }
        public int? teacherItsId { get; set; }

    }

    public class MaqaraatTeacher
    {
        public int itsId { get; set; }
        public string name { get; set; }
        public string moze { get; set; }
        public string category { get; set; }
        public int batchId { get; set; }
        public int numOfStudents { get; set; }
        public int numOfSessions { get; set; }
        public List<MaqaraatSessions> teacherSession { get; set; }
    }

    public class MaqaraatSessions
    {
        public int id { get; set; }
        public string isEvaluated2 { get; set; }
        public DateOnly date { get; set; }
        public int juz { get; set; }
        public bool isEvaluated { get; set; }
        public string reason { get; set; }
        public int acdemicYear { get; set; }
        public string day { get; set; }
        public string createdBy { get; set; }
        public int pages { get; set; }
        public List<MaqaraatStudentMarks> Students { get; set; }
    }

    public class MaqaraatStudentMarks
    {
        public int itsId { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public int marks { get; set; }
        public int batchId { get; set; }
        public int hifzYear { get; set; }
        public bool isPresent { get; set; }
        public string absentReason { get; set; }

    }
}
