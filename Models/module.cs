using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class module
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int moduleId { get; set; }

    [StringLength(100)]
    public string moduleName { get; set; }
}
