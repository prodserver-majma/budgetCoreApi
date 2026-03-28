namespace mahadalzahrawebapi.Mappings
{
    public class Wafd_Training_MasoolModel
    {
        public int id { get; set; }
        public int? itsId { get; set; }
        public int batchId { get; set; }
        public bool? activeStatus { get; set; }
        public DateTime? createdOn { get; set; }
        public string? createdBy { get; set; }
        public DateTime? updatedOn { get; set; }

        public string? photo { get; set; }
        public string? moze { get; set; }
        public string? role { get; set; }

        public string? whatsappNumber { get; set; }

        public string? masoolName { get; set; }
        public int? numberOfStudents { get; set; }

        public List<int>? days { get; set; }

    }
}
