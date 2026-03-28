using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

[PrimaryKey("itsId", "date")]
public partial class employee_e_attendence
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Key]
    public DateOnly date { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? entryMorning { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? exitMorning { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? entryEvening { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? exitEvening { get; set; }

    [Column(TypeName = "int(11)")]
    public int? extraHour { get; set; }

    public string logJson { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("employee_e_attendence")]
    public virtual khidmat_guzaar its { get; set; }
}
