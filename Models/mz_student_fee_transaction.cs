using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_student_fee_transaction
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? studentId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "text")]
    public string currency { get; set; }

    [Column(TypeName = "int(11)")]
    public int? debit { get; set; }

    [Column(TypeName = "int(11)")]
    public int? credit { get; set; }

    [Column(TypeName = "text")]
    public string paymentMode { get; set; }

    [Column(TypeName = "text")]
    public string remarks { get; set; }

    [Column(TypeName = "int(11)")]
    public int? psetId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? recieptId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? allotmentId { get; set; }

    [Column(TypeName = "text")]
    public string transactionId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? collection_center_no { get; set; }
}
