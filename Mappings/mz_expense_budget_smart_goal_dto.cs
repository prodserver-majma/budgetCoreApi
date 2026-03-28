
namespace mahadalzahrawebapi.Mappings
{
    public class mz_expense_budget_smart_goal_dto
    {
        public int id { get; set; }

        public int deptVenueId { get; set; }

        public int itsId { get; set; }

        public string? category { get; set; }

        public string? specific { get; set; }

        public string? measearable { get; set; }

        public string? attainable { get; set; }

        public string? relevant { get; set; }

        public DateTime? timeStart { get; set; }

        public DateTime? timeEnd { get; set; }

        public DateTime? createdOn { get; set; }

        public string? remarks_admin { get; set; }

        public string? updatedBy { get; set; }

        public DateTime? updatedOn { get; set; }

        public int financialYear { get; set; }

        public string? stage { get; set; }

        public virtual dept_venue_dto deptVenue { get; set; }

        public virtual ICollection<mz_expense_budget_smart_issue_log_dto> mz_expense_budget_smart_issue_logs { get; set; } = new List<mz_expense_budget_smart_issue_log_dto>();
    }
}
