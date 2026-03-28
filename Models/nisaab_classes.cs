using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class nisaab_classes
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? std { get; set; }

    [StringLength(45)]
    public string div { get; set; }

    [StringLength(45)]
    public string gender { get; set; }

    [StringLength(45)]
    public string className { get; set; }

    public bool? showTimeTable { get; set; }
}
