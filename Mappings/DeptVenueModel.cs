namespace mahadalzahrawebapi.Mappings
{
    public class DeptVenueModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public int? qismId { get; set; }

        public string deptName { get; set; }

        public string venueName { get; set; }

        public string masteDeptname { get; set; }



        public IEnumerable<BaseItemModel> baseItems { get; set; }

    }
}
