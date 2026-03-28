using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("leaveId", Name = "fk_mzlm_leave_application_log_idx")]
[Index("createdBy", Name = "fk_mzlm_leave_updateby_idx")]
[Index("stageId", Name = "fk_mzlm_stage_id_idx")]
public partial class mzlm_leave_logs
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int leaveId { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string remark { get; set; }

    [Column(TypeName = "int(11)")]
    public int stageId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime createdOn { get; set; }

    [Column(TypeName = "int(11)")]
    public int createdBy { get; set; }

    [ForeignKey("createdBy")]
    [InverseProperty("mzlm_leave_logs")]
    public virtual khidmat_guzaar createdByNavigation { get; set; }

    [ForeignKey("leaveId")]
    [InverseProperty("mzlm_leave_logs")]
    public virtual mzlm_leave_application leave { get; set; }

    [ForeignKey("stageId")]
    [InverseProperty("mzlm_leave_logs")]
    public virtual mzlm_leave_stage stage { get; set; }
}
