using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class kg_identitycards
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string cardType { get; set; }

    [Column(TypeName = "text")]
    public string country { get; set; }

    [Column(TypeName = "text")]
    public string nameOnCard { get; set; }

    [Column(TypeName = "text")]
    public string cardNumber { get; set; }

    [Column(TypeName = "int(11)")]
    public int? itsId { get; set; }

    [Column(TypeName = "text")]
    public string attachment { get; set; }

    [Column(TypeName = "text")]
    public string back_attachment { get; set; }
}
