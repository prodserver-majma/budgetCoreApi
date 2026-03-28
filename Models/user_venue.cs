using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class user_venue
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int venueId { get; set; }

    [ForeignKey(nameof(itsId))]
    [InverseProperty(nameof(khidmat_guzaar.user_venues))]
    virtual public khidmat_guzaar employee { get; set; }

    [ForeignKey(nameof(venueId))]
    [InverseProperty(nameof(venue.user_venues))]
    virtual public venue venueDetails { get; set; }
}
