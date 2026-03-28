using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("masoolIts", Name = "fk_class_masool_kg_idx")]
public partial class training_class
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [StringLength(45)]
    public string className { get; set; }

    [Column(TypeName = "int(11)")]
    public int? academicYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? masoolIts { get; set; }

    [ForeignKey("masoolIts")]
    [InverseProperty("training_class")]
    public virtual khidmat_guzaar masoolItsNavigation { get; set; }

    [InverseProperty("_class")]
    public virtual ICollection<training_class_student> training_class_student { get; set; } = new List<training_class_student>();

    [InverseProperty("_class")]
    public virtual ICollection<training_class_subject_teacher> training_class_subject_teacher { get; set; } = new List<training_class_subject_teacher>();
}
