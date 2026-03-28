using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_vendor_master
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column (TypeName = "int(11)")]
    public int schoolId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? deptVenueId {get; set;}

    [Column(TypeName = "int(11)")]

    public int? psetId { get; set; }

    [Column(TypeName = "text")]
    public string? name { get; set; }

    [Column(TypeName = "text")]
    public string? phoneNo { get; set; }

    [Column(TypeName = "text")]
    public string? mobileNo { get; set; }

    [Column(TypeName = "text")]
    public string? whatsappNo { get; set; }

    [Column(TypeName = "text")]
    public string? address { get; set; }

    [Column(TypeName = "text")]
    public string? state { get; set; }

    [Column(TypeName = "text")]
    public string? city { get; set; }

    [Column(TypeName = "text")]
    public string? ifscCode { get; set; }

    [Column(TypeName = "text")]
    public string? bankName { get; set; }

    [Column(TypeName = "text")]
    public string? accountNo { get; set; }

    [Column(TypeName = "text")]
    public string? accountName { get; set; }

    [Column(TypeName = "text")]
    public string? panCardNo { get; set; }

    //[Column(TypeName = "int(11)")]
    //public int userItsId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string? createdBy { get; set; }

    public bool? status { get; set; }

    [Column(TypeName = "text")]
    public string? type { get; set; }

    [StringLength(60)]
    public string? email { get; set; }

    [StringLength(15)]
    public string? gstNumber { get; set; }

    [Column(TypeName = "text")]
    public string? panCardAttachment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? updatedOn { get; set; }

    [Column(TypeName = "int(11)")]
    public int? updatedBy { get; set; }
}
