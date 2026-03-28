using mahadalzahrawebapi.GenericModels;

namespace mahadalzahrawebapi.Mappings
{
    public class payroll_salary_package_dto
    {
        public int id { get; set; }

        public string name { get; set; }

        public int amount { get; set; }

        public string description { get; set; }

        public DateTime? paymentDate { get; set; }

        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }

        public int qismId { get; set; }

        public string paymentFrom { get; set; }
        public CalenderModel? fromDateCalenderModel { get; set; }
        public CalenderModel? toDateCalenderModel { get; set; }

        public string? qismName { get; set; }
        public List<SalaryAllocation> salaryAllocation { get; set; } = new List<SalaryAllocation>();
    }
}