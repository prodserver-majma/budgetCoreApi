namespace mahadalzahrawebapi.Mappings
{
    public class SalaryExportModel
    {
        public int srNo { get; set; }
        public int itsId { get; set; }
        public string name { get; set; }
        public string employeeType { get; set; }
        public string? mzIdara { get; set; }
        public string? designation { get; set; }
        public int age { get; set; }
        public int? batchId { get; set; }
        public string? category { get; set; }
        public int? farigDarajah { get; set; }

        public string? workType { get; set; }

        public string pan { get; set; }
        public string account_Number { get; set; }
        public string bank_AccountName { get; set; }
        public string ifsc { get; set; }
        public int salary { get; set; }
        public int rentAllowance { get; set; }
        public int marriageAllowance { get; set; }
        public int convenienceAllowance { get; set; }
        public int fmbAllowance { get; set; }
        public int arrears { get; set; }
        public int overtime { get; set; }


        public int shortfall { get; set; }
        public int ctc { get; set; }


        public int sabeel { get; set; }
        public int marafiqKhairiyah { get; set; }
        public int qardanHasanahRefundable { get; set; }
        public int qardanNonHasanahRefundable { get; set; }
        public int withholdings { get; set; }

        public int professionTax { get; set; }
        public int tds { get; set; }
        public int localTax { get; set; }

        public int netEarnings { get; set; }
        public string currency { get; set; }


        public DateTime? paymentDate { get; set; }

        public string Month_Year { get; set; }


        public int packageId { get; set; }
    }
}
