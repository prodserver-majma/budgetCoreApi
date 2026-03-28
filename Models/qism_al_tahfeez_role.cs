using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class qism_al_tahfeez_role
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    [Column(TypeName = "text")]
    public string description { get; set; }

    [Column(TypeName = "int(11)")]
    public int? qismId { get; set; }
}
