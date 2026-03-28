using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("UniqueKey", Name = "Secondary", IsUnique = true)]
[Index("UserID", Name = "indexed")]
public partial class password_reset_requests
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int ID { get; set; }

    [Column(TypeName = "int(11)")]
    public int? UserID { get; set; }

    [StringLength(128)]
    public string UniqueKey { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryTime { get; set; }
}
