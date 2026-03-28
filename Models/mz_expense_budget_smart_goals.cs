using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("deptVenueId", Name = "fk_budget_deptvenue_idx")]
[Index("itsId", Name = "fk_budget_smart_kg_idx")]
public partial class mz_expense_budget_smart_goals
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int deptVenueId { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [StringLength(75)]
    public string category { get; set; }

    public string specific { get; set; }

    public string measearable { get; set; }

    public string attainable { get; set; }

    public string relevant { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? timeStart { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? timeEnd { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string remarks_admin { get; set; }

    [Column(TypeName = "text")]
    public string updatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? updatedOn { get; set; }

    [Column(TypeName = "int(11)")]
    public int financialYear { get; set; }

    [Required]
    [StringLength(45)]
    public string stage { get; set; }

    [ForeignKey("deptVenueId")]
    [InverseProperty("mz_expense_budget_smart_goals")]
    public virtual dept_venue deptVenue { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("mz_expense_budget_smart_goals")]
    public virtual khidmat_guzaar its { get; set; }

    [InverseProperty("smartGoal")]
    public virtual ICollection<mz_expense_budget_smart_issue_logs> mz_expense_budget_smart_issue_logs { get; set; } = new List<mz_expense_budget_smart_issue_logs>();
}
