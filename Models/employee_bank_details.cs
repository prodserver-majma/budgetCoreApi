using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("itsId", Name = "employeeIts_idx", IsUnique = true)]
public partial class employee_bank_details
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "text")]
    public string bankName { get; set; }

    [Column(TypeName = "text")]
    public string bankAccountNumber { get; set; }

    [Column(TypeName = "text")]
    public string bankAccountName { get; set; }

    [Column(TypeName = "text")]
    public string ifsc { get; set; }

    [Column(TypeName = "text")]
    public string bankBranch { get; set; }

    [Column(TypeName = "text")]
    public string domesticCode { get; set; }

    [Column(TypeName = "text")]
    public string internationalCodeType { get; set; }

    [Column(TypeName = "text")]
    public string internationalCode { get; set; }

    [Column(TypeName = "text")]
    public string country { get; set; }

    [Column(TypeName = "text")]
    public string bankAccountType { get; set; }

    [Column(TypeName = "text")]
    public string panCard { get; set; }

    [Column(TypeName = "text")]
    public string panCardAttachment { get; set; }

    [Column(TypeName = "text")]
    public string chequeAttachment { get; set; }

    public bool isDefault { get; set; } = false;

    [ForeignKey("itsId")]
    [InverseProperty("employee_bank_details")]
    public virtual khidmat_guzaar employee { get; set; }

}
