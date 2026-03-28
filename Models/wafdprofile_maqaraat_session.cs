using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("teacherItsId", Name = "fk_maqaraat_session_kg_idx")]
[Index("teacherItsId", "sessionDate", Name = "secondary", IsUnique = true)]
public partial class wafdprofile_maqaraat_session
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int teacherItsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? dayId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    public bool? isEvaluated { get; set; }

    [Column(TypeName = "text")]
    public string reason { get; set; }

    [Column(TypeName = "int(11)")]
    public int? juz { get; set; }

    [Column(TypeName = "int(11)")]
    public int? acedemicYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? pages { get; set; }

    public DateOnly sessionDate { get; set; }

    [ForeignKey("teacherItsId")]
    [InverseProperty("wafdprofile_maqaraat_session")]
    public virtual khidmat_guzaar teacherIts { get; set; }

    [InverseProperty("session")]
    public virtual ICollection<wafdprofile_maqaraat_data> wafdprofile_maqaraat_data { get; set; } = new List<wafdprofile_maqaraat_data>();
}
