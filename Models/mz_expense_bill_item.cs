using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("itemId", Name = "fk_bill_item_bill_item_master_idx")]
[Index("billId", Name = "fk_bill_item_bill_master_idx")]
public partial class mz_expense_bill_item
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? billId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itemId { get; set; }

    public float? quantity { get; set; }

    public float? amountPerPc { get; set; }

    [Column(TypeName = "text")]
    public string remarks { get; set; }

    public float? gstPercentage { get; set; }

    public float? gstAmount { get; set; }

    [ForeignKey("billId")]
    [InverseProperty("mz_expense_bill_item")]
    public virtual mz_expense_bill_master bill { get; set; }

    [ForeignKey("itemId")]
    [InverseProperty("mz_expense_bill_item")]
    public virtual mz_expense_procurement_item item { get; set; }
}
