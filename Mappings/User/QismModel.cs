namespace mahadalzahrawebapi
{
    public class QismModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? password { get; set; }
        public string emailId { get; set; }

        public BranchUserModel? user { get; set; }
        public List<int>? modules { get; set; }
    }
}
