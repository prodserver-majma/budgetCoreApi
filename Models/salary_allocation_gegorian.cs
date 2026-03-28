using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("packageId", Name = "fk_salary_allocation_g_exp_package_idx")]
[Index("itsId", "month", "year", Name = "secondary", IsUnique = true)]
public partial class salary_allocation_gegorian
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int salary { get; set; }

    [Column(TypeName = "int(11)")]
    public int? rentAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? marriageAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? convenienceAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? mumbaiAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fmbAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? lessDeduction { get; set; }

    [Column(TypeName = "int(11)")]
    public int? extraAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? overtime { get; set; }

    [Column(TypeName = "int(11)")]
    public int? shortfall { get; set; }

    [Column(TypeName = "int(11)")]
    public int? arrears { get; set; }

    [Column(TypeName = "int(11)")]
    public int ctc { get; set; }

    [Column(TypeName = "int(11)")]
    public int? professionTax { get; set; }

    [Column(TypeName = "int(11)")]
    public int? tds { get; set; }

    [Column(TypeName = "int(11)")]
    public int? qardanHasanah { get; set; }

    [Column(TypeName = "int(11)")]
    public int? sabeel { get; set; }

    [Column(TypeName = "int(11)")]
    public int? marafiqKhairiyah { get; set; }

    [Required]
    [StringLength(50)]
    public string currency { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fmbDeduction { get; set; }

    [Column(TypeName = "int(11)")]
    public int? bqhs { get; set; }

    [Column(TypeName = "int(11)")]
    public int? mohammedi_qardanHasanah { get; set; }

    [Column(TypeName = "int(11)")]
    public int? husaini_qardanHasanah { get; set; }

    [Column(TypeName = "int(11)")]
    public int? installmentDeduction_Qardan { get; set; }

    [Column(TypeName = "int(11)")]
    public int? qardanHasanahRefundable { get; set; }

    [Column(TypeName = "int(11)")]
    public int? qardanHasanahNonRefundable { get; set; }

    [Column(TypeName = "int(11)")]
    public int? withHoldings { get; set; }

    [Column(TypeName = "int(11)")]
    public int? incomeTax { get; set; }

    [Column(TypeName = "int(11)")]
    public int? localTax { get; set; }

    [Column(TypeName = "int(11)")]
    public int netEarnings { get; set; }

    [Column(TypeName = "int(11)")]
    public int? timeDelta { get; set; }

    [Column(TypeName = "int(11)")]
    public int? dayDelta { get; set; }

    [Column(TypeName = "int(11)")]
    public int month { get; set; }

    [Column(TypeName = "int(11)")]
    public int year { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime salaryFrom { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime salaryTo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime createdOn { get; set; }

    [Column(TypeName = "text")]
    public string createdBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? paymentDate { get; set; }

    [Column(TypeName = "text")]
    public string? systemRemarks { get; set; }

    [Column(TypeName = "text")]
    public string? qismRemarks { get; set; }

    [Required]
    [Column(TypeName = "int(11)")]
    public int packageId { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("salary_allocation_gegorian")]
    public virtual khidmat_guzaar its { get; set; }

    [ForeignKey("packageId")]
    [InverseProperty("salary_allocation_gegorian")]
    public virtual payroll_salary_packages package { get; set; }

    [InverseProperty("allocation")]
    public virtual ICollection<salary_generation_gegorgian> salary_generation_gegorgian { get; set; } = new List<salary_generation_gegorgian>();
}
