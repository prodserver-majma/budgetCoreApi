namespace mahadalzahrawebapi.Mappings;

public partial class mzlm_leave_type_dto
{
    public int id { get; set; }

    public string name { get; set; }

    public string accessTo { get; set; }

    public int daysAllotted { get; set; }

    public string approvalFlow { get; set; }

    public string applicableTo { get; set; }

    public bool? active { get; set; }

    public float consumedLeaves { get; set; }

}

public partial class mzlm_leave_summary_type_info
{
    public int id { get; set; }

    public string name { get; set; }

    public float consumedLeaves { get; set; }
}

public partial class mzlm_Individual_leave_summary_info
{
    public int its { get; set; }

    public string name { get; set; }

    public float totalConsumedLeaves { get; set; }

    public List<mzlm_leave_summary_type_info> LeaveTypesData { get; set; }
    public List<string> LeaveTypeHeaders { get; set; }

    public int acedemicYear { get; set; }
}

public partial class mzlm_leave_summary_info
{
    public int its { get; set; }

    public string name { get; set; }

    public float totalConsumedLeaves { get; set; }

    public List<mzlm_leave_summary_type_info> LeaveTypesData { get; set; }
    public List<string> LeaveTypeHeaders { get; set; }

    public int acedemicYear { get; set; }
}