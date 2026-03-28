namespace mahadalzahrawebapi
{
    public class BranchUserModel
    {
        public int id { get; set; }
        public string? userName { get; set; }
        public int itsId { get; set; }
        public string? password { get; set; }
        public string? emailId { get; set; }
        public string? mobile { get; set; }
        public string? employeeType { get; set; }
        public string? mz_idara { get; set; }
        public string? designation { get; set; }
        public string? mauze { get; set; }
        public int? roleId { get; set; }
        public List<QismModel>? qism { get; set; }
        public List<int>? modules { get; set; }
    }

    public class UserDeptAssociationModel
    {
        public long Id { get; set; }
        public int DID { get; set; }
        public string Idara { get; set; }
        public string Department { get; set; }
        public string DisplayName { get; set; }
        public string DepartmentCode { get; set; }
    }

}
