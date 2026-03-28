namespace mahadalzahrawebapi.Mappings
{
    public class FeesAllotmentModel
    {
        public int id { get; set; }
        public int? studentId { get; set; }
        public int? itsId { get; set; }
        public string? studentName { get; set; }
        public string? psetName { get; set; }
        public int? pSetId { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdOn { get; set; }
        public int? feeAlloted { get; set; }
        public int? fcId { get; set; }
        public string? reason { get; set; }
        public string? remarks { get; set; }
        public int? monthId { get; set; }
        public string? hijriYear { get; set; }
        public string? currency { get; set; }
        public int? txn_Id { get; set; }
        public string? monthName { get; set; }
        public string? fc_Name { get; set; }
        public string? itsIdCSV { get; set; }
        public List<int>? monthList { get; set; }
        public List<string>? months { get; set; }
        public List<monthlyAmount> monthsList { get; set; } = new List<monthlyAmount>();
        public int? programSetId { get; set; }
        public List<feeCategory>? feeCategoriesAlloted { get; set; } = new List<feeCategory>();

    }

    public class BillItemListModel
    {
        public int itemId { get; set; }
        public string? itemName { get; set; }
        public string? remark { get; set; }
        public int? billId { get; set; }
        public float total { get; set; }
        public float quantity { get; set; }
        public float amountPerPc { get; set; }
    }

    public class SearchRecieptModel
    {
        public DateOnly? fromDate { get; set; }
        public DateOnly? toDate { get; set; }
        public string? itsCsv { get; set; }

        public int? monthId { get; set; }

        public string? hijriYear { get; set; }

    }

    public class BillManagementModel
    {
        public int? id { get; set; }
        public bool? select { get; set; }
        public int? vendorId { get; set; }
        public int? baseItemId { get; set; }
        public int? deptVenueId { get; set; }
        public int? psetId { get; set; }
        public string? billNumber { get; set; }
        public DateOnly? billDate { get; set; }
        public int? itemId { get; set; }
        public float? quantity { get; set; }
        public float? amountPerUom { get; set; }
        public int? totalAmount { get; set; }
        public string? remarks { get; set; }

        public string? baseItemName { get; set; }
        public bool? isCapex { get; set; }

        public string? className { get; set; }
        public string? vendorName { get; set; }
        public string? deptName { get; set; }
        public string? venueName { get; set; }
        public string? deptVenueName { get; set; }
        public string? itemName { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdOn { get; set; }
        public DateOnly? paymentDate { get; set; }
        public DateOnly? entryDate { get; set; }

        public string? paymentMode_User { get; set; }
        public string? paymentMode_Admin { get; set; }
        public string? paymentTo_AccNum { get; set; }
        public string? paymentTo_AccName { get; set; }
        public string? paymentTo_BankName { get; set; }
        public string? paymentTo_ifsc { get; set; }
        public string? paymentFrom_BankName { get; set; }
        public string? status { get; set; }
        public bool? isWaived { get; set; }
        public int? packageId { get; set; }

        public float? gstPercentage { get; set; }
        public int? gstAmount { get; set; }
        public float? itemGstPercentage { get; set; }
        public float? itemGstAmount { get; set; }
        public int? tdsApplicableAmount { get; set; }
        public float? tdsPercentage { get; set; }
        public int? tdsAmount { get; set; }
        public int? conveyanceAmount { get; set; }
        public bool? isReconciled { get; set; }
        public string? txnId { get; set; }
        public bool? isFundRequested { get; set; }

        public string? isFundRequestedString { get; set; }
        public string? paymentDateString { get; set; }
        public string? billDateString { get; set; }
        public string? entryDateString { get; set; }

        public string? billAttachment { get; set; }
        public string? billAttachmentFileName { get; set; }

    }

    public class ExpenseBillBrief
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
        public string? className { get; set; }
        public string nameOfVenue { get; set; }
        public string baseItemName { get; set; }

    }
}
