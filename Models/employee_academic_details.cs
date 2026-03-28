using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace mahadalzahrawebapi.Models;

[Index("itsId", Name = "itsId_idx")]
public partial class employee_academic_details
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? trNo { get; set; }

    [Column(TypeName = "int(11)")]
    public int? farigDarajah { get; set; }

    [Column(TypeName = "int(11)")]
    public int? farigYear { get; set; }

    [StringLength(50)]
    public string aljameaDegree { get; set; }

    [Column(TypeName = "int(11)")]
    public int? hifzSanadYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? wafdTrainingMasoolIts { get; set; }

    [Column(TypeName = "int(11)")]
    public int? wafdTrainingMushrifIts { get; set; }

    [Column(TypeName = "int(11)")]
    public int? maqaraatTeacherIts { get; set; }

    [Column(TypeName = "int(11)")]
    public int? wafdClassId { get; set; }

    [StringLength(45)]
    public string category { get; set; }

    [Column(TypeName = "int(11)")]
    public int? batchId { get; set; }

    [StringLength(45)]
    public string hifzStatus { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("employee_academic_details")]
    public virtual khidmat_guzaar its { get; set; }
}
