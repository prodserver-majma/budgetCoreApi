using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class kg_venue_transfer_history
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "text")]
    public string mauze { get; set; }

    [Column(TypeName = "text")]
    public string idara { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime dojDate { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("kg_venue_transfer_history")]
    public virtual khidmat_guzaar its { get; set; }
}
