using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class rights
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int rightsId { get; set; }

    [StringLength(100)]
    public string rightsName { get; set; }
}
