using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class printableformat_report_rights
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? reportId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }
}
