using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_pset_elqgrpid_mapping
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? pSetId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? elqId { get; set; }
}
