using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class wafdprofile_workshop_data
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [Column(TypeName = "text")]
    public string subCategory { get; set; }

    [Column(TypeName = "text")]
    public string cetificateCredentials { get; set; }

    [Column(TypeName = "text")]
    public string keypoints { get; set; }

    [Column(TypeName = "text")]
    public string courseName { get; set; }

    [Column(TypeName = "text")]
    public string category { get; set; }

    [Column(TypeName = "text")]
    public string mode { get; set; }

    [Column(TypeName = "text")]
    public string course { get; set; }

    [Column(TypeName = "text")]
    public string type { get; set; }

    [Column(TypeName = "text")]
    public string year { get; set; }

    [Column(TypeName = "text")]
    public string attachment { get; set; }

    [Column(TypeName = "int(11)")]
    public int? totalDays { get; set; }

    [Column(TypeName = "int(11)")]
    public int? hoursPerDay { get; set; }

    [Column(TypeName = "int(11)")]
    public int? totalHours { get; set; }

    public DateOnly? completionDate { get; set; }

    [Column(TypeName = "int(11)")]
    public int? academicYear { get; set; }
}
