using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class nisaab_jadwal
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? periodId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? classId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? dayId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? subjectId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? teacherId { get; set; }
}
