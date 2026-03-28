using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("smartGoalId", Name = "fk_budget_smart_issue_araz_idx")]
public partial class mz_expense_budget_smart_issue_logs
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    public string remark { get; set; }

    public string createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "int(11)")]
    public int smartGoalId { get; set; }

    [Required]
    public bool? isConcerning { get; set; }

    [ForeignKey("smartGoalId")]
    [InverseProperty("mz_expense_budget_smart_issue_logs")]
    public virtual mz_expense_budget_smart_goals smartGoal { get; set; }
}
