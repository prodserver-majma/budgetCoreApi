namespace mahadalzahrawebapi.Models
{
    public class enayat_medical_billentry
    {
        public int id { get; set; }
        public string billPeriod { get; set; }
        public string requestType { get; set; }
        public string requestFor { get; set; }
        public DateTime? entryDate { get; set; }
        public string billType { get; set; }
        public DateTime? billDate { get; set; }
        public string billFrom { get; set; }
        public int? amount { get; set; }
        public string illness { get; set; }
        public int? billPeriodId { get; set; }
        public int? aplicantItsId { get; set; }
        public string relationType { get; set; }
        public int? relationTypeId { get; set; }
        public string billStatus { get; set; }
        public string status { get; set; }
        public DateTime? updatedOn { get; set; }
        public string updatedBy { get; set; }
        public string currencySymbol { get; set; }
        public string attachment { get; set; }
        public int? amount_billClearance { get; set; }
        public string billNumber { get; set; }
        public int? relationItsId { get; set; }
        public string originalBillStatus { get; set; }
    }
}
