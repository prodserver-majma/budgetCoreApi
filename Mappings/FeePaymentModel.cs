namespace mahadalzahrawebapi.Mappings
{
    public class FeePaymentModel
    {
        public int? id { get; set; }
        public int? allotmentId { get; set; }
        public string? name { get; set; }
        public int? amount { get; set; }

        public string? paymentId { get; set; }
        public FeeTransactionModel? reciept { get; set; }
    }
}
