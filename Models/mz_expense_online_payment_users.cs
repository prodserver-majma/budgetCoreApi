using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_online_payment_users
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName ="int(11)")]
    public int schoolId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? deptVenueId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? psetId { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    [Column(TypeName = "text")]
    public string ifsc { get; set; }

    [Column(TypeName = "text")]
    public string accNum { get; set; }

    [Column(TypeName = "text")]
    public string accName { get; set; }

    [Column(TypeName = "text")]
    public string bankName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime createdOn { get; set; }

    [Column(TypeName = "int(11)")]
    public int createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? updatedOn { get; set; }

    [Column(TypeName = "int(11)")]
    public int? updatedBy { get; set; }
}
