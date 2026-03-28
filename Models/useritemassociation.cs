using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class useritemassociation
{
    [Key]
    [Column(TypeName = "bigint(20)")]
    public long Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int UserId { get; set; }

    [Column(TypeName = "int(11)")]
    public int DID { get; set; }

    [Column(TypeName = "int(11)")]
    public int BaseItemId { get; set; }
}
