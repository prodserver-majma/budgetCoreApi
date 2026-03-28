using mahadalzahrawebapi.Mappings;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace mahadalzahrawebapi.Models;

public partial class mzlm_leave_package
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string name { get; set; }

    [Column(TypeName = "int(11)")]
    public int stageId { get; set; }

    [Column(TypeName = "text")]
    public string purpose { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? UploadedDocumentUrl { get; set; }

    [Column(TypeName = "json")]
    public string leaveBulkAssignationJson { get; set; }

    [NotMapped]
    public leaveBulkAssignation LeaveBulkAssignation
    {
        get => JsonSerializer.Deserialize<leaveBulkAssignation>(leaveBulkAssignationJson);
        set => leaveBulkAssignationJson = JsonSerializer.Serialize(value);
    }

    [InverseProperty("package")]
    public virtual ICollection<mzlm_leave_application> mzlm_leave_application { get; set; } = new List<mzlm_leave_application>();

    [InverseProperty("package")]
    public virtual ICollection<mzlm_leave_package_logs> mzlm_leave_package_logs { get; set; } = new List<mzlm_leave_package_logs>();
}
