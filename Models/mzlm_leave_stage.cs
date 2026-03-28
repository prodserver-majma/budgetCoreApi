using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mzlm_leave_stage
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string name { get; set; }

    [Column(TypeName = "tinyint(1)")]
    public bool? isDeductable { get; set; }

    [InverseProperty("stage")]
    public virtual ICollection<mzlm_leave_application> mzlm_leave_application { get; set; } = new List<mzlm_leave_application>();

    [InverseProperty("stage")]
    public virtual ICollection<mzlm_leave_logs> mzlm_leave_logs { get; set; } = new List<mzlm_leave_logs>();

    [InverseProperty("stage")]
    public virtual ICollection<mzlm_leave_package_logs> mzlm_leave_package_logs { get; set; } = new List<mzlm_leave_package_logs>();
}
