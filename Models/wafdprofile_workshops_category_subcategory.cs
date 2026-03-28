using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class wafdprofile_workshops_category_subcategory
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string category { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string subCategory { get; set; }
}
