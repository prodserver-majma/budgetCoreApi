using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class printableformat_report
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string description { get; set; }

    [Column(TypeName = "text")]
    public string fileName { get; set; }

    [Column(TypeName = "text")]
    public string reportName { get; set; }
}
