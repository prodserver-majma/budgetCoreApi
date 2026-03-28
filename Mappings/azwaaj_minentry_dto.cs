namespace mahadalzahrawebapi.Mappings
{
    public class azwaaj_minentry_dto
    {
        public int id { get; set; }
        public int? itsid { get; set; }
        public DateOnly? date { get; set; }
        public int min { get; set; } = 0;
        public int mindiff { get; set; } = 0;
        public int? deptVenueId { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdOn { get; set; }
        public string? description { get; set; }
        public int? policyId { get; set; }
        public bool isOnLeave { get; set; }
        public int? qismId { get; set; }
        public string? policyName { get; set; }
        public string? name { get; set; }
        public string? deptVenueName { get; set; }
        public string? hijriDate { get; set; }
        public bool ispending { get; set; }
        public float? value { get; set; } = 0.0f;
        public string employeeType { get; set; }
        public string mz_idara { get; set; }
        public string designation { get; set; }
        public string mauze { get; set; }
        public string mz_idaracolor { get; set; }
        public float? rate { get; set; }

        public int? allotedMin { get; set; }
    }
}
