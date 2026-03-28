using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models
{
    //create index for all columns
    [Index("current_venue_id", Name = "fk_venue_transfer_approval_current_venue_idx")]
    [Index("employeeIts", Name = "fk_venue_transfer_approval_khidmatguzaar_idx")]
    [Index("requested_by", Name = "fk_venue_transfer_approval_khidmatguzaar1_idx")]
    [Index("reviewed_by", Name = "fk_venue_transfer_approval_khidmatguzaar2_idx")]
    public class venue_transfer_approval
    {
        //add column type for all columns
        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }

        [Column(TypeName = "int(11)")]
        public int requested_by { get; set; }

        [Column(TypeName = "int(11)")]
        public int employeeIts { get; set; }

        [Column(TypeName = "int(11)")]
        public int current_venue_id { get; set; }

        [AllowNull]
        [Column(TypeName = "int(11)")]
        public int? reviewed_by { get; set; }

        [StringLength(15)]
        public string stage { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime requested_on { get; set; }

        [AllowNull]
        [Column(TypeName = "text")]
        public string? approval_comment { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? updated_on { get; set; }

        [ForeignKey("current_venue_id")]
        [InverseProperty("venue_transfer_approval_current_venue")]
        public virtual venue venue { get; set; }
        //add virtual to venue table 

        [ForeignKey("employeeIts")]
        [InverseProperty("venue_transfer_approval_emplyee")]
        public virtual khidmat_guzaar employee { get; set; }

        [ForeignKey("requested_by")]
        [InverseProperty("venue_transfer_approval_requested_by")]
        public virtual khidmat_guzaar requestedBy { get; set; }

        [ForeignKey("reviewed_by")]
        [InverseProperty("venue_transfer_approval_reviewed_by")]
        public virtual khidmat_guzaar reviewedBy { get; set; }
    }
}
