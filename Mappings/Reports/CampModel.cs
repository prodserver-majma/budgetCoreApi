namespace mahadalzahrawebapi.Mappings
{

    public static class MahadFixedData
    {
        public static string HijriYears = "1416,1417,1418,1419,1420,1421,1422,1423,1424,1425,1426,1427,1428,1429,1430,1431,1432,1433,1434,1435,1436,1437,1438,1439,1440,1441,1442,1443,1444,1445,1446,1447,1448,1449,1450";
    }


    public class CampRegistrationModel
    {
        public long Id { get; set; }
        public string? StudentName { get; set; }
        public int? ItsId { get; set; }
        public string? Password { get; set; }
        public string? dob { get; set; }
        public string? vatan { get; set; }
        public string? Maqaam { get; set; }
        public string? Jamaat { get; set; }
        public string? FatherMobile { get; set; }
        public string? MotherMobile { get; set; }
        public string? FatherEmail { get; set; }
        public string? MotherEmail { get; set; }
        public string? FatherOccupation { get; set; }
        public string? PostalAddress { get; set; }
        public string? status { get; set; }
        public string? Reason { get; set; }
        public string? CampName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CampId { get; set; }
        public string? Criteria { get; set; }
        public string? CampVenue { get; set; }
        public string? Gender { get; set; }
        public string? CreatedBy { get; set; }
        public string? Months { get; set; }
        public string? Schooling { get; set; }
        public int? Juz { get; set; }
        public string? Notes { get; set; }
        public int? deptVenueId { get; set; }
        public string? programName { get; set; }
        public string? subProgramName { get; set; }
        public string? venueName { get; set; }

        public int? programSetId { get; set; }

    }

    public class feeCategory
    {
        public int id { get; set; }
        public string name { get; set; }
        public double? amount { get; set; }
        public List<monthlyAmount> month { get; set; } = new List<monthlyAmount>();
    }

    public class monthlyAmount
    {
        public string month { get; set; }
        public double? amount { get; set; }
    }

    public class CampStudentModel
    {
        public long Id { get; set; }
        public int? ItsId { get; set; }
        public int? mzId { get; set; }
        public string? StudentName { get; set; }
        public string? Category { get; set; }
        public string? CampId { get; set; }
        public string? CampVenue { get; set; }
        public string? FeeStatus { get; set; }
        public Nullable<bool> ActiveStatus { get; set; }
        public string? MobileNo { get; set; }
        public string? Vatan { get; set; }
        public DateTime? DOB { get; set; }
        public string? DOB_string { get; set; }

        public string? DOB_hijri { get; set; }
        public string? EmailId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? Jamaat { get; set; }
        public string? Jamiat { get; set; }

        public string? EmailStatus { get; set; }

        public string? Inactive_Reason { get; set; }
        public int? deptVenueId { get; set; }
        public int? programSetId { get; set; }

        //*** To Return Pending And Cleared Fees
        public int? FeesAlotted { get; set; }
        public int? FeesPaid { get; set; }
        public int? DueAmount { get; set; }
        public int? dueAmount2 { get; set; }
        //***** For Transfer reason ************
        public string? Transfer_Reason { get; set; }
        public string? Months { get; set; }

        public string? program { get; set; }
        public string? venue { get; set; }
        public string? subProgram { get; set; }
        public int? age { get; set; }

        public string? pset_Name { get; set; }
        public string? programName { get; set; }
        public string? subProgramName { get; set; }
        public string? venueName { get; set; }
        public string? activeStatusString { get; set; }
        public string? activeStatusString2 { get; set; }

        public string? currency { get; set; }
        public string? fc_name { get; set; }

        public List<feeCategory> feeCategoriesAlloted { get; set; } = new List<feeCategory>();
        public string? amount { get; set; }
        public int? pendingAmount { get; set; }
        public int? walletAmount { get; set; }

        public string? studentMobile { get; set; }
        public string? studentEmail { get; set; }
        public string? fatherMobile { get; set; }
        public string? fatherEmail { get; set; }
        public string? motherMobile { get; set; }
        public string? motherEmail { get; set; }
        public string? hijriMonthName { get; set; }
        public string? hijriyear { get; set; }

        public int? trno { get; set; }
        public string? std { get; set; }

        public string? masoolName { get; set; }
        public int? hafizYear { get; set; }
        public string? watan { get; set; }
        public string? maqaam { get; set; }
        public string? blood_grp { get; set; }
        public string? nationality { get; set; }
        public string? address { get; set; }

        public int? dq_fasal { get; set; }
    }

    public class StudentFeesAssociationModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int? ItsId { get; set; }
        public int? FeesAlotted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? Remark { get; set; }
        public int? FeesPaid { get; set; }
        public int? DueAmount { get; set; }
        public string? Category { get; set; }
        public string? Reason { get; set; }
        public DateTime? UpdateOn { get; set; }
        public int? MonthId { get; set; }
    }

    public class FeesAlottedModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int? ItsId { get; set; }
        public string? CreatedBy { get; set; }
        public int? FeeAlotted { get; set; }
        public string? EnumType { get; set; }
        public string? Reason { get; set; }
        public string? Remarks { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? MonthId { get; set; }
    }

    public class FeesPaidModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int? ItsId { get; set; }
        public string? name { get; set; }
        public string? receiptNo { get; set; }
        public string? CreatedBy { get; set; }
        public int? FeePaidAmount { get; set; }
        public string? EnumType { get; set; }
        public string? Reason { get; set; }
        public string? Remarks { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? printDate { get; set; }
        public int? MonthId { get; set; }
        public string? PaymentMode { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public string? PaidAt { get; set; }

        public string? Action { get; set; }
        public DateOnly? ChequeDate { get; set; }
        public Nullable<bool> ReverseStatus { get; set; }
        public DateTime? ReverseDate { get; set; }

        public long ReceiptId { get; set; }
        public int? EntryId { get; set; }
        public string? VenuePrefix { get; set; }
        public string? Account { get; set; }
        public int? FinancialYear { get; set; }
        public bool IsDraft { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public int? transferTo { get; set; }
        public int? transferFrom { get; set; }
        public string? referenceNumber { get; set; }
        public string? amountInWord { get; set; }
    }

    public class ReceiptModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int? ItsId { get; set; }
        public int? FPLogs_Id { get; set; }
        public int? Amount { get; set; }
        public string? CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string? Remark { get; set; }
        public string? PaymentMode { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public string? Reason { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? MonthId { get; set; }
        public int? EntryId { get; set; }
        public string? PaidAt { get; set; }
        public string? VenuePrefix { get; set; }
        public DateTime? ChequeDate { get; set; }
        public Nullable<bool> ReverseStatus { get; set; }
        public DateTime? ReverseDate { get; set; }
        public string? Account { get; set; }
        public int? FinancialYear { get; set; }
        public bool IsDraft { get; set; }
        public DateTime? ReceiptDate { get; set; }

        //*********** to provide student name *********
        public string? StudentName { get; set; }
        public string? CampVenue { get; set; }
    }

    public class YellowReceiptModel
    {
        public long Id { get; set; }
        public int? ItsId { get; set; }
        public string? Name { get; set; }
        public int? Amount { get; set; }
        public string? CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public DateTime CancelDate { get; set; }
        public string? PaymentMode { get; set; }
        public string? BankName { get; set; }
        public string? ChequeNo { get; set; }
        public string? PaidAt { get; set; }
        public string? Account { get; set; }
        public int? FinancialYear { get; set; }
        public string? Remarks { get; set; }
        public string? Purpose { get; set; }
        public int? EntryId { get; set; }
        public string? Status { get; set; }
        public string? whatsappNo { get; set; }
        public string? email { get; set; }
    }

    public class RefundModel
    {
        public long Id { get; set; }
        public int? ItsId { get; set; }
        public string? StudentName { get; set; }
        public int? Amount { get; set; }
        public string? BankName { get; set; }
        public string? ChequeNo { get; set; }
        public System.DateTime ChequeDate { get; set; }
        public string? AccountHolderName { get; set; }
        public string? AccountNumber { get; set; }
        public string? Reason { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public class CampModel
    {
        public long Id { get; set; }
        public int? CampId { get; set; }
        public string? CampName { get; set; }
        public int? MaxCount { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public System.DateTime PreviewDate { get; set; }
        public string? ForGender { get; set; }
        public int? FromAge { get; set; }
        public int? ToAge { get; set; }
        public Nullable<bool> ActiveStatus { get; set; }
        public System.DateTime Createdon { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

    public class AddStudentModel
    {
        public string? ItsCSV { get; set; }
        public int? CampId { get; set; }
        public string? Category { get; set; }
        public string? name { get; set; }
    }

    public static class MZ_Dept
    {
        public static string Nisaab = "Nisaab";
        public static string Camp = "Camp";
    }

    public static class MZ_CampId
    {
        public static string NajamBaug = "CNB";
        public static string Panchgani = "CP";
        public static string Nairobi = "CMN";
        public static string Rise = "CR";
        public static string Atfaal = "AS";
        public static string Sharjah = "CSHAR";

        public static string OtherVolCon = "OVC";


        public static string C_Burhanpur = "CBR";
        public static string C_Taherabad = "CT";
        public static string C_Galiakot = "CG";
        public static string C_Banswara = "CB";
        public static string C_Hasanfeer = "CH";
        public static string C_Doangam = "CD";
        public static string C_Nuzul = "CN";
        public static string C_ZainyBanglow = "CZ";
        public static string C_Panchgani = "CP";


    }

    public static class MZ_CampVenue
    {
        public static string NajamBaug = "Najam Baug";
        public static string Panchgani = "Panchgani";
        public static string Nairobi = "Nairobi";
        public static string Rise = "Rise";
    }
}
