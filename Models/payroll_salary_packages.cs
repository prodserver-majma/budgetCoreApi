using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models;

public partial class payroll_salary_packages
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "text")]
    public string name { get; set; }

    [Column(TypeName = "int(11)")]
    public int amount { get; set; }

    [Column(TypeName = "text")]
    public string description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? paymentDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? fromDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? toDate { get; set; }

    [Column(TypeName = "int(11)")]
    public int qismId { get; set; }

    [Column(TypeName = "int(11)")]
    public int createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdOn { get; set; }

    [Required]
    [StringLength(45)]
    public string paymentFrom { get; set; }

    [InverseProperty("package")]
    public virtual ICollection<salary_allocation_gegorian> salary_allocation_gegorian { get; set; } = new List<salary_allocation_gegorian>();

    [InverseProperty("package")]
    public virtual ICollection<salary_allocation_hijri> salary_allocation_hijri { get; set; } = new List<salary_allocation_hijri>();

    [ForeignKey("qismId")]
    public virtual qism_al_tahfeez qism { get; set; }

    [ForeignKey("createdBy")]
    public virtual khidmat_guzaar user { get; set; }
}
