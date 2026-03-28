using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_procurement_baseitem
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    public bool? status { get; set; }

    public bool isCapital { get; set; }

    public bool isIncome { get; set; }
    public bool? isNonOpIncome { get; set; }
    public bool? isDawatReceipt {  get; set; }
    public bool? isOtherReceipt { get; set; }
    public bool? isOtherPayment {  get; set; }

    [InverseProperty("baseItem")]
    public virtual ICollection<dept_venue_baseitem> dept_venue_baseitem { get; set; } = new List<dept_venue_baseitem>();

    [InverseProperty("baseItem")]
    public virtual ICollection<mz_expense_budget_araz> mz_expense_budget_araz { get; set; } = new List<mz_expense_budget_araz>();

    // [InverseProperty("baseItem")]
    // public virtual ICollection<mz_expense_budget_araz_monthly> mz_expense_budget_araz_monthly { get; set; } = new List<mz_expense_budget_araz_monthly>();

    [ForeignKey("baseItemId")]
    [InverseProperty("baseItem")]
    public virtual ICollection<mz_expense_procurement_item> item { get; set; } = new List<mz_expense_procurement_item>();

    //public virtual ICollection<mz_expense_procurement_item_baseitem> ItemBaseItems { get; set; }

}
