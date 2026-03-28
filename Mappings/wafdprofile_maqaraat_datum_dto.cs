
namespace mahadalzahrawebapi.Mappings
{
    public class wafdprofile_maqaraat_datum_dto
    {

        public int id { get; set; }

        public int sessionId { get; set; }

        public int studentItsId { get; set; }

        public int? marks { get; set; }

        public bool? isPresent { get; set; }

        public string? absentReason { get; set; }

        public virtual wafdprofile_maqaraat_session_dto session { get; set; }
    }
}
