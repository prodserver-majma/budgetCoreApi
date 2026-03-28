using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_budget_transfer_logs
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fromDeptVenueId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fromBaseItemId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? toDeptVenueId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? toBaseItemId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "text")]
    public string remarks { get; set; }
}
