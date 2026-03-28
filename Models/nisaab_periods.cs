using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class nisaab_periods
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [StringLength(100)]
    public string periodName { get; set; }

    [Column(TypeName = "text")]
    public string timing { get; set; }
}
