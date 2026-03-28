namespace mahadalzahrawebapi.Mappings
{
    public class salary_allocation_hijri_dto
    {
        public int id { get; set; }

        public int itsId { get; set; }

        public int salary { get; set; }

        public int? rentAllowance { get; set; }

        public int? marriageAllowance { get; set; }

        public int? convenienceAllowance { get; set; }

        public int? mumbaiAllowance { get; set; }

        public int? fmbAllowance { get; set; }

        public int? lessDeduction { get; set; }

        public int? extraAllowance { get; set; }

        public int ctc { get; set; }

        public int? professionTax { get; set; }

        public int? tds { get; set; }

        public int? qardanHasanah { get; set; }

        public int? sabeel { get; set; }

        public int? marafiqKhairiyah { get; set; }

        public string? currency { get; set; }

        public int? fmbDeduction { get; set; }

        public int? bqhs { get; set; }

        public int? mohammedi_qardanHasanah { get; set; }

        public int? husaini_qardanHasanah { get; set; }

        public int? installmentDeduction_Qardan { get; set; }

        public int netEarnings { get; set; }

        public int month { get; set; }

        public int year { get; set; }

        public DateTime createdOn { get; set; }

        public string? createdBy { get; set; }

        public DateTime? paymentDate { get; set; }

        public int? packageId { get; set; }
        public virtual payroll_salary_package_dto package { get; set; }
        public virtual ICollection<salary_generation_hijri_dto> salary_generation_hijris { get; set; } = new List<salary_generation_hijri_dto>();
    }
}
