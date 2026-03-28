namespace mahadalzahrawebapi
{
    public class ReportsRightsModel
    {
        public int rId { get; set; }
        public string? reportName { get; set; }
        public bool right { get; set; }
        public bool? isTagged { get; set; }
        public int? qismId { get; set; }
    }
}
