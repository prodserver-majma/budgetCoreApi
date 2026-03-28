namespace mahadalzahrawebapi.Mappings
{
    public class FeeTransactionModel
    {

        public int id { get; set; }
        public int? itsId { get; set; }
        public int? itsId_from { get; set; }
        public int? itsId_to { get; set; }
        public string? billNumber { get; set; }
        public string? reference { get; set; }

        public DateTime dateTime { get; set; }
        public DateOnly billDate { get; set; }
        public string? debit { get; set; }
        public string? credit { get; set; }
        public int? debitNo { get; set; }
        public int? creditNo { get; set; }
        public string? balance { get; set; }
        public string? paymentType { get; set; }
        public string? note { get; set; }
        public string? createdBy { get; set; }

        public DateOnly? chequeDate { get; set; }
        public DateTime cancelDate { get; set; }


        public string? reason { get; set; }
        public string? purpose { get; set; }
        public string? action { get; set; }

        public int? collectioncenterId { get; set; }
        public string? bankName { get; set; }

        public string? status { get; set; }

        public string? cssClass { get; set; }

        public int? tdsAmount { get; set; }
        public int? tdsApplicableAmount { get; set; }
        public float? tdsPercentage { get; set; }
        public string? whatsappNo { get; set; }
        public string? email { get; set; }
        public string? transactionId { get; set; }

    }
}
