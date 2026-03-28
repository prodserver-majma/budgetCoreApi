using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class masterdepartment
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int masterDeptId { get; set; }

    [StringLength(100)]
    public string masterDeptName { get; set; }

    [InverseProperty("masterDept")]
    public virtual ICollection<dept_venue> dept_venue { get; set; } = new List<dept_venue>();
}
