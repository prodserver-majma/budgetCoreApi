using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("id", Name = "id_UNIQUE", IsUnique = true)]
[Index("itsId", Name = "targetId_idx")]
public partial class employee_logs
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int updatedby { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime updatedon { get; set; }

    [Required]
    [StringLength(100)]
    public string status { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("employee_logs")]
    public virtual khidmat_guzaar its { get; set; }
}
