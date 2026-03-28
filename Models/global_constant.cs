using System.ComponentModel.DataAnnotations;

namespace mahadalzahrawebapi.Models;

public partial class global_constant
{
    [Key]
    [StringLength(45)]
    public string key { get; set; }

    [StringLength(45)]
    public string value { get; set; }
}
