namespace mahadalzahrawebapi.Mappings.Leave;
public partial class mzlm_leave_package_logs_dto
{
    public int id { get; set; }

    public int leaveId { get; set; }
    public string remark { get; set; }

    public int stageId { get; set; }

    public DateTime createdOn { get; set; }

    public int createdBy { get; set; }

    public string? createdByName { get; set; }
    public string? stageName { get; set; }
}
