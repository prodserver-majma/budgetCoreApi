using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class qism_al_tahfeez_login_logs
{
    [Key]
    [Column(TypeName = "bigint(20)")]
    public long id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? date { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [Column(TypeName = "text")]
    public string ipAddress { get; set; }

    [Column(TypeName = "text")]
    public string email { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? logoutTime { get; set; }

    [Column(TypeName = "text")]
    public string deviceDetails { get; set; }
}
