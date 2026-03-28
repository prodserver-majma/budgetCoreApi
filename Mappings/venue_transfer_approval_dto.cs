namespace mahadalzahrawebapi.Mappings
{
    public class venue_transfer_approval_dto
    {
        public int id { get; set; }
        public int requested_by { get; set; }
        public int employeeIts { get; set; }
        public int current_venue_id { get; set; }
        public int? reviewed_by { get; set; }
        public string stage { get; set; }
        public DateTime requested_on { get; set; }
        public string? approval_comment { get; set; }
        public DateTime? updated_on { get; set; }
    }
}
