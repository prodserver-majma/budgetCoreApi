using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models
{
    public partial class mz_expense_school_section_class
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }

        [Column(TypeName = "int(11)")]
        public int dept_venue_id { get; set; }

        [Column(TypeName = "int(11)")]
        public int section_id { get; set; }

        [Column(TypeName = "int(11)")]
        public int class_id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime created_on { get; set; }
    }
}
