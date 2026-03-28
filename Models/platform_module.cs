using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("buttonId", Name = "fk_platform_module_button_idx")]
[Index("pageId", Name = "fk_platform_module_page_idx")]
public partial class platform_module
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int pageId { get; set; }

    [Column(TypeName = "int(11)")]
    public int buttonId { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool isDefault { get; set; }

    [ForeignKey("buttonId")]
    [InverseProperty("platform_module")]
    public virtual platform_button button { get; set; }

    [ForeignKey("pageId")]
    [InverseProperty("platform_module")]
    public virtual platform_page page { get; set; }

    [InverseProperty("module")]
    public virtual ICollection<platform_user_module> platform_user_module { get; set; } = new List<platform_user_module>();

    [ForeignKey("moduleId")]
    [InverseProperty("module")]
    public virtual ICollection<platform_role> role { get; set; } = new List<platform_role>();
}
