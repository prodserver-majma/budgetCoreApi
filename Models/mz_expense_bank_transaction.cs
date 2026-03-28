using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_bank_transaction
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string bankName { get; set; }

    [Column(TypeName = "int(11)")]
    public int? credit { get; set; }

    [Column(TypeName = "int(11)")]
    public int? debit { get; set; }

    [Column(TypeName = "text")]
    public string paymentMode { get; set; }

    [Column(TypeName = "int(11)")]
    public int? paymentId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? transactionId { get; set; }

    [Column(TypeName = "text")]
    public string remarks { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }
}
