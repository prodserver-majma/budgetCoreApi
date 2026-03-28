
namespace mahadalzahrawebapi.Mappings
{
    public class branch_user_dto
    {
        public int itsId { get; set; }

        public string? password { get; set; }

        public string? emailId { get; set; }

        public DateTime? lastLoggedIn { get; set; }

        public virtual ICollection<platform_user_module_dto> platform_user_modules { get; set; } = new List<platform_user_module_dto>();

        public virtual ICollection<platform_user_role_dto> platform_user_roles { get; set; } = new List<platform_user_role_dto>();

        public virtual qism_al_tahfeez_dto qism_al_tahfeez { get; set; }

        public virtual ICollection<dept_venue_dto> deptVenues { get; set; } = new List<dept_venue_dto>();

        public virtual ICollection<registrationform_dropdown_set_dto> psets { get; set; } = new List<registrationform_dropdown_set_dto>();
    }
}
