using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class wafd_languageproficiency
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [Column(TypeName = "text")]
    public string language { get; set; }

    [Column(TypeName = "int(11)")]
    public int? selfRanking { get; set; }

    [Column(TypeName = "text")]
    public string speaking { get; set; }

    [Column(TypeName = "text")]
    public string reading { get; set; }

    [Column(TypeName = "text")]
    public string writing { get; set; }

    [Column(TypeName = "text")]
    public string listening { get; set; }
}
