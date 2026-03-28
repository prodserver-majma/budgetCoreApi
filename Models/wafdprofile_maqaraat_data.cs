using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("studentItsId", Name = "fk_maqaarat_data_kg_idx")]
[Index("sessionId", Name = "fk_maqaraat_data_maqaraat_session_idx")]
public partial class wafdprofile_maqaraat_data
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int sessionId { get; set; }

    [Column(TypeName = "int(11)")]
    public int studentItsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? marks { get; set; }

    public bool? isPresent { get; set; }

    [StringLength(45)]
    public string absentReason { get; set; }

    [ForeignKey("sessionId")]
    [InverseProperty("wafdprofile_maqaraat_data")]
    public virtual wafdprofile_maqaraat_session session { get; set; }

    [ForeignKey("studentItsId")]
    [InverseProperty("wafdprofile_maqaraat_data")]
    public virtual khidmat_guzaar studentIts { get; set; }
}
