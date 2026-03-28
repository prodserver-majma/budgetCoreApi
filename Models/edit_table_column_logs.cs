using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace mahadalzahrawebapi.Models
{
    //create index for all columns
    [Index("edited_by", Name = "fk_edit_table_column_logs_khidmatguzaar_idx")]
    [Index("table_name", Name = "edit_table_column_logs_table_name_idx")]
    [Index("column_name", Name = "edit_table_column_logs_column_name_idx")]
    [Index("table_primary_key_value", Name = "edit_table_column_logs_table_primary_key_value_idx")]
    public class edit_table_column_logs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }

        [Column(TypeName = "text")]
        public string old_value { get; set; }

        [Column(TypeName = "text")]
        public string new_value { get; set; }

        [Column(TypeName = "int(11)")]
        public int edited_by { get; set; }

        [StringLength(36)]
        public string table_name { get; set; }
        [StringLength(36)]
        public string column_name { get; set; }

        [StringLength(36)]
        public string table_primary_key_value { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime edit_date_time { get; set; }

        [ForeignKey("edited_by")]
        [InverseProperty("edit_table_column_logs_editted_by")]
        public virtual khidmat_guzaar editedBy { get; set; }
    }
}
