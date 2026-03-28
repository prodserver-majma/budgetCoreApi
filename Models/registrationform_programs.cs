using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class registrationform_programs
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    [InverseProperty("program")]
    public virtual ICollection<registrationform_dropdown_set> registrationform_dropdown_set { get; set; } = new List<registrationform_dropdown_set>();
}
