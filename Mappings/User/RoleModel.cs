
namespace mahadalzahrawebapi.Mappings.User
{
    public class RoleModel
    {
        public List<ModuleModel> module;
        public string icon;
        public List<PageModel> pages;

        public string description { get; set; }
        public bool isDefault { get; set; }
        public string name { get; set; }
        public int roleId { get; set; }
        public bool isSelected { get; set; }
    }
}