using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace mahadalzahrawebapi.Models;

[Index("deptVenueId", Name = "fk_dept_venue_baseitem_dept_venue_idx")]
[Index("baseItemId", Name = "fk_deptvenue_procurement_baseitem_idx")]
public partial class dept_venue_baseitem
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? deptVenueId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? psetId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? baseItemId { get; set; }

    [StringLength(45)]
    public string tag { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool hasItemBlock { get; set; }

    [ForeignKey("baseItemId")]
    [InverseProperty("dept_venue_baseitem")]
    public virtual mz_expense_procurement_baseitem baseItem { get; set; }

    [ForeignKey("deptVenueId")]
    [InverseProperty("dept_venue_baseitem")]
    public virtual dept_venue deptVenue { get; set; }
}
