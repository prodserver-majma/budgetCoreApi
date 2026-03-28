using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class export_type_displayheader
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? typeId { get; set; }

    [Column(TypeName = "text")]
    public string actualName { get; set; }

    [Column(TypeName = "text")]
    public string displayName { get; set; }
}
