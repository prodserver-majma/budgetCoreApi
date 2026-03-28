namespace mahadalzahrawebapi.Mappings
{
    public class WorkShopModel
    {
        public int id { get; set; }
        public string? year { get; set; }
        public string? type { get; set; }
        public string? course { get; set; }
        public string? mode { get; set; }
        public string? category { get; set; }
        public string? subCategory { get; set; }
        public string? courseName { get; set; }
        public string? keypoints { get; set; }
        public string? cetificateCredentials { get; set; }
        public string? attachment { get; set; }
        public string? attachmentFileName { get; set; }

        public string? academicYear { get; set; }
        public DateOnly? completionDate { get; set; }
        public int? totalHours { get; set; }
        public int? hoursPerDay { get; set; }
        public int? totalDays { get; set; }

    }
}
