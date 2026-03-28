using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_student_receipt
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? studentId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? recieptNumber { get; set; }

    public DateOnly? recieptDate { get; set; }

    [Column(TypeName = "int(11)")]
    public int? collectionCenter { get; set; }

    [Column(TypeName = "text")]
    public string paymentMode { get; set; }

    [StringLength(45)]
    public string currency { get; set; }

    [Column(TypeName = "int(11)")]
    public int? amount { get; set; }

    [Column(TypeName = "text")]
    public string status { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "int(11)")]
    public int? psetId { get; set; }

    [Column(TypeName = "text")]
    public string transactionId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string bankName { get; set; }

    [Column(TypeName = "text")]
    public string account { get; set; }

    [Column(TypeName = "text")]
    public string note { get; set; }

    [Column(TypeName = "text")]
    public string recieptUrl { get; set; }

    public DateOnly? chequeDate { get; set; }
}
