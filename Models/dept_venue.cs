using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace mahadalzahrawebapi.Models;

[Index("deptId", Name = "fk_dept_venue_deptid_idx")]
[Index("masterDeptId", Name = "fk_dept_venue_masterdid_idx")]
[Index("qismId", Name = "fk_dept_venue_qism_al_tahfeez_idx")]
[Index("venueId", Name = "fk_dept_venue_venue_idx")]
public partial class dept_venue
{

    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? masterDeptId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? deptId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? venueId { get; set; }

    [StringLength(100)]
    public string? masterDeptName { get; set; }

    [StringLength(100)]
    public string? deptName { get; set; }

    [StringLength(100)]
    public string? venueName { get; set; }

    [StringLength(45)]
    public string? status { get; set; }

    [StringLength(45)]
    public string? tag { get; set; }

    [Column(TypeName = "int(11)")]
    public int? qismId { get; set; }

    [ForeignKey("deptId")]
    [InverseProperty("dept_venue")]
    public virtual department dept { get; set; }

    [InverseProperty("deptVenue")]
    public virtual ICollection<dept_venue_baseitem> dept_venue_baseitem { get; set; } = new List<dept_venue_baseitem>();

    [InverseProperty("deptVenue")]
    public virtual ICollection<employee_dept_salary> employee_dept_salary { get; set; } = new List<employee_dept_salary>();

    [InverseProperty("deptVenue")]
    public virtual ICollection<azwaaj_minentry> azwaaj_minentry { get; set; } = new List<azwaaj_minentry>();

    [InverseProperty("deptVenue")]
    public virtual ICollection<holiday_allocation> holiday_allocation { get; set; } = new List<holiday_allocation>();

    [ForeignKey("masterDeptId")]
    [InverseProperty("dept_venue")]
    public virtual masterdepartment masterDept { get; set; }

    [InverseProperty("deptVenue")]
    public virtual ICollection<qism_al_tahfeez_user_deptvenue> branchuser_deptvenue { get; set; } = new List<qism_al_tahfeez_user_deptvenue>();

    [InverseProperty("deptVenue")]
    public virtual ICollection<mz_expense_budget_araz> mz_expense_budget_araz { get; set; } = new List<mz_expense_budget_araz>();

    // [InverseProperty("deptVenue")]
    // public virtual ICollection<mz_expense_budget_araz_monthly> mz_expense_budget_araz_monthly { get; set; } = new List<mz_expense_budget_araz_monthly>();

    [InverseProperty("deptVenue")]
    public virtual ICollection<mz_expense_budget_smart_goals> mz_expense_budget_smart_goals { get; set; } = new List<mz_expense_budget_smart_goals>();

    [ForeignKey("qismId")]
    [InverseProperty("dept_venue")]
    public virtual qism_al_tahfeez qism { get; set; }

    [InverseProperty("deptVenue")]
    public virtual ICollection<registrationform_dropdown_set> registrationform_dropdown_set { get; set; } = new List<registrationform_dropdown_set>();

    [InverseProperty("deptVenue")]
    public virtual ICollection<salary_generation_gegorgian> salary_generation_gegorgian { get; set; } = new List<salary_generation_gegorgian>();

    [InverseProperty("deptVenue")]
    public virtual ICollection<salary_generation_hijri> salary_generation_hijri { get; set; } = new List<salary_generation_hijri>();

    [ForeignKey("venueId")]
    [InverseProperty("dept_venue")]
    public virtual venue venue { get; set; }

    [ForeignKey("deptVenueId")]
    [InverseProperty("deptVenue")]
    public virtual ICollection<branch_user> user { get; set; } = new List<branch_user>();

    [InverseProperty("deptVenue")]
    public virtual ICollection<user_deptvenue> user_deptvenues { get; set; } = new List<user_deptvenue>();
}
