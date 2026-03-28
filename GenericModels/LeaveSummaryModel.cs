namespace mahadalzahrawebapi.GenericModels
{
    public class LeaveSummaryModel
    {
        public List<leaveApplicant> rowHeaders { get; set; }
        public List<leaveTypeHeader> colHeader { get; set; }
        public List<LeaveApplicationData> details { get; set; }

    }
    public class leaveTypeHeader
    {
        public string name { get; set; }
        public string color { get; set; }
        public int id { get; set; }
        public int daysAllotted { get; set; }

    }

    public class leaveApplicant
    {
        public string name { get; set; }
        public int itsId { get; set; }
        public string email { get; set; }
        public string contactNum { get; set; }
        public string mauze { get; set; }
        public string mzIdara { get; set; }
        public float? totalConsumed { get; set; }
    }

    public class LeaveApplicationData
    {
        public int itsId { get; set; }


        public List<leaveTypeStats> data { get; set; }
    }

    public class leaveTypeStats
    {
        public int typeId { get; set; }
        public float? applied { get; set; }
        public float? rejected { get; set; }
        public float? consumed { get; set; }
        public float? Cacelled { get; set; }
        public float? remaining { get; set; }

    }
}
