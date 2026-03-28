using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class hijri_calender
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? hijri_day { get; set; }

    [Column(TypeName = "int(11)")]
    public int? hijri_month { get; set; }

    [Column(TypeName = "int(11)")]
    public int? hijri_year { get; set; }

    [Column(TypeName = "int(11)")]
    public int? english_day { get; set; }

    [Column(TypeName = "int(11)")]
    public int? english_month { get; set; }

    [Column(TypeName = "int(11)")]
    public int? english_year { get; set; }

    [InverseProperty("holiday")]
    public virtual ICollection<holiday_allocation> holiday_allocation { get; set; } = new List<holiday_allocation>();
}
