using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class salary_querylogs
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? hijriMonth { get; set; }

    [Column(TypeName = "int(11)")]
    public int? hijriYear { get; set; }

    [StringLength(200)]
    public string type { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "text")]
    public string remarks { get; set; }

    public DateOnly? fromDate { get; set; }

    public DateOnly? toDate { get; set; }
}
