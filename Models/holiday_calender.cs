using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class holiday_calender
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    public DateOnly? holidayDate { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [StringLength(45)]
    public string duration { get; set; }
}
