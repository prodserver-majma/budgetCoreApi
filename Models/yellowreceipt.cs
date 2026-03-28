using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class yellowreceipt
{
    [Key]
    [Column(TypeName = "bigint(20)")]
    public long Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int ItsId { get; set; }

    [Column(TypeName = "text")]
    public string Name { get; set; }

    [Column(TypeName = "int(11)")]
    public int Amount { get; set; }

    [Column(TypeName = "text")]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [StringLength(100)]
    public string PaymentMode { get; set; }

    [Column(TypeName = "text")]
    public string BankName { get; set; }

    [StringLength(100)]
    public string ChequeNo { get; set; }

    [StringLength(200)]
    public string PaidAt { get; set; }

    [StringLength(200)]
    public string Account { get; set; }

    [Column(TypeName = "int(11)")]
    public int FinancialYear { get; set; }

    [Column(TypeName = "text")]
    public string Remarks { get; set; }

    [Column(TypeName = "int(11)")]
    public int? EntryId { get; set; }

    [StringLength(45)]
    public string purpose { get; set; }

    public DateOnly? cancelDate { get; set; }

    [Required]
    [StringLength(45)]
    public string status { get; set; }

    public DateOnly? paymentDate { get; set; }

    [StringLength(20)]
    public string whatsappNo { get; set; }

    [StringLength(100)]
    public string email { get; set; }
}
