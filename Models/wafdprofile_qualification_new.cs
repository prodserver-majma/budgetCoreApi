using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class wafdprofile_qualification_new
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsid { get; set; }

    [Column(TypeName = "text")]
    public string country { get; set; }

    [Column(TypeName = "text")]
    public string mediumOfEducation { get; set; }

    [Column(TypeName = "text")]
    public string stage { get; set; }

    [Column(TypeName = "text")]
    public string degree { get; set; }

    [Column(TypeName = "text")]
    public string institutionName { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string status { get; set; }

    [Column(TypeName = "text")]
    public string pursuingYear { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string year { get; set; }

    [Column(TypeName = "text")]
    public string attachment { get; set; }

    [InverseProperty("qualification")]
    public virtual khidmat_guzaar? its { get; set; }
}
