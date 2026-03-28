namespace mahadalzahrawebapi.Templates
{
    public class ExpenseBillBrief1
    {
        public int billid { get; set; }
        public string billNumber { get; set; }

        public int vendorId { get; set; }
        public string? vendorName { get; set; }
        public string nameOfVendor { get; set; }
        public string vendorAccName { get; set; }
        public string vendorAccountNo { get; set; }
        public string vendorBankName { get; set; }
        public string vendorIfsc { get; set; }


        public DateOnly billDate { get; set; }

        public DateTime entryDate { get; set; }
        public int billAmount { get; set; }

        public string paymentMode { get; set; }

        public string deptName { get; set; }
        public string? venueName { get; set; }
        public string nameOfVenue { get; set; }
        public string baseItemName { get; set; }

    }
}
