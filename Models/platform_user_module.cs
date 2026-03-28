using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[PrimaryKey("userId", "moduleId", "qismId")]
[Index("userId", Name = "fk_paltform_user_module_kg_idx")]
[Index("moduleId", Name = "fk_platform_user_module_mid_idx")]
[Index("qismId", Name = "fk_platform_user_module_qism_idx")]
public partial class platform_user_module
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int userId { get; set; }

    [Key]
    [Column(TypeName = "int(11)")]
    public int moduleId { get; set; }

    [Key]
    [Column(TypeName = "int(11)")]
    public int qismId { get; set; }

    [ForeignKey("moduleId")]
    [InverseProperty("platform_user_module")]
    public virtual platform_module module { get; set; }

    [ForeignKey("qismId")]
    [InverseProperty("platform_user_module")]
    public virtual qism_al_tahfeez qism { get; set; }

    [ForeignKey("userId")]
    [InverseProperty("platform_user_module")]
    public virtual branch_user user { get; set; }
}
