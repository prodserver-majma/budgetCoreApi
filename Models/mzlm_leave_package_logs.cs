using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("packageId", Name = "fk_mzlm_leave_package_log_idx")]
[Index("createdBy", Name = "fk_mzlm_leave_updateby_idx1")]
[Index("stageId", Name = "fk_mzlm_stage_id_idx1")]
public partial class mzlm_leave_package_logs
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int packageId { get; set; }

    [Column(TypeName = "text")]
    public string remark { get; set; }

    [Column(TypeName = "int(11)")]
    public int stageId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime createdOn { get; set; }

    [Column(TypeName = "int(11)")]
    public int createdBy { get; set; }

    [ForeignKey("createdBy")]
    [InverseProperty("mzlm_leave_package_logs")]
    public virtual khidmat_guzaar createdByNavigation { get; set; }

    [ForeignKey("packageId")]
    [InverseProperty("mzlm_leave_package_logs")]
    public virtual mzlm_leave_package package { get; set; }

    [ForeignKey("stageId")]
    [InverseProperty("mzlm_leave_package_logs")]
    public virtual mzlm_leave_stage stage { get; set; }
}
