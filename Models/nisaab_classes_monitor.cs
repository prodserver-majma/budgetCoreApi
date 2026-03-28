using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class nisaab_classes_monitor
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? monitorItsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? classId { get; set; }
}
