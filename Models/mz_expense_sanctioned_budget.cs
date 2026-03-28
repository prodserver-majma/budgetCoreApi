using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_sanctioned_budget
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? deptVenueId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? baseItemId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? user_arazAmount { get; set; }

    [Column(TypeName = "int(11)")]
    public int? admin_arazAmount { get; set; }

    [Column(TypeName = "int(11)")]
    public float? sanctioned_amount { get; set; }

    [Column(TypeName = "int(11)")]
    public int? financialYear { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? updatedOn { get; set; }

    [Column(TypeName = "text")]
    public string updatedBy { get; set; }
}
