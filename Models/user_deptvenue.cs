using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class user_deptvenue
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int deptVenueId { get; set; }

    [Column(TypeName ="int(11)")]
    public int psetId { get; set; }

    [ForeignKey(nameof(deptVenueId))]
    [InverseProperty(nameof(dept_venue.user_deptvenues))]
    virtual public dept_venue deptVenue { get; set; }

    [ForeignKey(nameof(itsId))]
    [InverseProperty(nameof(khidmat_guzaar.user_deptvenues))]
    virtual public khidmat_guzaar employee { get; set; }
}
