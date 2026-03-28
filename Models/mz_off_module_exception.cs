using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

[PrimaryKey("moduleId", "itsId")]
public partial class mz_off_module_exception
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int moduleId { get; set; }

    [Key]
    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [ForeignKey("moduleId")]
    [InverseProperty("mz_off_module_exception")]
    public virtual mz_on_off_modules module { get; set; }
}
