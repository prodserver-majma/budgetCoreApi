using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class holiday_hijri_miqaat
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? month_id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? date_id { get; set; }

    [Column(TypeName = "text")]
    public string miqaats_title { get; set; }

    [Column(TypeName = "text")]
    public string miqaats_description { get; set; }

    [Column(TypeName = "int(11)")]
    public int? miqaats_priority { get; set; }
}
