namespace mahadalzahrawebapi.Mappings
{
    public class ItemRequisitionModel
    {

        public string baseItemName { get; set; }
        public string itemName { get; set; }
        public int? baseItemId { get; set; }
        public int? itemId { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string uom { get; set; }
        public string status { get; set; }
        public List<DeptVenueRightModel> deptVenue { get; set; }


    }
}
