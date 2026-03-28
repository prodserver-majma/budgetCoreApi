using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_vendor_payment
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? vendorId { get; set; }

    public DateOnly? paymentDate { get; set; }

    [Column(TypeName = "int(11)")]
    public int? debit { get; set; }

    [Column(TypeName = "int(11)")]
    public int? credit { get; set; }

    [Column(TypeName = "text")]
    public string paymentMode { get; set; }

    [Column(TypeName = "text")]
    public string currency { get; set; }

    [Column(TypeName = "text")]
    public string status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "text")]
    public string note { get; set; }

    [StringLength(45)]
    public string chequeDate { get; set; }

    [Column(TypeName = "text")]
    public string transactionId { get; set; }
}
