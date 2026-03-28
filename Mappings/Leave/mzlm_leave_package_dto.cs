namespace mahadalzahrawebapi.Mappings
{
    public class mzlm_leave_package_dto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int stageId { get; set; }
        public string? purpose { get; set; }
        public string appliedBy { get; set; }
        public string typeName { get; set; }
        public string categoryName { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime fromEngDate { get; set; }
        public DateTime toDate { get; set; }
        public DateTime toEngDate { get; set; }
        public float totalDays { get; set; }
        public string stageName { get; set; }
        public bool morningShift { get; set; }
        public bool eveningShift { get; set; }
        public int[] qismIds { get; set; }
        public String qismName { get; set; }
        public string? approvalStages { get; set; }
        public string? reason { get; set; }
        public int remainingDays { get; set; }
        public bool? onLeave { get; set; }
        public bool? isDeductable { get; set; }

        public string? UploadedFileBase64 { get; set; }
        public string? UploadedFileName { get; set; }

        public string? UploadedDocumentUrl { get; set; }
    }
}
