using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mzlm_leave_type
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [StringLength(45)]
    public string name { get; set; }

    [Required]
    [StringLength(45)]
    public string accessTo { get; set; }

    [Column(TypeName = "int(11)")]
    public int daysAllotted { get; set; }

    [Required]
    [StringLength(75)]
    public string approvalFlow { get; set; }

    [Required]
    [Column(TypeName = "json")]
    public string applicableTo { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool? active { get; set; }

    [InverseProperty("type")]
    public virtual ICollection<mzlm_leave_application> mzlm_leave_application { get; set; } = new List<mzlm_leave_application>();

    [InverseProperty("leaveType")]
    public virtual ICollection<mzlm_leave_category> mzlm_leave_category { get; set; } = new List<mzlm_leave_category>();
}
