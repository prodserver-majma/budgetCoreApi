
namespace mahadalzahrawebapi.Mappings
{
    public class nisaab_alumnus_dto
    {
        public int itsId { get; set; }

        public bool? jamea { get; set; }

        public string? degree { get; set; }

        public int? farigYear { get; set; }

        public int? farigDarajah { get; set; }

        public int? batchId { get; set; }

        public bool hafizAtFarig { get; set; }

        public int? kgIts { get; set; }

        public virtual mz_student_dto its { get; set; }
    }
}
