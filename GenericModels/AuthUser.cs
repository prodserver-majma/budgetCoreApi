namespace mahadalzahrawebapi.GenericModels
{
    public class AuthUser
    {
        public long Id { get; set; }
        public int qismId { get; set; }
        public string Name { get; set; }
        public int ItsId { get; set; }
        public string Department { get; set; }
        public string DisplayName { get; set; }
        public string DepartmentCode { get; set; }
        public string DID { get; set; }
        public int DeptVenueId { get; set; }
        public string DeptVenueName { get; set; }
        public string ipAddress { get; set; }
        public string deviceDetails { get; set; }
        public long logId { get; set; }
        public string loginName { get; set; }
    }
}
