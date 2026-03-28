using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class mz_student_fee_allotment
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? studentId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? pSetId { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "int(11)")]
    public int? feeAlloted { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fcId { get; set; }

    [Column(TypeName = "text")]
    public string reason { get; set; }

    [Column(TypeName = "text")]
    public string remarks { get; set; }

    [Column(TypeName = "varchar(10)")]
    public int? monthId { get; set; }

    [Column(TypeName = "varchar(11)")]
    public string? hijriYear { get; set; }

    [Column(TypeName = "text")]
    public string currency { get; set; }

    [Column(TypeName = "int(11)")]
    public int? txn_Id { get; set; }

    public bool? waiveStatus { get; set; }
}
