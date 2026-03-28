using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class qism_al_tahfeez_role_right
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int roleId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? rightId { get; set; }
}
