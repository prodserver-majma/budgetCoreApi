using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class wafdprofile_qualification_stage_degree
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string stage { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string degree { get; set; }
}
