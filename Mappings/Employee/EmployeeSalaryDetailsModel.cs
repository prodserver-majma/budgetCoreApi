
using mahadalzahrawebapi.GenericModels;

namespace mahadalzahrawebapi.Mappings
{
    public class EmployeeSalaryDetailsModel
    {
        public int itsId { get; set; }
        public int grossSalary { get; set; }
        public int? rentAllowance { get; set; }
        public int? marriageAllowance { get; set; }
        public int? mumbaiAllowance { get; set; }
        public int? conveyanceAllowance { get; set; }
        public int? extraAllowance { get; set; }
        public int? professionTax { get; set; }
        public int? tds { get; set; }
        public int? incomeTax { get; set; }
        public int? localTax { get; set; }
        public int? qardanHasanah { get; set; }
        public int? marafiqKhairiyah { get; set; }
        public int? sabeel { get; set; }
        public int? bqhs { get; set; }
        public bool? isHijriAllowence { get; set; }
        public int? lessDeduction { get; set; }
        public int? installmentDeduction_Qardan { get; set; }
        public int? husainiQardanHasanah { get; set; }
        public int? qardanHasanahRefundable { get; set; }
        public int? qardanHasanahNonRefundable { get; set; }
        public bool? isHusainiQardan { get; set; }
        public int? mohammediQardanHasanah { get; set; }
        public bool isMahadSalary { get; set; }
        public int? fmbAllowance { get; set; }
        public int? fmbDeduction { get; set; }
        public int? arrears { get; set; }


        public int? withHoldings { get; set; }
        public DateTime? lastSalaryDate { get; set; }

        public int? lastSalary { get; set; }


        public CalenderModel? lastSalaryDateModel { get; set; }

        public String? currency { get; set; } = "INR";
    }
}
