
namespace mahadalzahrawebapi.Mappings
{
    public class wafdprofile_maqaraat_session_dto
    {
        public int id { get; set; }

        public int teacherItsId { get; set; }

        public int? dayId { get; set; }

        public DateTime? createdOn { get; set; }

        public string? createdBy { get; set; }

        public bool? isEvaluated { get; set; }

        public string? reason { get; set; }

        public int? juz { get; set; }

        public int? acedemicYear { get; set; }

        public int? pages { get; set; }

        public DateOnly sessionDate { get; set; }

        public virtual ICollection<wafdprofile_maqaraat_datum_dto> wafdprofile_maqaraat_data { get; set; } = new List<wafdprofile_maqaraat_datum_dto>();

    }
}
