using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_expense_estimate_student_monthly
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int estimate_student_id { get; set; }

    [Column(TypeName = "int(11)")]
    public int psetId { get; set; }

    [Column(TypeName = "varchar(16)")]
    public string month { get; set; }

    [Column(TypeName = "int(11)")]
    public int student_count { get; set; }

    [Column(TypeName ="int(11)")]
    public int fees_per_student { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime create_on { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime modified_on { get; set; }
}   