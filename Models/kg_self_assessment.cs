using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class kg_self_assessment
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "text")]
    public string strength { get; set; }

    [Column(TypeName = "text")]
    public string weakness { get; set; }

    [Column(TypeName = "text")]
    public string longTermGoal { get; set; }

    [Column(TypeName = "text")]
    public string roleModel { get; set; }

    [Column(TypeName = "text")]
    public string changeAboutYourself { get; set; }

    [Column(TypeName = "text")]
    public string alternativeCareerPath { get; set; }

    [StringLength(15)]
    public string personalitytype { get; set; }

    [Column(TypeName = "text")]
    public string personalityReport { get; set; }

    [Column(TypeName = "text")]
    public string aboutYourSelf { get; set; }
}
