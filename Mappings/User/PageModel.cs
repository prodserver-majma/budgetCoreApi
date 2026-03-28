namespace mahadalzahrawebapi.Mappings.User
{
    public class PageModel
    {
        public int pageId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<ModuleModel> module { get; set; }
    }
}