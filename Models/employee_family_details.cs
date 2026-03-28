using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("itsId", Name = "empId_UNIQUE", IsUnique = true)]
[Index("id", Name = "itsId_UNIQUE", IsUnique = true)]
public partial class employee_family_details
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [StringLength(100)]
    public string FatherName { get; set; }

    [StringLength(100)]
    public string FatherIts { get; set; }

    [StringLength(100)]
    public string MotherName { get; set; }

    [StringLength(100)]
    public string MotherIts { get; set; }

    [StringLength(100)]
    public string SpouseName { get; set; }

    [StringLength(100)]
    public string SpouseIts { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("employee_family_details")]
    public virtual khidmat_guzaar its { get; set; }
}
