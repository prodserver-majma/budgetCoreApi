using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("billId", Name = "fk_bill_logs_bill_master_idx")]
public partial class mz_expense_bill_logs
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? billId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [StringLength(45)]
    public string status { get; set; }

    [Column(TypeName = "text")]
    public string description { get; set; }

    [ForeignKey("billId")]
    [InverseProperty("mz_expense_bill_logs")]
    public virtual mz_expense_bill_master bill { get; set; }
}
