namespace mahadalzahrawebapi
{
    public class RoleRightsModel
    {
        public int moduleId { get; set; }
        public string moduleName { get; set; }
        public bool moduleRight { get; set; }
        public List<ModuleSubRightsModel> subRights { get; set; }
    }
}
