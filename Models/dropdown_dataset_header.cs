using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace mahadalzahrawebapi.Models;

[Index("id", Name = "id_UNIQUE", IsUnique = true)]
public partial class dropdown_dataset_header
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    [InverseProperty("header")]
    public virtual ICollection<dropdown_dataset_options> dropdown_dataset_options { get; set; } = new List<dropdown_dataset_options>();
}
