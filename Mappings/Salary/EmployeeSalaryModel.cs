using mahadalzahrawebapi.Models;

namespace mahadalzahrawebapi.Mappings
{
    public class EmployeeSalaryModel
    {

        public EmployeeSalaryDetailsModel salaryDetails { get; set; }
        public List<EmployeeDeptSalaries> deptSalaries { get; set; }

    }
    public class EmployeeDeptSalaries
    {
        public int srno { get; set; }
        public int itsId { get; set; }
        public int deptVenueId { get; set; }
        public int salaryTypeId { get; set; }
        public int? workingMin { get; set; }
        public bool? hasSalary { get; set; }
        public Nullable<float> salaryAmount { get; set; }
        public bool? isHijriSalary { get; set; }

        public virtual dept_venue dept_venue { get; set; }
        public virtual salary_type salary_type { get; set; }
    }

    public class SalaryAllocation
    {
        public int id { get; set; }
        public string? phoroUrl { get; set; }
        public string? photoBase64 { get; set; }
        public int itsId { get; set; }
        public string? name { get; set; }
        public string? employeeType { get; set; }
        public string? mzIdara { get; set; }
        public string? designation { get; set; }
        public int age { get; set; }
        public int? batchId { get; set; }
        public string? category { get; set; }
        public int? farigDarajah { get; set; }
        public string? workType { get; set; }

        public int salary { get; set; }
        public int? rentAllowance { get; set; }
        public int? marriageAllowance { get; set; }
        public int? convenienceAllowance { get; set; }
        public int? mumbaiAllowance { get; set; }
        public int? fmbAllowance { get; set; }
        public int? lessDeduction { get; set; }
        public int? extraAllowance { get; set; }
        public int? overtime { get; set; }
        public int? shortfall { get; set; }
        public int? arrears { get; set; }
        public int ctc { get; set; }
        public int? professionTax { get; set; }
        public int? tds { get; set; }
        public int? qardanHasanah { get; set; }
        public int? sabeel { get; set; }
        public int? marafiqKhairiyah { get; set; }
        public string currency { get; set; }
        public int? fmbDeduction { get; set; }
        public int? bqhs { get; set; }
        public int? mohammedi_qardanHasanah { get; set; }
        public int? husaini_qardanHasanah { get; set; }
        public int? installmentDeduction_Qardan { get; set; }
        public int? qardanHasanahRefundable { get; set; }
        public int? qardanHasanahNonRefundable { get; set; }
        public int? withHoldings { get; set; }
        public int? incomeTax { get; set; }
        public int? localTax { get; set; }
        public int netEarnings { get; set; }
        public int? timeDelta { get; set; }
        public int? dayDelta { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public DateTime salaryFrom { get; set; }
        public DateTime salaryTo { get; set; }
        public DateTime createdOn { get; set; }
        public string createdBy { get; set; }
        public DateTime? paymentDate { get; set; }
        public string? systemRemarks { get; set; }
        public string? qismRemarks { get; set; }
        public int packageId { get; set; }
        public bool isHijri { get; set; }

        public int? timeDeltaAmount { get; set; }
        public int? dayDeltaAmount { get; set; }

        public string? accountName { get; set; }
    }

    public partial class SalaryGeneration
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
        public int salaryTypeId { get; set; }

        public string? departmentName { get; set; }
        public string? venueName { get; set; }
        public string? salaryType { get; set; }
        public bool isHijri { get; set; }
        public DateTime? paymentDate { get; set; }
    }
}
