namespace mahadalzahrawebapi.Models
{
    public class nisaab_student_logs
    {
        public int id { get; set; }
        public int itsId { get; set; }
        public string courseName { get; set; }
        public int courseDuration { get; set; }
        public string instituteCountry { get; set; }
        public string instituteCity { get; set; }
        public string instituteName { get; set; }
        public DateTime courseStartDate { get; set; }
        public DateTime courseEndDate { get; set; }
        public int academicYear { get; set; }
    }
}
