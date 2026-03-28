using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("itsId", Name = "citizenIts_idx")]
[Index("id", Name = "id_UNIQUE", IsUnique = true)]
public partial class employee_passport_details
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [StringLength(100)]
    public string passportName { get; set; }

    [StringLength(100)]
    public string passportNo { get; set; }

    [StringLength(100)]
    public string dateOfIssue { get; set; }

    [StringLength(100)]
    public string dateOfExpiry { get; set; }

    [StringLength(100)]
    public string placeOfIssue { get; set; }

    [StringLength(100)]
    public string passportPlaceOfBirth { get; set; }

    [StringLength(100)]
    public string dobPassport { get; set; }

    [StringLength(100)]
    public string passportCopy { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("employee_passport_details")]
    public virtual khidmat_guzaar its { get; set; }
}
