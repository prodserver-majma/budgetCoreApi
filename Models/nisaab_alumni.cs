using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("kgIts", Name = "fk_alumni_mz_kg_idx")]
public partial class nisaab_alumni
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    public bool? jamea { get; set; }

    [StringLength(75)]
    public string degree { get; set; }

    [Column(TypeName = "int(11)")]
    public int? farigYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? farigDarajah { get; set; }

    [Column(TypeName = "int(11)")]
    public int? batchId { get; set; }

    public bool hafizAtFarig { get; set; }

    [Column(TypeName = "int(11)")]
    public int? kgIts { get; set; }

    [StringLength(10)]
    public string degreeNum { get; set; }

    public virtual mz_student its { get; set; }

    [ForeignKey("kgIts")]
    [InverseProperty("nisaab_alumni")]
    public virtual khidmat_guzaar? kgItsNavigation { get; set; }
}
