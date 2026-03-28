namespace mahadalzahrawebapi.Mappings
{
    public class EmployeeAcademicDetailsModel
    {
        public int id { get; set; }
        public int itsId { get; set; }
        public int? trNo { get; set; }
        public int? farigDarajah { get; set; }
        public int? farigYear { get; set; }
        public string? aljameaDegree { get; set; } = "";
        public int? hifzSanadYear { get; set; }
        public int? wafdTrainingMasoolIts { get; set; }
        public int? wafdTrainingMushrifIts { get; set; }
        public int? maqaraatTeacherIts { get; set; }
        public int? wafdClassId { get; set; }
        public string? category { get; set; } = "";
        public int? batchId { get; set; }
        public string? hifzStatus { get; set; }

        public string? trainingClass { get; set; }
    }
}
