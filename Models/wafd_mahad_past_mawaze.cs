using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class wafd_mahad_past_mawaze
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsIs { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fromYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? toYear { get; set; }

    [Column(TypeName = "text")]
    public string mauze { get; set; }

    [Column(TypeName = "text")]
    public string program { get; set; }
}
