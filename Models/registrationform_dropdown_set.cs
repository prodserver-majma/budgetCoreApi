using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("deptVenueId", Name = "fk_pset_dept_venue_idx")]
[Index("programId", Name = "fk_pset_program_idx")]
[Index("qismId", Name = "fk_pset_qism_al_tahfeez_idx")]
[Index("subprogramId", Name = "fk_pset_sub_program_idx")]
[Index("venueId", Name = "fk_pset_venue_idx")]
public partial class registrationform_dropdown_set
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? programId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? venueId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? subprogramId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? deptVenueId { get; set; }

    public bool? activeStatus { get; set; }

    [Column(TypeName = "int(11)")]
    public int? qismId { get; set; }

    [ForeignKey("deptVenueId")]
    [InverseProperty("registrationform_dropdown_set")]
    public virtual dept_venue deptVenue { get; set; }

    [ForeignKey("programId")]
    [InverseProperty("registrationform_dropdown_set")]
    public virtual registrationform_programs program { get; set; }

    [ForeignKey("qismId")]
    [InverseProperty("registrationform_dropdown_set")]
    public virtual qism_al_tahfeez qism { get; set; }

    [ForeignKey("subprogramId")]
    [InverseProperty("registrationform_dropdown_set")]
    public virtual registrationform_subprograms subprogram { get; set; }

    [ForeignKey("venueId")]
    [InverseProperty("registrationform_dropdown_set")]
    public virtual venue venue { get; set; }

    [ForeignKey("psetId")]
    [InverseProperty("pset")]
    public virtual ICollection<branch_user> user { get; set; } = new List<branch_user>();
}
