using System.Text.Json;

namespace mahadalzahrawebapi.Mappings.Training
{
    public class training_student_subject_marksheet_dto
    {

        public int id { get; set; }

        public int cstId { get; set; }

        public int studentITS { get; set; }

        public string? answers { get; set; }

        public string? status { get; set; }

        public int? marks { get; set; }

        public int acedemicYear { get; set; }

        public int? gradedBy { get; set; }

        public DateOnly startDate { get; set; }

        public DateOnly endDate { get; set; }

        public string? remarks { get; set; }

        public string? qustionare { get; set; }
        public JsonElement? qustionareJson { get; set; }
    }

}
