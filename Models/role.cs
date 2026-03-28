using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace mahadalzahrawebapi.Models;

public partial class role
{
    [Key]
    [Column("roleId", TypeName = "int(11)")]
    public int roleId { get; set; }

    [Column("roleName")]
    [StringLength(100)]
    public string roleName { get; set; }

    [Column("description")]
    [StringLength(100)]
    public string description { get; set; }

    [Column("status", TypeName = "int(11)")] // ✅ THIS FIXES IT
    public int status { get; set; }

    [JsonIgnore]
    public ICollection<user> user { get; set; }
}

