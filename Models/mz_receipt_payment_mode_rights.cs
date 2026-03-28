using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_receipt_payment_mode_rights
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? paymentModeId { get; set; }
}
