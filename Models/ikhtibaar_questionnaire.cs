using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("ikhtibaarId", Name = "fk_questionnaire_ikhtebaar_idx")]
public partial class ikhtibaar_questionnaire
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int ikhtibaarId { get; set; }

    [Required]
    [StringLength(150)]
    public string question { get; set; }

    [Column(TypeName = "int(11)")]
    public int weightage { get; set; }

    [ForeignKey("ikhtibaarId")]
    [InverseProperty("ikhtibaar_questionnaire")]
    public virtual ikhtibaar ikhtibaar { get; set; }
}
