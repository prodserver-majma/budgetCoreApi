using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class qism_al_tahfeez_user
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? qismId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? userId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? roleId { get; set; }

    public bool? isAdmin { get; set; }
}
