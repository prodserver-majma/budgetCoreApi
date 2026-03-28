using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("studentITS", Name = "fk_class_student_kg_student_idx")]
[Index("classId", Name = "fk_class_student_training_class_idx")]
public partial class training_class_student
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int classId { get; set; }

    [Column(TypeName = "int(11)")]
    public int studentITS { get; set; }

    [Column(TypeName = "int(11)")]
    public int? rank { get; set; }

    [Column(TypeName = "int(11)")]
    public int? prevRank { get; set; }

    [StringLength(45)]
    public string mauze { get; set; }

    [Column(TypeName = "int(11)")]
    public int academicYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? marks { get; set; }

    [Column(TypeName = "int(11)")]
    public int? percentage { get; set; }

    [ForeignKey("classId")]
    [InverseProperty("training_class_student")]
    public virtual training_class _class { get; set; }

    [ForeignKey("studentITS")]
    [InverseProperty("training_class_student")]
    public virtual khidmat_guzaar studentITSNavigation { get; set; }
}
