using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("itsId", Name = "khidmatguzarIts_idx")]
public partial class employee_khidmat_details
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? mahad_khidmatYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? khidmatYear { get; set; }

    [Column(TypeName = "text")]
    public string khidmatMauzeHouseStatus { get; set; }

    [Column(TypeName = "text")]
    public string khdimatMauzeHouseType { get; set; }

    [Column(TypeName = "int(11)")]
    public int? tayeenYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? tayeenMonth { get; set; }

    [Column(TypeName = "int(11)")]
    public int? khidmatMonth { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("employee_khidmat_details")]
    public virtual khidmat_guzaar its { get; set; }
}
