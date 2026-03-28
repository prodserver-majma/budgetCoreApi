using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_on_off_modules
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    public bool? status { get; set; }

    [InverseProperty("module")]
    public virtual ICollection<mz_off_module_exception> mz_off_module_exception { get; set; } = new List<mz_off_module_exception>();
}
