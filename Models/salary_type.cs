using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class salary_type
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [StringLength(45)]
    public string? Name { get; set; }

    [InverseProperty("salaryType")]
    public virtual ICollection<employee_dept_salary> employee_dept_salary { get; set; } = new List<employee_dept_salary>();

    [InverseProperty("salaryType")]
    public virtual ICollection<azwaaj_minentry> azwaaj_minentry { get; set; } = new List<azwaaj_minentry>();

    [InverseProperty("salaryTypeNavigation")]
    public virtual ICollection<salary_generation_gegorgian> salary_generation_gegorgian { get; set; } = new List<salary_generation_gegorgian>();

    [InverseProperty("salaryTypeNavigation")]
    public virtual ICollection<salary_generation_hijri> salary_generation_hijri { get; set; } = new List<salary_generation_hijri>();
}
