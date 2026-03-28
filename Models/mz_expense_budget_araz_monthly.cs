using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;
namespace mahadalzahrawebapi.Models
{
    public partial class mz_expense_budget_araz_monthly
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }

        [Column(TypeName = "int(11)")]
        public int budget_araz_id { get; set; }

        [Column(TypeName = "varchar(16)")]
        public string month_num { get; set; }

        [Column(TypeName = "double(11)")]
        public double amount { get; set; }

        [Column(TypeName = "double(11)")]
        public double quantity { get; set; }

        [Column(TypeName = "int(11)")]
        public int deptVenueId { get; set; }

        [Column(TypeName = "int(11)")]
        public int baseItemId { get; set; }

        [Column(TypeName = "int(11)")]
        public int itemId { get; set; }

        [Column(TypeName = "int(11)")]
        public float consumedAmount { get; set; }

        [Column(TypeName = "int(11)")]
        public float consumedQuantity { get; set; }

        [Column(TypeName = "int(11)")]
        public int transferredAmount { get; set; }

        [StringLength(45)]
        public string status { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? created_on { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? modified_on { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey("budget_araz_id")]
        [InverseProperty("mz_expense_budget_araz_monthly")]
        public virtual mz_expense_budget_araz? budgetArazMonthly { get; set; }

        // [ForeignKey("baseItemId")]
        // [InverseProperty("mz_expense_budget_araz_monthly")]
        // public virtual mz_expense_procurement_baseitem baseItem { get; set; }

        // [ForeignKey("deptVenueId")]
        // [InverseProperty("mz_expense_budget_araz_monthly")]
        // public virtual dept_venue deptVenue { get; set; }

        // [ForeignKey("itemId")]
        // [InverseProperty("mz_expense_budget_araz_monthly")]
        // public virtual mz_expense_procurement_item item { get; set; }

        // [ForeignKey("budget_araz_id")]
        // [InverseProperty("mz_expense_budget_araz_monthly")]
        // public virtual mz_expense_budget_araz budget_id { get; set; }
    }
}
