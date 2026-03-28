using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

[Table("user_dept-venue_baseitem")]
public partial class user_dept_venue_baseitem
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? dept_venueId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? baseItemId { get; set; }
    public int? psetId { get; set; }

}
