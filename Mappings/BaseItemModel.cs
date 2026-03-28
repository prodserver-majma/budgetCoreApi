namespace mahadalzahrawebapi.Mappings
{
    public class BaseItemModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public bool status { get; set; }

        public int? deptId { get; set; }

        public int? psetId { get; set; }

        public int deptMappingId { get; set; }
    }
}
