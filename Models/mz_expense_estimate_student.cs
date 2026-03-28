using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_estimate_student
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName ="int(11)")]
    public int sfcp_id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? psetId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fcId { get; set; }

    [Column(TypeName = "int(11)")]
    public float? feesAmount { get; set; }

    [Column(TypeName = "int(11)")]
    public int? studentCountPerMonth { get; set; }

    [Column(TypeName = "int(11)")]
    public int? financialYear { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "int(11)")]
    public float duration { get; set; }

    [Required]
    [StringLength(45)]
    public string stage { get; set; }

    [Column(TypeName = "text")]
    public string remarks_admin { get; set; }

    [Column(TypeName = "text")]
    public string remarks { get; set; }

    [InverseProperty("estimateStudent")]
    public virtual ICollection<mz_expense_student_budget_issue_logs> mz_expense_student_budget_issue_logs { get; set; } = new List<mz_expense_student_budget_issue_logs>();

}
