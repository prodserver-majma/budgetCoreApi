namespace mahadalzahrawebapi.Mappings
{
    public class EmployeeBankDetailsModel
    {
        public int id { get; set; }
        public int itsId { get; set; }
        public string? bankName { get; set; }
        public string? bankAccountNumber { get; set; }
        public string? bankAccountName { get; set; }
        public string? domesticCode { get; set; }
        public string? internationalCodeType { get; set; }
        public string? internationalCode { get; set; }
        public string? country { get; set; }
        public string? ifsc { get; set; }
        public string? bankBranch { get; set; }
        public string? bankAccountType { get; set; }
        public string? chequeAttachment { get; set; }
        public string? chequeAttachmentFileName { get; set; }
        public string? panCard { get; set; }
        public string? panCardAttachment { get; set; }
        public string? panCardAttachmentFileName { get; set; }
        public bool isDefault { get; set; }
    }
}
