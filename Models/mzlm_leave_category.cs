using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("leaveTypeId", Name = "fk_leave_type_category_idx")]
public partial class mzlm_leave_category
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [StringLength(45)]
    public string name { get; set; }

    [Column(TypeName = "int(11)")]
    public int leaveTypeId { get; set; }

    [Column(TypeName = "int(11)")]
    public int maxAllowed { get; set; }

    [Column(TypeName = "int(11)")]
    public int consicutiveLimit { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool isHijri { get; set; }

    [Column(TypeName = "int(11)")]
    public int minApplicationDate { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool isDeductable { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool isRepeated { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool isCarryForward { get; set; }

    [Required]
    [Column(TypeName = "json")]
    public string notifyTo { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool? active { get; set; }

    [ForeignKey("leaveTypeId")]
    [InverseProperty("mzlm_leave_category")]
    public virtual mzlm_leave_type leaveType { get; set; }

    [InverseProperty("category")]
    public virtual ICollection<mzlm_leave_application> mzlm_leave_application { get; set; } = new List<mzlm_leave_application>();
}
