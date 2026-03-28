using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("emailId", Name = "emailId_UNIQUE", IsUnique = true)]
[Index("itsId", Name = "itsId_UNIQUE", IsUnique = true)]
public partial class qism_al_tahfeez
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [StringLength(200)]
    public string emailId { get; set; }

    [Column(TypeName = "text")]
    public string password { get; set; }

    public bool? isActive { get; set; }

    [InverseProperty("qism")]
    public virtual ICollection<dept_venue> dept_venue { get; set; } = new List<dept_venue>();

    public virtual ICollection<payroll_salary_packages> payroll_salary_packages { get; set; } = new List<payroll_salary_packages>();

    [ForeignKey("itsId")]
    [InverseProperty("qism_al_tahfeez")]
    public virtual branch_user its { get; set; }

    [InverseProperty("qism")]
    public virtual ICollection<platform_user_module> platform_user_module { get; set; } = new List<platform_user_module>();

    [InverseProperty("qism")]
    public virtual ICollection<platform_user_role> platform_user_role { get; set; } = new List<platform_user_role>();

    [InverseProperty("qism")]
    public virtual ICollection<registrationform_dropdown_set> registrationform_dropdown_set { get; set; } = new List<registrationform_dropdown_set>();

    [InverseProperty("qism")]
    public virtual ICollection<venue> venue { get; set; } = new List<venue>();
}
