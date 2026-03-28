namespace mahadalzahrawebapi.Mappings
{
    public class EmployeeDeptSalaryModel
    {
        public int itsId { get; set; }
        public int deptVenueId { get; set; }
        public int salaryTypeId { get; set; }
        public int? workingMin { get; set; }
        public bool? hasSalary { get; set; }
        public Nullable<float> salaryAmount { get; set; }
        public bool? isHijriSalary { get; set; }
        public int? workingDays { get; set; }

        public virtual dept_venue_dto? dept_venue { get; set; }
        public virtual salary_type_dto? salary_Type { get; set; }

    }
}
