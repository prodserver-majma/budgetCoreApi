using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("itsId", "deptVenueId", "salaryType", "month", "year", Name = "Secondary")]
[Index("itsId", Name = "empsalarygeneration_idx")]
[Index("salaryType", Name = "fk_salary_gen_h_salary_type")]
[Index("allocationId", Name = "fk_salary_gen_hijri_allocation_idx")]
[Index("deptVenueId", Name = "sdv_idx")]
public partial class salary_generation_hijri
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int quantity { get; set; }

    [Column(TypeName = "int(11)")]
    public int? netSalary { get; set; }

    [Column(TypeName = "int(11)")]
    public int month { get; set; }

    [Column(TypeName = "int(11)")]
    public int year { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "int(11)")]
    public int deptVenueId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? allocationId { get; set; }

    [Column(TypeName = "int(11)")]
    public int salaryType { get; set; }

    [ForeignKey("allocationId")]
    [InverseProperty("salary_generation_hijri")]
    public virtual salary_allocation_hijri allocation { get; set; }

    [ForeignKey("deptVenueId")]
    [InverseProperty("salary_generation_hijri")]
    public virtual dept_venue deptVenue { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("salary_generation_hijri")]
    public virtual khidmat_guzaar its { get; set; }

    [ForeignKey("salaryType")]
    [InverseProperty("salary_generation_hijri")]
    public virtual salary_type salaryTypeNavigation { get; set; }
}
