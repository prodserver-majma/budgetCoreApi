namespace mahadalzahrawebapi.Mappings
{
    public class dept_venue_dto
    {

        public int id { get; set; }

        public int? masterDeptId { get; set; }

        public int? deptId { get; set; }

        public int? venueId { get; set; }

        public string? masterDeptName { get; set; }

        public string? deptName { get; set; }

        public string? venueName { get; set; }

        public string? status { get; set; }

        public string? tag { get; set; }
        public int? qismId { get; set; }

    }
}