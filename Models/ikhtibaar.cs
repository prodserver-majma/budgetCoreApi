using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class ikhtibaar
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [StringLength(45)]
    public string name { get; set; }

    [InverseProperty("ikhtibaar")]
    public virtual ICollection<ikhtibaar_marksheet> ikhtibaar_marksheet { get; set; } = new List<ikhtibaar_marksheet>();

    [InverseProperty("ikhtibaar")]
    public virtual ICollection<ikhtibaar_questionnaire> ikhtibaar_questionnaire { get; set; } = new List<ikhtibaar_questionnaire>();
}
