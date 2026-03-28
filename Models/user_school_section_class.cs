using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mahadalzahrawebapi.Models
{
    public partial class user_school_section_class
    {
        [Key]
        [Column(TypeName = "INT(11)")]
        public int id { get; set; }

        [Column(TypeName = "INT(11)")]
        public int school_section_class_id { get; set; }

        [Column(TypeName = "INT(11)")]
        public int itsId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime created_on { get; set; }
    }
}
