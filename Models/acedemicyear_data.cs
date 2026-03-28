using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class acedemicyear_data
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fromday_hijri { get; set; }

    [Column(TypeName = "int(11)")]
    public int? frommonth_hijri { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fromyear_hijri { get; set; }

    [Column(TypeName = "int(11)")]
    public int? today_hijri { get; set; }

    [Column(TypeName = "int(11)")]
    public int? tomonth_hijri { get; set; }

    [Column(TypeName = "int(11)")]
    public int? toyear_hijri { get; set; }

    [Column(TypeName = "int(11)")]
    public int? acedemicYear { get; set; }

    [StringLength(200)]
    public string acedemicName { get; set; }
}
