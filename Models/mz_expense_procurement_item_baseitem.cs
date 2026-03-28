using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models
{
    public class mz_expense_procurement_item_baseitem
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int itemId { get; set; }

        [Column(TypeName = "int(11)")]
        public int baseItemId { get; set; }

    }
}
