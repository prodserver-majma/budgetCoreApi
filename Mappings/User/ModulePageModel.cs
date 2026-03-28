namespace mahadalzahrawebapi
{
    public class ModulePageModel
    {
        public int pageId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string icon { get; set; }

        public List<ModuleModel> modules { get; set; }
    }
}
