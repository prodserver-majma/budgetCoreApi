using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class greg_months
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "varchar(10)")]
    public string slug { get; set; }

    [Column(TypeName = "text")]
    public string month_name { get; set; }
}
