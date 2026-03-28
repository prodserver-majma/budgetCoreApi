using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class user
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "text")]
    public string Username { get; set; }

    [Column(TypeName = "int(11)")]
    public int ItsId { get; set; }

    [StringLength(100)]
    public string Password { get; set; }

    [StringLength(100)]
    public string Accesslevel { get; set; }

    [Column(TypeName ="int(11)")]
    public int? mauze { get; set; }

    [Column(TypeName = "int(11)")]
    public int DID { get; set; }

    [Column(TypeName = "text")]
    public string EmailId { get; set; }

    [StringLength(100)]
    public string Mobile { get; set; }

    [Column(TypeName = "int(11)")]
    public int? roleId { get; set; }

    [Column(TypeName = "text")]
    public string loginStatus { get; set; }
    public role role { get; set; }
    
}
