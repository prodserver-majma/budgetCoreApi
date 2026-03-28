using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace mahadalzahrawebapi.Models;

[Index("emailId", Name = "emailId_UNIQUE", IsUnique = true)]
public partial class branch_user
{

    [Key]
    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "text")]
    public string password { get; set; }

    [StringLength(200)]
    public string emailId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? lastLoggedIn { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("branch_user")]
    public virtual khidmat_guzaar its { get; set; }

    [InverseProperty("branch_user")]
    public virtual ICollection<qism_al_tahfeez_user_deptvenue> branchuser_deptvenue { get; set; } = new List<qism_al_tahfeez_user_deptvenue>();

    [InverseProperty("user")]
    public virtual ICollection<platform_user_module> platform_user_module { get; set; } = new List<platform_user_module>();

    [InverseProperty("user")]
    public virtual ICollection<platform_user_role> platform_user_role { get; set; } = new List<platform_user_role>();

    [InverseProperty("its")]
    public virtual qism_al_tahfeez qism_al_tahfeez { get; set; }

    [ForeignKey("userId")]
    [InverseProperty("user")]
    public virtual ICollection<dept_venue> deptVenue { get; set; } = new List<dept_venue>();

    public virtual ICollection<venue> venues { get; set; } = new List<venue>();

    [ForeignKey("userId")]
    [InverseProperty("user")]
    public virtual ICollection<registrationform_dropdown_set> pset { get; set; } = new List<registrationform_dropdown_set>();
}
