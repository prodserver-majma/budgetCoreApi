namespace mahadalzahrawebapi.Mappings
{
    public class NisaabTalabatModel
    {
        public bool select { get; set; }
        public long id { get; set; }
        public string photo { get; set; }

        public int itsId { get; set; }
        public string fullNameArabic { get; set; }
        public string fullName { get; set; }
        public string age { get; set; }
        public string bloodGroup { get; set; }
        public string dob { get; set; }
        public string dobArabic { get; set; }
        public string roomNo { get; set; }

        public string nameArabic { get; set; }
        public int? fatherItsId { get; set; }
        public string fatherNameArabic { get; set; }
        public string fatherOccupation { get; set; }
        public int? motherItsId { get; set; }
        public string motherNameArabic { get; set; }
        public string surNameArabic { get; set; }
        public string name { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string surName { get; set; }

        public string watan { get; set; }
        public string watanArabic { get; set; }
        public string maqaam { get; set; }
        public string maqaamArabic { get; set; }
        public string nationality { get; set; }
        public string birthPlace { get; set; }
        public string currentAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pincode { get; set; }
        public string jamaat { get; set; }
        public string jamiat { get; set; }

        public string mobileNo { get; set; }
        public string mobileNo2 { get; set; }
        public string talabatEmailId { get; set; }
        public string parentEmailId { get; set; }
        public string parentEmailId2 { get; set; }
        public string fatherMobielNo { get; set; }
        public string motherMobileNo { get; set; }

        public string std { get; set; }
        public string division { get; set; }
        public string masool { get; set; }
        public string idNo { get; set; }
        public string rfNo { get; set; }
        public string hafiz { get; set; }
        public string yearOfAdmission { get; set; }
        public string yearOfAdmissionHijri { get; set; }

        public string dobPassport { get; set; }
        public string passportNo { get; set; }
        public string passportName { get; set; }
        public string dateOfIssue { get; set; }
        public string dateOfValidity { get; set; }
        public string passportNationality { get; set; }
        public string placeOfIssue { get; set; }

        public Nullable<bool> activeStatus { get; set; }
        public string feeCategory { get; set; }
        public string venue { get; set; }
        public string gender { get; set; }
        public int? deptVenueId { get; set; }
        public int hifzTabaqa { get; set; }
        public string result_std_1 { get; set; }
        public string result_std_2 { get; set; }
        public string result_std_3 { get; set; }
        public string result_std_4 { get; set; }
        public int? classId { get; set; }
        public string className { get; set; }

    }

    public class NisaabReceiptModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int ItsId { get; set; }
        public int? FPLogs_Id { get; set; }
        public int Amount { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string Remark { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string Reason { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? MonthId { get; set; }
        public int? EntryId { get; set; }
        public string PaidAt { get; set; }
        public string VenuePrefix { get; set; }
        public DateTime? ChequeDate { get; set; }
        public Nullable<bool> ReverseStatus { get; set; }
        public DateTime? ReverseDate { get; set; }
        public string Account { get; set; }
        public bool IsDraft { get; set; }
        public DateTime? ReceiptDate { get; set; }

        //*********** to provide student name *********
        public string StudentName { get; set; }
        public string CampVenue { get; set; }
    }

    public class NisaabFeesAssociationModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int ItsId { get; set; }
        public int FeesAlotted { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int FeesPaid { get; set; }
        public int DueAmount { get; set; }
        public string Category { get; set; }
        public string Reason { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? MonthId { get; set; }
    }

    public class NisaabFeesAlottmentModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int ItsId { get; set; }
        public string CreatedBy { get; set; }
        public int FeeAlotted { get; set; }
        public string EnumType { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? MonthId { get; set; }
    }

    public class NisaabFeesPaidModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int ItsId { get; set; }
        public string CreatedBy { get; set; }
        public int FeePaidAmount { get; set; }
        public string EnumType { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string PaidAt { get; set; }
        public DateTime? ChequeDate { get; set; }
        public Nullable<bool> ReverseStatus { get; set; }
        public DateTime? ReverseDate { get; set; }
        public int? MonthId { get; set; }
        public string Account { get; set; }
        public bool IsDraft { get; set; }
        public string referenceNumber { get; set; }
        public long ReceiptId { get; set; }
        public int? EntryId { get; set; }
        public string VenuePrefix { get; set; }
        public DateTime? ReceiptDate { get; set; }
    }

}