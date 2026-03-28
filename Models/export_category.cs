using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mahadalzahrawebapi.Models;

public partial class export_category
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? categoryId { get; set; }

    [StringLength(45)]
    public string categoryName { get; set; }

    [StringLength(45)]
    public string fieldActualName { get; set; }

    [StringLength(45)]
    public string fieldDisplayName { get; set; }
}
