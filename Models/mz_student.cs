using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("itsID", Name = "itsID_UNIQUE", IsUnique = true)]
public partial class mz_student
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int mz_id { get; set; }

    [Required]
    [Column(TypeName = "int(11)")]
    public int? itsID { get; set; }

    [Column(TypeName = "text")]
    public string nameEng { get; set; }

    [Column(TypeName = "text")]
    public string nameArabic { get; set; }

    [Column(TypeName = "text")]
    public string gender { get; set; }

    [Column(TypeName = "text")]
    public string bloodGroup { get; set; }

    [Column(TypeName = "text")]
    public string studentEmail { get; set; }

    [Column(TypeName = "text")]
    public string studentMobile { get; set; }

    [Column(TypeName = "text")]
    public string studentWhatsapp { get; set; }

    [Column(TypeName = "text")]
    public string dobGregorian { get; set; }

    [Column(TypeName = "text")]
    public string dobHijri { get; set; }

    [Column(TypeName = "int(11)")]
    public int? age { get; set; }

    [Column(TypeName = "text")]
    public string fatherEmail { get; set; }

    [Column(TypeName = "text")]
    public string fatherMobile { get; set; }

    [Column(TypeName = "text")]
    public string fatherWhatsapp { get; set; }

    [Column(TypeName = "text")]
    public string motherEmail { get; set; }

    [Column(TypeName = "text")]
    public string motherMobile { get; set; }

    [Column(TypeName = "text")]
    public string motherWhatsapp { get; set; }

    [Column(TypeName = "text")]
    public string address { get; set; }

    [Column(TypeName = "text")]
    public string jamaat { get; set; }

    [Column(TypeName = "text")]
    public string jamiat { get; set; }

    [Column(TypeName = "text")]
    public string vatan { get; set; }

    [Column(TypeName = "text")]
    public string maqaam { get; set; }

    [Column(TypeName = "text")]
    public string nationality { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    public bool? activeStatus { get; set; }

    [Column(TypeName = "int(11)")]
    public int? psetId { get; set; }

    [Column(TypeName = "text")]
    public string photoPath { get; set; }

    [Column(TypeName = "text")]
    public string photoBase64 { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fcId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? elq_GroupId { get; set; }

    [Column(TypeName = "text")]
    public string elq_BranchName { get; set; }

    [Column(TypeName = "int(11)")]
    public int? trNo { get; set; }

    [Column(TypeName = "int(11)")]
    public int? classId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? hifzSanadYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int? dq_fasal { get; set; }

    [StringLength(50)]
    public string hifzStatus { get; set; }

    [StringLength(100)]
    public string idara { get; set; }

    public virtual nisaab_alumni nisaab_alumni { get; set; }
}
