using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

/// <summary>
/// 		
/// </summary>
public partial class mz_kg_wajebaat_araz
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? hijriYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? niyyatAmount { get; set; }

    public float? takhmeenAmount { get; set; }

    [Column(TypeName = "int(11)")]
    public int? paidAmount { get; set; }

    [StringLength(45)]
    public string currency { get; set; }

    [Column(TypeName = "text")]
    public string bankName { get; set; }

    [Column(TypeName = "text")]
    public string draftNo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? draftDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? updatedOn { get; set; }

    [Column(TypeName = "text")]
    public string updatedBy { get; set; }

    [Column(TypeName = "text")]
    public string userRemarks { get; set; }

    [Column(TypeName = "text")]
    public string officeRemarks { get; set; }

    [StringLength(45)]
    public string displayCurrency { get; set; }

    public float? currencyRate { get; set; }

    [StringLength(45)]
    public string wajebaatType { get; set; }

    [StringLength(45)]
    public string stage { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? verifiedOn { get; set; }
}
