using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class kg_faimalydetails
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? hofItsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    [StringLength(45)]
    public string age { get; set; }

    [Column(TypeName = "text")]
    public string relation { get; set; }

    [Column(TypeName = "text")]
    public string jamaat { get; set; }

    [Column(TypeName = "text")]
    public string idara { get; set; }

    [Column(TypeName = "text")]
    public string occupation { get; set; }

    [Column(TypeName = "text")]
    public string hifzStatus { get; set; }

    [Column(TypeName = "text")]
    public string nationality { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? dob { get; set; }

    [StringLength(45)]
    public string bloodGroup { get; set; }
}
