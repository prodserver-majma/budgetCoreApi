using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class userdeptassociation
{
    [Key]
    [Column(TypeName = "bigint(20)")]
    public long Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int DID { get; set; }

    [Column(TypeName = "text")]
    public string Idara { get; set; }

    [Column(TypeName = "text")]
    public string Department { get; set; }

    [Column(TypeName = "text")]
    public string DisplayName { get; set; }

    [StringLength(50)]
    public string DepartmentCode { get; set; }
}
