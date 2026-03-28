namespace mahadalzahrawebapi.Mappings;

public partial class leaveBulkAssignation
{
    public string? itsCsv { get; set; }
    public List<int>? mauze { get; set; }
    public List<string>? mzCategory { get; set; }
}

public partial class mzlm_leave_application_dto
{
    public int id { get; set; }

    public int itsId { get; set; }

    public int typeId { get; set; }

    public int categoryId { get; set; }

    public int fromDayId { get; set; }

    public int fromMonthId { get; set; }

    public int toDayId { get; set; }

    public int toMonthId { get; set; }

    public bool morningShift { get; set; }

    public bool eveningShift { get; set; }

    public int shiftCount { get; set; }

    public int hijrAcademicYear { get; set; }

    public int stageId { get; set; }
    public int toYear { get; set; }
    public int fromYear { get; set; }
    public int? packageId { get; set; }

    public int venueId { get; set; }

    public string appliedBy { get; set; }

    public int createdBy { get; set; }

    public DateTime createdOn { get; set; }
    public DateTime fromEngDate { get; set; }
    public DateTime toEngDate { get; set; }
    public DateTime updatedOn { get; set; }

    public string? UploadedFileBase64 { get; set; }
    public string? UploadedFileName { get; set; }

    public string? UploadedDocumentUrl { get; set; }

    public string? purpose { get; set; }

    public leaveBulkAssignation? leaveBulkAssignation { get; set; }

    public string? name { get; set; }
    public string? mzIdara { get; set; }
    public string? mauze { get; set; }
    public string? categoryName { get; set; }
    public string? typeName { get; set; }

    public string? stageName { get; set; }
    public string? qismName { get; set; }
    public int qismId { get; set; }
    public string? toDate { get; set; }
    public string? fromDate { get; set; }
    public string? approvalStages { get; set; }
    public string? reason { get; set; }
    public bool? notifyBranch { get; set; }
    public bool? notifyIndividual { get; set; }
    public bool? notifyAdmin { get; set; }
    public bool? onLeave { get; set; }
    public int remainingDays { get; set; }
    public bool? isDeductable { get; set; }

}
