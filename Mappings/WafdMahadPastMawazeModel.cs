namespace mahadalzahrawebapi.Mappings
{
    public class WafdMahadPastMawazeModel
    {

        public int id { get; set; }
        public int? itsIs { get; set; }
        public int? fromYear { get; set; }
        public int? toYear { get; set; }
        public string mauze { get; set; }
        public string program { get; set; }

        public List<string> programList { get; set; }
    }
}
