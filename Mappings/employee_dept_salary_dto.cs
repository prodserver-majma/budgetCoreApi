namespace mahadalzahrawebapi.Mappings
{
    public class employee_dept_salary_dto
    {

        public int srno { get; set; }

        public int itsId { get; set; }

        public int deptVenueId { get; set; }

        public int salaryTypeId { get; set; }

        public int? workingMin { get; set; }
        public int? workingDays { get; set; }

        public bool? hasSalary { get; set; }

        public float? salaryAmount { get; set; }

        public bool? isHijriSalary { get; set; }

        public virtual dept_venue_dto deptVenue { get; set; }

        public virtual salary_type_dto salaryType { get; set; }
    }
}
