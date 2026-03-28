using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_procurement_item
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    [Column(TypeName = "text")]
    public string type { get; set; }

    [Column(TypeName = "text")]
    public string uom { get; set; }

    [InverseProperty("item")]
    public virtual ICollection<mz_expense_bill_item> mz_expense_bill_item { get; set; } = new List<mz_expense_bill_item>();

    [InverseProperty("item")]
    public virtual ICollection<mz_expense_budget_araz> mz_expense_budget_araz { get; set; } = new List<mz_expense_budget_araz>();

    // [InverseProperty("item")]
    // public virtual ICollection<mz_expense_budget_araz_monthly> mz_expense_budget_araz_monthly { get; set; } = new List<mz_expense_budget_araz_monthly>();

    [ForeignKey("itemId")]
    [InverseProperty("item")]
    public virtual ICollection<mz_expense_procurement_baseitem> baseItem { get; set; } = new List<mz_expense_procurement_baseitem>();

    //public ICollection<mz_expense_procurement_item_baseitem> ItemBaseItems { get; set; }

}
