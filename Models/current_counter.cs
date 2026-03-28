using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class current_counter
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [StringLength(45)]
    public string name { get; set; }

    [Column(TypeName = "int(11)")]
    public int count { get; set; }
}
