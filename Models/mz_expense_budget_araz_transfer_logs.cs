using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("fromArazId", Name = "fk_budget_araz_from_idx")]
[Index("toArazId", Name = "fk_budget_araz_to_idx")]
public partial class mz_expense_budget_araz_transfer_logs
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int fromArazId { get; set; }

    [Column(TypeName = "int(11)")]
    public int toArazId { get; set; }

    [Column(TypeName = "int(11)")]
    public int amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime createdOn { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string remarks { get; set; }

    [Column(TypeName = "text")]
    public string trabferModel { get; set; }

    [ForeignKey("fromArazId")]
    [InverseProperty("mz_expense_budget_araz_transfer_logsfromAraz")]
    public virtual mz_expense_budget_araz fromAraz { get; set; }

    [ForeignKey("toArazId")]
    [InverseProperty("mz_expense_budget_araz_transfer_logstoAraz")]
    public virtual mz_expense_budget_araz toAraz { get; set; }
}
