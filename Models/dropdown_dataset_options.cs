using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace mahadalzahrawebapi.Models;

[Index("headerId", Name = "fk_options_header_id_idx")]
[Index("id", Name = "id_UNIQUE", IsUnique = true)]
public partial class dropdown_dataset_options
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? headerId { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    [ForeignKey("headerId")]
    [InverseProperty("dropdown_dataset_options")]
    public virtual dropdown_dataset_header header { get; set; }
}
