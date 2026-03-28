using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("itsId", Name = "fk_maqaraat_teacher_kg_idx")]
public partial class wafdprofile_maqaraat_teacher
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "text")]
    public string days { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("wafdprofile_maqaraat_teacher")]
    public virtual khidmat_guzaar its { get; set; }
}
