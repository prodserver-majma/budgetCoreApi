using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("cstId", "studentITS", "acedemicYear", Name = "Secondary", IsUnique = true)]
[Index("gradedBy", Name = "fk_student_marksheet_kg_idx")]
[Index("studentITS", Name = "fk_student_marksheet_student_idx")]
[Index("cstId", Name = "fk_student_marksheet_subject_cst_idx")]
public partial class training_student_subject_marksheet
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int cstId { get; set; }

    [Column(TypeName = "int(11)")]
    public int studentITS { get; set; }

    [Column(TypeName = "text")]
    public string answers { get; set; }

    [Required]
    [StringLength(45)]
    public string status { get; set; }

    [Column(TypeName = "int(11)")]
    public int? marks { get; set; }

    [Column(TypeName = "int(11)")]
    public int acedemicYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? gradedBy { get; set; }

    public DateOnly startDate { get; set; }

    public DateOnly endDate { get; set; }

    [Column(TypeName = "text")]
    public string remarks { get; set; }

    [ForeignKey("cstId")]
    [InverseProperty("training_student_subject_marksheet")]
    public virtual training_class_subject_teacher cst { get; set; }

    [ForeignKey("gradedBy")]
    [InverseProperty("training_student_subject_marksheetgradedByNavigation")]
    public virtual khidmat_guzaar gradedByNavigation { get; set; }

    [ForeignKey("studentITS")]
    [InverseProperty("training_student_subject_marksheetstudentITSNavigation")]
    public virtual khidmat_guzaar studentITSNavigation { get; set; }
}
