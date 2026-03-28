using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class kg_venue_worktype
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? venueId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? workTypeId { get; set; }
}
