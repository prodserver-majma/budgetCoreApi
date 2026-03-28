using mahadalzahrawebapi.GenericModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models;

[Index("itsId", Name = "itsId_UNIQUE", IsUnique = true)]
public partial class employee_salary
{
    internal CalenderModel lastSalaryDateModel;

    [Key]
    [Column(TypeName = "int(11)")]
    public int itsId { get; set; }

    [Column(TypeName = "int(11)")]
    public int grossSalary { get; set; }

    [Column(TypeName = "int(11)")]
    public int? rentAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? marriageAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? mumbaiAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? conveyanceAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? extraAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? arrears { get; set; }

    [Column(TypeName = "int(11)")]
    public int? incomeTax { get; set; }

    [Column(TypeName = "int(11)")]
    public int? localTax { get; set; }

    [Column(TypeName = "int(11)")]
    public int? professionTax { get; set; }

    [Column(TypeName = "int(11)")]
    public int? tds { get; set; }

    [Column(TypeName = "int(11)")]
    public int? qardanHasanah { get; set; }

    [Column(TypeName = "int(11)")]
    public int? marafiqKhairiyah { get; set; }

    [Column(TypeName = "int(11)")]
    public int? sabeel { get; set; }

    [Column(TypeName = "int(11)")]
    public int? bqhs { get; set; }

    public bool? isHijriAllowence { get; set; }

    [Column(TypeName = "int(11)")]
    public int? lessDeduction { get; set; }

    [Column(TypeName = "int(11)")]
    public int? installmentDeduction_Qardan { get; set; }

    [Column(TypeName = "int(11)")]
    public int? husainiQardanHasanah { get; set; }

    public bool? isHusainiQardan { get; set; }

    [Column(TypeName = "int(11)")]
    public int? mohammediQardanHasanah { get; set; }

    [Column(TypeName = "int(11)")]
    public int? qardanHasanahRefundable { get; set; }


    [Column(TypeName = "int(11)")]
    public int? qardanHasanahNonRefundable { get; set; }

    [Column(TypeName = "int(11)")]
    public int? withHoldings { get; set; }

    public bool isMahadSalary { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fmbAllowance { get; set; }

    [Column(TypeName = "int(11)")]
    public int? fmbDeduction { get; set; }

    [StringLength(10)]
    public string currency { get; set; }

    [Column(TypeName = "date")]
    public DateTime? lastSalaryDate { get; set; }

    [Column(TypeName = "int(11)")]
    public int? lastSalary { get; set; }

    [ForeignKey("itsId")]
    [InverseProperty("employee_salary")]
    public virtual khidmat_guzaar its { get; set; }
}
