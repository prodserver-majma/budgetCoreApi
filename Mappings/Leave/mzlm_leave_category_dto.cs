namespace mahadalzahrawebapi.Mappings.Leave;
public partial class mzlm_leave_category_dto
{
    public int id { get; set; }

    public string name { get; set; }

    public int leaveTypeId { get; set; }

    public int maxAllowed { get; set; }

    public int consicutiveLimit { get; set; }

    public bool isHijri { get; set; }

    public int minApplicationDate { get; set; }

    public bool isDeductable { get; set; }

    public bool isRepeated { get; set; }

    public bool isCarryForward { get; set; }

    public string notifyTo { get; set; }

    public bool? active { get; set; }

    public float consumedLeaves { get; set; }
}
