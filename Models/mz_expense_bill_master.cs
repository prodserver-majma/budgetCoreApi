using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mahadalzahrawebapi.Models;

public partial class mz_expense_bill_master
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string billNo { get; set; }

    public DateOnly? billDate { get; set; }

    [Column(TypeName = "int(11)")]
    public int? billAmount { get; set; }

    [Column(TypeName = "int(11)")]
    public int? vendorId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? baseItemId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? deptVenueId { get; set; }

    [Column(TypeName ="int(11)")]
    public int? psetId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? financialYear { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "int(11)")]
    public int? txn_Id { get; set; }

    [Column(TypeName = "text")]
    public string paymentMode_User { get; set; }

    [Column(TypeName = "text")]
    public string paymentMode_Admin { get; set; }

    [Column(TypeName = "text")]
    public string paymentTo_AccNum { get; set; }

    [Column(TypeName = "text")]
    public string paymentTo_AccName { get; set; }

    [Column(TypeName = "text")]
    public string paymentTo_BankName { get; set; }

    [Column(TypeName = "text")]
    public string paymentTo_ifsc { get; set; }

    [Column(TypeName = "text")]
    public string paymentFrom_BankName { get; set; }

    [StringLength(45)]
    public string status { get; set; }

    public bool? isWaived { get; set; }

    [Column(TypeName = "int(11)")]
    public int? packageId { get; set; }

    public float? gstPercentage { get; set; }

    [Column(TypeName = "int(11)")]
    public int? gstAmount { get; set; }

    [Column(TypeName = "int(11)")]
    public int? tdsApplicableAmount { get; set; }

    public float? tdsPercentage { get; set; }

    [Column(TypeName = "int(11)")]
    public int? tdsAmount { get; set; }

    [Column(TypeName = "int(11)")]
    public int? conveyanceAmount { get; set; }

    public bool? isReconciled { get; set; }

    public bool? isFundRequested { get; set; }

    [Column(TypeName = "text")]
    public string billAttachment { get; set; }

    [InverseProperty("bill")]
    public virtual ICollection<mz_expense_bill_item> mz_expense_bill_item { get; set; } = new List<mz_expense_bill_item>();

    [InverseProperty("bill")]
    public virtual ICollection<mz_expense_bill_logs> mz_expense_bill_logs { get; set; } = new List<mz_expense_bill_logs>();
}
