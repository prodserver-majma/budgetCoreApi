using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("ikhtibaarId", "itsId", Name = "Secondary", IsUnique = true)]
public partial class ikhtibaar_marksheet
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int ikhtibaarId { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    public string marks { get; set; }

    public float? totalMarks { get; set; }

    public string remarks { get; set; }

    [StringLength(45)]
    public string type { get; set; }

    [Column(TypeName = "int(11)")]
    public int attempts { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool hasAttempted { get; set; }

    [StringLength(100)]
    public string mukhtabir { get; set; }

    [StringLength(45)]
    public string category { get; set; }

    [ForeignKey("ikhtibaarId")]
    [InverseProperty("ikhtibaar_marksheet")]
    public virtual ikhtibaar ikhtibaar { get; set; }
}
