namespace mahadalzahrawebapi.Mappings
{
    public class PayrollProcessingModel
    {
        public int id { get; set; }
        public int itsId { get; set; }
        public string name { get; set; }
        public string employeeType { get; set; }

        public string pan { get; set; }
        public string account_Number { get; set; }
        public string bank_AccountName { get; set; }
        public string bankName { get; set; }
        public string ifsc { get; set; }
        public string currency { get; set; }

        public int salary { get; set; }
        public int ctc { get; set; }
        public int professionTax { get; set; }
        public int tds { get; set; }
        public int netEarnings { get; set; }

        public DateTime? paymentDate { get; set; }
        public int packageId { get; set; }

        public int month { get; set; }
        public int year { get; set; }
        public bool isHijri { get; set; }

        public string paymentFrom { get; set; }
    }
    public class AddPaymentModel
    {
        public int id { get; set; }
        public DateTime paymentDate { get; set; }
    }
}
