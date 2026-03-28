using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[PrimaryKey("userId", "roleId", "qismId")]
[Index("userId", Name = "fk_paltform_user_role_kg_idx")]
[Index("roleId", Name = "fk_platform_user_role_mid_idx")]
[Index("qismId", Name = "fk_platform_user_role_qism_idx")]
public partial class platform_user_role
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int userId { get; set; }

    [Key]
    [Column(TypeName = "int(11)")]
    public int roleId { get; set; }

    [Key]
    [Column(TypeName = "int(11)")]
    public int qismId { get; set; }

    [ForeignKey("qismId")]
    [InverseProperty("platform_user_role")]
    public virtual qism_al_tahfeez qism { get; set; }

    [ForeignKey("roleId")]
    [InverseProperty("platform_user_role")]
    public virtual platform_role role { get; set; }

    [ForeignKey("userId")]
    [InverseProperty("platform_user_role")]
    public virtual branch_user user { get; set; }
}
