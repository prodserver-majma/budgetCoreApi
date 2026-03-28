using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class department
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int deptId { get; set; }

    [StringLength(100)]
    public string deptName { get; set; }

    [StringLength(45)]
    public string tag { get; set; }

    [InverseProperty("dept")]
    public virtual ICollection<dept_venue> dept_venue { get; set; } = new List<dept_venue>();
}
