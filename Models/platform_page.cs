using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class platform_page
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [StringLength(45)]
    public string pageName { get; set; }

    [StringLength(75)]
    public string description { get; set; }

    [StringLength(150)]
    public string link { get; set; }

    [StringLength(45)]
    public string icon { get; set; }

    [InverseProperty("page")]
    public virtual ICollection<platform_module> platform_module { get; set; } = new List<platform_module>();
}
