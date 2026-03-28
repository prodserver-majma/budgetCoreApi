using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_student_feecategory_pset_monthly
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int student_feecategory_pset_id { get; set; }

    [Column(TypeName = "int(11)")]
    public int psetId { get; set; }

    [Column(TypeName = "varchar(16)")]
    public string month { get; set; }

    [Column(TypeName = "int(11)")]
    public int student_count { get; set; }

    [Column(TypeName = "int(11)")]
    public int fees_per_student { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime create_on { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime modified_on { get; set; }
}