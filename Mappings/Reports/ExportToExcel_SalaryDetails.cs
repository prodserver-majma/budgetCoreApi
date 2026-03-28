namespace mahadalzahrawebapi.Mappings
{
    public class ExportToExcel_SalaryDetails
    {
        public string srNo { get; set; }
        public string itsId { get; set; }
        public string name { get; set; }
        public string age { get; set; }
        public string farigYear { get; set; }
        public string farigDarajah { get; set; }
        public string batchId { get; set; }
        public string mauze { get; set; }

        public int grossSalary { get; set; }
        public int? rentAllowance { get; set; }
        public int? marriageAllowance { get; set; }
        public int? mumbaiAllowance { get; set; }
        public int? conveyanceAllowance { get; set; }
        public int? extraAllowance { get; set; }
        public int? fmbAllowance { get; set; }
        public int ctc { get; set; }
        public int? marafiqKhairiyah { get; set; }
        public int? qardanHasanah { get; set; }
        public int? professionTax { get; set; }
        public int? tds { get; set; }
        public int? sabeel { get; set; }
        public int? bqhs { get; set; }
        public int? fmbDeduction { get; set; }
        public int netSalary { get; set; }

        public string isMahadSalary { get; set; }
    }
}
