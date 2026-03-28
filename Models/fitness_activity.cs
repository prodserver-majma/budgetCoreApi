namespace mahadalzahrawebapi.Models
{
    public class fitness_activity
    {
        public int id { get; set; }
        public string activityName { get; set; }
        public string venue { get; set; }
        public int? hours { get; set; }
        public int? minutes { get; set; }
        public int? itsId { get; set; }
        public DateTime? createdOn { get; set; }
        public string attachmentFile { get; set; }
        public string routine { get; set; }
        public int? academicYear { get; set; }
    }
}
