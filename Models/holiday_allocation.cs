using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("holidayId", "deptVenueId", "employeeType", Name = "Seondary", IsUnique = true)]
[Index("holidayId", Name = "fk_holiday_allocation_calender_idx")]
[Index("deptVenueId", Name = "fk_holiday_allocation_deptvenue_idx")]
public partial class holiday_allocation
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int holidayId { get; set; }

    [Column(TypeName = "int(11)")]
    public int deptVenueId { get; set; }

    [Required]
    [StringLength(45)]
    public string employeeType { get; set; }

    [ForeignKey("deptVenueId")]
    [InverseProperty("holiday_allocation")]
    public virtual dept_venue deptVenue { get; set; }

    [ForeignKey("holidayId")]
    [InverseProperty("holiday_allocation")]
    public virtual hijri_calender holiday { get; set; }
}
