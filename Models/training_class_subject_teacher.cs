using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("classId", Name = "fk_cst_class_idx")]
[Index("subjectId", Name = "fk_cst_subject_idx")]
[Index("teacherITS", Name = "fk_cst_teacher_idx")]
[Index("classId", "subjectId", Name = "secondary_key", IsUnique = true)]
public partial class training_class_subject_teacher
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int classId { get; set; }

    [Column(TypeName = "int(11)")]
    public int subjectId { get; set; }

    [Column(TypeName = "int(11)")]
    public int teacherITS { get; set; }

    [Required]
    [StringLength(45)]
    public string status { get; set; }

    [Column(TypeName = "int(11)")]
    public int? acedemicYear { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime createdOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? updatedOn { get; set; }

    [Column(TypeName = "int(11)")]
    public int createdBy { get; set; }

    public DateOnly startDate { get; set; }

    public DateOnly endDate { get; set; }

    [ForeignKey("classId")]
    [InverseProperty("training_class_subject_teacher")]
    public virtual training_class _class { get; set; }

    [ForeignKey("subjectId")]
    [InverseProperty("training_class_subject_teacher")]
    public virtual training_subject subject { get; set; }

    [ForeignKey("teacherITS")]
    [InverseProperty("training_class_subject_teacher")]
    public virtual khidmat_guzaar teacherITSNavigation { get; set; }

    [InverseProperty("cst")]
    public virtual ICollection<training_student_subject_marksheet> training_student_subject_marksheet { get; set; } = new List<training_student_subject_marksheet>();
}
