using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class wafdprofile_english_assessment
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [StringLength(45)]
    public string verificationCode { get; set; }

    [Column(TypeName = "text")]
    public string examLink { get; set; }

    [Column(TypeName = "text")]
    public string userName { get; set; }

    [Column(TypeName = "text")]
    public string password { get; set; }
}
