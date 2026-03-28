using System.Text.Json;

namespace mahadalzahrawebapi.Mappings.Training
{
    public class TrainingModel
    {

    }
    public class trainingReportModelColHeader
    {
        public string? name { get; set; }
        public string? color { get; set; }
        public int id { get; set; }

    }


    public class TrainingReportModel
    {
        public List<trainingCandidate> rowHeaders { get; set; }
        public List<trainingReportModelColHeader> colHeader { get; set; }

        public List<TrainingStudentQuestionareModel> details { get; set; }
    }

    public class trainingCandidate
    {
        public string? name { get; set; }
        public string? darajah { get; set; }
        public int itsId { get; set; }
        public string? email { get; set; }
        public string? contactNum { get; set; }
        public string? mauze { get; set; }
        public int? farigDarajah { get; set; }
        public int? farigYear { get; set; }

        public string? latestQualification { get; set; }
        public double maqaraatAvg { get; set; }
        public int courseCount { get; set; }
        public string? courseNames { get; set; }
    }

    public class trainingCandidateDropDown
    {
        public string? name { get; set; }
        public int itsId { get; set; }
    }
    public class TrainingStudentQuestionareModel
    {
        public int id { get; set; }
        public int cstId { get; set; }
        public int studentITS { get; set; }
        public string? studentName { get; set; }
        public string? studentMauze { get; set; }
        public string? answers { get; set; }
        public JsonElement? answersOBJ { get; set; }
        public string? status { get; set; }
        public string? marks { get; set; }
        public int acedemicYear { get; set; }
        public int? gradedBy { get; set; }
        public string? teacherName { get; set; }
        public string? remarks { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public string? hijriStart { get; set; }
        public string? hijriEnd { get; set; }
        public string? hijriMonth { get; set; }


        public TrainingSubjectModel? subject { get; set; }
        public trainingCandidate? teacher { get; set; }

        public string? response { get; set; }
        public bool modifyUpload { get; set; }
        public bool modifyAns { get; set; }
        public int subjectId { get; set; }
        public string? qustionare { get; set; }

        public JsonElement? qustionareJson { get; set; }
    }

    public class TrainingClassModel
    {
        public int id { get; set; }
        public int? academicYear { get; set; }
        public string? className { get; set; }
        public int? masoolIts { get; set; }
        public bool deletable { get; set; }

    }
    public class TrainingSubjectModel
    {
        public int id { get; set; }
        public int? academicYear { get; set; }
        public string? name { get; set; }
        public string? status { get; set; }
        public int wheightage { get; set; }
        public string? qustionare { get; set; }
        public JsonElement? qustionareJson { get; set; }
        public bool deletable { get; set; }

    }

    public class TrainingClassStudent
    {
        public int id { get; set; }
        public int classId { get; set; }
        public int studentITS { get; set; }
        public int? rank { get; set; }
        public int? prevRank { get; set; }
        public string? mauze { get; set; }
        public int academicYear { get; set; }
        public int marks { get; set; }
        public int percentage { get; set; }

        public List<int> studentsToTag { get; set; }
        public string? studentName { get; set; }
        public string? className { get; set; }
        public int batchId { get; set; }
        public int age { get; set; }

    }

    public class TrainingClassSubjectTeacherModel
    {
        public int id { get; set; }
        public int classId { get; set; }
        public string? className { get; set; }
        public int subjectId { get; set; }
        public string? subjectName { get; set; }
        public int teacherITS { get; set; }
        public string? teacherName { get; set; }
        public string? status { get; set; }
        public int? acedemicYear { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime? updatedOn { get; set; }
        public int createdBy { get; set; }
        public string? hijriStart { get; set; }
        public string? hijriEnd { get; set; }
        public string? hijriMonth { get; set; }

        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public bool deletable { get; set; }
    }
}
