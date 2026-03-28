using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("qismId", Name = "fk_venue_qism_idx")]
public partial class venue
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [StringLength(1000)]
    [MySqlCharSet("cp1256")]
    [MySqlCollation("cp1256_general_ci")]
    public string CampVenue { get; set; }

    [StringLength(100)]
    [MySqlCharSet("cp1256")]
    [MySqlCollation("cp1256_general_ci")]
    public string CampId { get; set; }

    public bool? ActiveStatus { get; set; } = false;

    [Column(TypeName = "int(11)")]
    public int? CashBalance { get; set; } = 0;

    [StringLength(200)]
    [MySqlCharSet("cp1256")]
    [MySqlCollation("cp1256_general_ci")]
    public string currency { get; set; }

    [StringLength(500)]
    [MySqlCharSet("cp1256")]
    [MySqlCollation("cp1256_general_ci")]
    public string qismTahfeez { get; set; }

    [StringLength(500)]
    [MySqlCharSet("cp1256")]
    [MySqlCollation("cp1256_general_ci")]
    public string ho { get; set; }

    [StringLength(500)]
    [MySqlCharSet("cp1256")]
    [MySqlCollation("cp1256_general_ci")]
    public string displayName { get; set; }

    [Column(TypeName = "int(11)")]
    public int? qismId { get; set; }

    [InverseProperty("venue")]
    public virtual ICollection<dept_venue> dept_venue { get; set; } = new List<dept_venue>();

    [InverseProperty("mauzeNavigation")]
    public virtual ICollection<khidmat_guzaar> khidmat_guzaar { get; set; } = new List<khidmat_guzaar>();

    [ForeignKey("qismId")]
    [InverseProperty("venue")]
    public virtual qism_al_tahfeez qism { get; set; }

    [InverseProperty("venue")]
    public virtual ICollection<registrationform_dropdown_set> registrationform_dropdown_set { get; set; } = new List<registrationform_dropdown_set>();

    [InverseProperty("venue")]
    public virtual ICollection<venue_transfer_approval> venue_transfer_approval_current_venue { get; set; } = new List<venue_transfer_approval>();

    [InverseProperty("venueDetails")]
    public virtual ICollection<user_venue> user_venues { get; set; } = new List<user_venue>();

    public virtual ICollection<branch_user> branch_users { get; set; } = new List<branch_user>();
}
