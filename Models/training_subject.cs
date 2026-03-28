using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class training_subject
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [StringLength(45)]
    public string name { get; set; }

    [Required]
    [StringLength(45)]
    public string status { get; set; }

    [Column(TypeName = "int(11)")]
    public int outOf { get; set; }

    [Column(TypeName = "text")]
    public string qustionare { get; set; }

    [Column(TypeName = "int(11)")]
    public int? accademicYear { get; set; }

    [InverseProperty("subject")]
    public virtual ICollection<training_class_subject_teacher> training_class_subject_teacher { get; set; } = new List<training_class_subject_teacher>();
}
