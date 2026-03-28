using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class platform_button
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [StringLength(45)]
    public string name { get; set; }

    [InverseProperty("button")]
    public virtual ICollection<platform_module> platform_module { get; set; } = new List<platform_module>();
}
