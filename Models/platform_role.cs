using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class platform_role
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [StringLength(45)]
    public string name { get; set; }

    [StringLength(100)]
    public string description { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool isDefault { get; set; }

    [StringLength(45)]
    public string icon { get; set; }

    [StringLength(150)]
    public string link { get; set; }

    [InverseProperty("role")]
    public virtual ICollection<platform_user_role> platform_user_role { get; set; } = new List<platform_user_role>();

    [ForeignKey("subRole")]
    [InverseProperty("subRole")]
    public virtual ICollection<platform_role> mainRole { get; set; } = new List<platform_role>();

    [ForeignKey("roleId")]
    [InverseProperty("role")]
    public virtual ICollection<platform_module> module { get; set; } = new List<platform_module>();

    [ForeignKey("mainRole")]
    [InverseProperty("mainRole")]
    public virtual ICollection<platform_role> subRole { get; set; } = new List<platform_role>();
}
