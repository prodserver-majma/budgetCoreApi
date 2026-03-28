using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_vendor_wallet
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? vendorId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? debit { get; set; }

    [Column(TypeName = "int(11)")]
    public int? credit { get; set; }

    [Column(TypeName = "text")]
    public string currency { get; set; }

    [Column(TypeName = "text")]
    public string paymentType { get; set; }

    public bool? status { get; set; }

    [Column(TypeName = "text")]
    public string note { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }
}
