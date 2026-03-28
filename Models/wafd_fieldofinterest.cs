using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class wafd_fieldofinterest
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [Column(TypeName = "text")]
    public string fieldofInterest { get; set; }

    [Column(TypeName = "text")]
    public string selfRanking { get; set; }
}
