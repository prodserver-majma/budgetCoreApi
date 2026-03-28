using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class qism_al_tahfeez_api_logs
{
    [Key]
    [Column(TypeName = "bigint(20)")]
    public long id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? date { get; set; }

    [Column(TypeName = "text")]
    public string apiString { get; set; }

    [Column(TypeName = "bigint(20)")]
    public long? logId { get; set; }

    [StringLength(200)]
    public string loginName { get; set; }

    [StringLength(45)]
    public string ipAddress { get; set; }

    [Column(TypeName = "text")]
    public string deviceDetails { get; set; }

    [Column(TypeName = "text")]
    public string referrer { get; set; }

    [Column(TypeName = "text")]
    public string apiWithParameter { get; set; }

    [StringLength(100)]
    public string httpRequestType { get; set; }
}
