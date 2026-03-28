using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_student_fee_excluding_list
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? studentMzId { get; set; }

    [Column(TypeName = "varchar(11)")]
    public int? monthId { get; set; }

    [Column(TypeName = "varchar(11)")]
    public string? hijriYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? pSetId { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }
}
