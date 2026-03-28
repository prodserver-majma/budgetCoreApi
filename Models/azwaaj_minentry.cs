using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace mahadalzahrawebapi.Models;

[Index("itsid", Name = "fk_azwaaj_entry_kg_vf_idx")]
[Index("deptVenueId", Name = "fk_minentry_deptVenue_idx")]
[Index("policyId", Name = "fk_minentry_salaryType_idx")]
public partial class azwaaj_minentry
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsid { get; set; }

    public DateOnly? date { get; set; }

    [Column(TypeName = "int(11)")]
    public int? min { get; set; }

    [Column(TypeName = "int(11)")]
    public int? allotedMin { get; set; }

    public float? rate { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool? isOnLeave { get; set; }

    [Column(TypeName = "int(11)")]
    public int? deptVenueId { get; set; }

    [StringLength(200)]
    public string? createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [StringLength(200)]
    public string? description { get; set; }

    [Column(TypeName = "int(11)")]
    public int? policyId { get; set; }

    [ForeignKey("itsid")]
    [InverseProperty("azwaaj_minentry")]
    public virtual khidmat_guzaar its { get; set; }

    [ForeignKey("deptVenueId")]
    [InverseProperty("azwaaj_minentry")]
    public virtual dept_venue deptVenue { get; set; }

    [ForeignKey("policyId")]
    [InverseProperty("azwaaj_minentry")]
    public virtual salary_type salaryType { get; set; }
}
