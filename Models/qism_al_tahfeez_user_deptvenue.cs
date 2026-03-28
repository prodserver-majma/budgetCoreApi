namespace mahadalzahrawebapi.Models
{
    public class qism_al_tahfeez_user_deptvenue
    {
        public int userId { get; set; }
        public int deptVenueId { get; set; }

        public branch_user branch_user { get; set; }
        public dept_venue deptVenue { get; set; }
    }

}
