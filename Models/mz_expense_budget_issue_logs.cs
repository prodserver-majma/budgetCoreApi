using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("budgetArazId", Name = "fk_budget_issue_araz_idx")]
public partial class mz_expense_budget_issue_logs
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string remark { get; set; }

    public string createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "int(11)")]
    public int budgetArazId { get; set; }

    [Required]
    public bool? isConcerning { get; set; }

    public string arazState { get; set; }

    [ForeignKey("budgetArazId")]
    [InverseProperty("mz_expense_budget_issue_logs")]
    public virtual mz_expense_budget_araz budgetAraz { get; set; }
}
