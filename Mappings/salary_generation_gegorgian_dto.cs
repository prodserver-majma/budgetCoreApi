namespace mahadalzahrawebapi.Mappings
{
    public class salary_generation_gegorgian_dto
    {

        public int id { get; set; }

        public int itsId { get; set; }

        public int quantity { get; set; }

        public int? netSalary { get; set; }

        public int month { get; set; }

        public int year { get; set; }

        public DateTime? createdOn { get; set; }

        public string? createdBy { get; set; }

        public int deptVenueId { get; set; }

        public int? allocationId { get; set; }

        public int salaryType { get; set; }

        public virtual salary_allocation_gegorian_dto allocation { get; set; }
        public virtual dept_venue_dto deptVenue { get; set; }

        public virtual salary_type_dto salaryTypeNavigation { get; set; }
    }

}
