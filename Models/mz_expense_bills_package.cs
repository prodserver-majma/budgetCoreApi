using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_bills_package
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    [Column(TypeName = "int(11)")]
    public int? amount { get; set; }

    [Column(TypeName = "text")]
    public string description { get; set; }

    public DateOnly? paymentDate { get; set; }
}
