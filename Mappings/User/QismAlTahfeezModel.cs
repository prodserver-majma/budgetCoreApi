using mahadalzahrawebapi.Models;

namespace mahadalzahrawebapi
{
    public class QismAlTahfeezModel
    {
        public BranchUserModel user { get; set; }
        public List<DeptVenueRightModel> dv { get; set; }
        public List<ReportsRightsModel> rr { get; set; }
        public List<ModulePageModel>? pageModels { get; set; }
    }

    public class QismAlTahfeezModel2
    {
        public QismModel user { get; set; }
        public List<DeptVenueRightModel> dv { get; set; }
        public List<ReportsRightsModel> rr { get; set; }
        public List<platform_user_role>? role { get; set; }
        public List<platform_user_module>? module { get; set; }
        public List<ModulePageModel>? pageModels { get; set; }
    }
}
