namespace mahadalzahrawebapi.Mappings
{
    public class ReceiptStatement_ExportModel
    {
        public int srNo { get; set; }
        public string recieptDate { get; set; }
        public string receiptNo { get; set; }

        public int mz_Id { get; set; }

        public int itsId { get; set; }
        public string name { get; set; }
        public string feePaidAmount { get; set; }

        public string recieptDate_print { get; set; }
        public string chequeDate { get; set; }
        public string paymentMode { get; set; }
        public string bankName { get; set; }
        public string transactionId { get; set; }
        public string note { get; set; }

        public string receiptId { get; set; }
        public string createdBy { get; set; }
        public string collectionCenter { get; set; }
        public string account { get; set; }

        public string status { get; set; }


    }
}
