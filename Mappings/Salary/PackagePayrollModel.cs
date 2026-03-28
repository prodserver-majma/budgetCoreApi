namespace mahadalzahrawebapi.Mappings
{
    public class PackagePaymentModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<BillManagementModel> bills { get; set; }

    }

    public class PackagePayrollModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int totalPaymentAmount { get; set; }
        public string description { get; set; }
        public DateTime? paymentDate { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string? fromDateHijri { get; set; }
        public string? toDateHijri { get; set; }
        public int qismId { get; set; }
        public int createdBy { get; set; }
        public DateTime? createdOn { get; set; }
        public string? paymentFrom { get; set; }
        public List<SalaryAllocation> salaries { get; set; }

    }
}
