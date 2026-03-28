namespace mahadalzahrawebapi.Mappings
{
    public class Export_VendorLedgerModel
    {


        public int id { get; set; }
        public string vendorId { get; set; }
        public string vendorName { get; set; }

        public int billId { get; set; }
        public string billNumber { get; set; }
        public string billDate { get; set; }

        public string txnDate { get; set; }

        public string credit { get; set; }
        public string debit { get; set; }


        public string balance { get; set; }
        public string paymentType { get; set; }



        public string note { get; set; }
        public string createdBy { get; set; }






    }
}
