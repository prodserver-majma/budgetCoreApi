using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace mahadalzahrawebapi.Models;

[Index("categoryId", Name = "fk_leave_apply_category_idx")]
[Index("itsId", Name = "fk_leave_apply_khidmatguzaar_idx")]
[Index("packageId", Name = "fk_leave_apply_package_idx")]
[Index("stageId", Name = "fk_leave_apply_stage_idx")]
[Index("typeId", Name = "fk_leave_apply_type_idx")]
public partial class mzlm_leave_application
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int typeId { get; set; }

    [Column(TypeName = "int(11)")]
    public int categoryId { get; set; }

    [Column(TypeName = "int(11)")]
    public int fromDayId { get; set; }

    [Column(TypeName = "int(11)")]
    public int fromMonthId { get; set; }

    [Column(TypeName = "int(11)")]
    public int toDayId { get; set; }

    [Column(TypeName = "int(11)")]
    public int toMonthId { get; set; }

    public bool morningShift { get; set; }

    public bool eveningShift { get; set; }

    [Column(TypeName = "int(11)")]
    public int shiftCount { get; set; }

    [Column(TypeName = "int(11)")]
    public int hijrAcademicYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int stageId { get; set; }

    [Column(TypeName = "int(11)")]
    public int venueId { get; set; }

    [Required]
    [StringLength(15)]
    public string appliedBy { get; set; }

    [Column(TypeName = "int(11)")]
    public int createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime fromEngDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime toEngDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime updatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime createdOn { get; set; }

    [Column(TypeName = "text")]
    public string purpose { get; set; }

    [AllowNull]
    [Column(TypeName = "int(11)")]
    public int? packageId { get; set; }

    [Column(TypeName = "int(11)")]
    public int toYear { get; set; }

    [Column(TypeName = "int(11)")]
    public int fromYear { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string UploadedDocumentUrl { get; set; }

    [ForeignKey("categoryId")]
    [InverseProperty("mzlm_leave_application")]
    public virtual mzlm_leave_category category { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("mzlm_leave_application")]
    public virtual khidmat_guzaar its { get; set; }

    [InverseProperty("leave")]
    public virtual ICollection<mzlm_leave_logs> mzlm_leave_logs { get; set; } = new List<mzlm_leave_logs>();

    [ForeignKey("packageId")]
    [InverseProperty("mzlm_leave_application")]
    public virtual mzlm_leave_package? package { get; set; }

    [ForeignKey("stageId")]
    [InverseProperty("mzlm_leave_application")]
    public virtual mzlm_leave_stage stage { get; set; }

    [ForeignKey("typeId")]
    [InverseProperty("mzlm_leave_application")]
    public virtual mzlm_leave_type type { get; set; }
}
