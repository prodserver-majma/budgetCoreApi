namespace mahadalzahrawebapi.Models
{
    public class enayat_scholarship_billentry
    {
        public int id { get; set; }
        public string billPeriodName { get; set; }
        public int? billPeriodId { get; set; }
        public int? applicantItsId { get; set; }
        public DateTime? createdOn { get; set; }
        public string category { get; set; }
        public string subCategory1 { get; set; }
        public string studentName { get; set; }
        public string courseYear { get; set; }
        public string period { get; set; }
        public string standard { get; set; }
        public int? admissionFee { get; set; }
        public int? tutuionFee { get; set; }
        public int? diniyatFee { get; set; }
        public int? exCurricularFee { get; set; }
        public int? courseBooks { get; set; }
        public int? stationary { get; set; }
        public int? uniform { get; set; }
        public int? conveyence { get; set; }
        public int? termFee { get; set; }
        public int? totalAmount { get; set; }
        public int? activityFee { get; set; }
        public int? examinationFee { get; set; }
        public int? diniyatFeeExam { get; set; }
        public string billStatus { get; set; }
        public string status { get; set; }
        public DateTime? updatedOn { get; set; }
        public string updatedBy { get; set; }
        public string currencySymbol { get; set; }
        public string subCategory2 { get; set; }
        public int? relationTypeId { get; set; }
        public string billType { get; set; }
        public string institutionName { get; set; }
        public string qualification { get; set; }
        public string billattchment { get; set; }
        public string marksheetattachment { get; set; }
        public int? amount_billClearance { get; set; }
        public int? relationItsId { get; set; }
        public string billNumber { get; set; }
        public string venue { get; set; }
        public DateTime? billDate { get; set; }
        public string originalBillStatus { get; set; }
    }
}
