using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_student_feecategory_pset
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fcId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? psetId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? amount { get; set; }

    [Column(TypeName = "text")]
    public string currency { get; set; }
    [Column(TypeName = "text")]
    public string frequency { get; set; }
}
