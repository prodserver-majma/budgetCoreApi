using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[PrimaryKey("itsId", "deptVenueId", "salaryTypeId")]
[Index("deptVenueId", Name = "deptVenueSalaryId_idx")]
[Index("salaryTypeId", Name = "salaryTypeId_idx")]
public partial class employee_dept_salary
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Key]
    [Column(TypeName = "int(11)")]
    public int deptVenueId { get; set; }

    [Key]
    [Column(TypeName = "int(11)")]
    public int salaryTypeId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? workingMin { get; set; }


    [Column(TypeName = "int(11)")]
    public int? workingDays { get; set; }

    public bool? hasSalary { get; set; }

    public float? salaryAmount { get; set; }

    public bool? isHijriSalary { get; set; }

    [ForeignKey("deptVenueId")]
    [InverseProperty("employee_dept_salary")]
    public virtual dept_venue deptVenue { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("employee_dept_salary")]
    public virtual khidmat_guzaar its { get; set; }

    [ForeignKey("salaryTypeId")]
    [InverseProperty("employee_dept_salary")]
    public virtual salary_type salaryType { get; set; }
}
