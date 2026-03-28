using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("baseItemId", Name = "fk_budget_araz_baseItem_idx")]
[Index("itemId", Name = "fk_budget_araz_items_idx")]
[Index("deptVenueId", Name = "fk_budget_deptvenue_idx")]
[Index("deptVenueId", "baseItemId", "itemId", "financialYear", Name = "secondary", IsUnique = true)]
public partial class mz_expense_budget_araz
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? venueId { get; set; }

    [Column(TypeName = "int(11)")]
    public int deptVenueId { get; set; }

    [Column(TypeName ="int(11)")]
    public int? psetId { get; set; }

    [Column(TypeName = "int(11)")]
    public int baseItemId { get; set; }

    [Column(TypeName = "int(11)")]
    public int itemId { get; set; }

    [Column(TypeName = "float")]
    public float amountPerUom { get; set; }

    [Column(TypeName = "int(11)")]
    public int quantity { get; set; }

    [Column(TypeName = "float")]
    public float? amountPerQuantity { get; set; }

    [StringLength(45)]
    public string uom { get; set; }

    [Column(TypeName = "text")]
    public string justification { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "text")]
    public string remarks_admin { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? updatedOn { get; set; }

    [Column(TypeName = "text")]
    public string updatedBy { get; set; }

    [Column(TypeName = "int(11)")]
    public int financialYear { get; set; }

    [Required]
    [StringLength(45)]
    public string stage { get; set; }

    public float? consumedAmount { get; set; }

    public float? consumedQty { get; set; }

    [Column(TypeName = "int(11)")]
    public int? transferedAmount { get; set; }

    [ForeignKey("baseItemId")]
    [InverseProperty("mz_expense_budget_araz")]
    public virtual mz_expense_procurement_baseitem baseItem { get; set; }

    [ForeignKey("deptVenueId")]
    [InverseProperty("mz_expense_budget_araz")]
    public virtual dept_venue deptVenue { get; set; }

    [ForeignKey("itemId")]
    [InverseProperty("mz_expense_budget_araz")]
    public virtual mz_expense_procurement_item item { get; set; }

    [InverseProperty("fromAraz")]
    public virtual ICollection<mz_expense_budget_araz_transfer_logs> mz_expense_budget_araz_transfer_logsfromAraz { get; set; } = new List<mz_expense_budget_araz_transfer_logs>();

    [InverseProperty("toAraz")]
    public virtual ICollection<mz_expense_budget_araz_transfer_logs> mz_expense_budget_araz_transfer_logstoAraz { get; set; } = new List<mz_expense_budget_araz_transfer_logs>();

    [InverseProperty("budgetAraz")]
    public virtual ICollection<mz_expense_budget_issue_logs> mz_expense_budget_issue_logs { get; set; } = new List<mz_expense_budget_issue_logs>();

    [InverseProperty("budgetArazMonthly")]
    public virtual ICollection<mz_expense_budget_araz_monthly> mz_expense_budget_araz_monthly { get; set; } = new List<mz_expense_budget_araz_monthly>();

    // [InverseProperty("budget_id")]
    // public virtual ICollection<mz_expense_budget_araz_monthly> mz_expense_budget_araz_monthly { get; set; } = new List<mz_expense_budget_araz_monthly>();
}
