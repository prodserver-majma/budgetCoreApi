namespace mahadalzahrawebapi
{
    public class DeptVenueRightModel
    {

        public int id { get; set; }

        public int? deptId { get; set; }
        public int? venueId { get; set; }

        public string? deptName { get; set; }
        public string? venueName { get; set; }
        public bool right { get; set; }
        public string? deptVenueName { get; set; }
        public bool? isTagged { get; set; }
        public int? qismId { get; set; }
        public int? psetId { get; set; }
        public int? classId { get; set; }
    }
}
