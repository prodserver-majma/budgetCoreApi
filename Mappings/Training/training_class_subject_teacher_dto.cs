namespace mahadalzahrawebapi.Mappings.Training
{
    public class training_class_subject_teacher_dto
    {

        public int id { get; set; }

        public int classId { get; set; }

        public int subjectId { get; set; }

        public int teacherITS { get; set; }

        public string? status { get; set; }

        public int? acedemicYear { get; set; }

        public DateTime createdOn { get; set; }

        public DateTime? updatedOn { get; set; }

        public int createdBy { get; set; }

        public DateOnly startDate { get; set; }

        public DateOnly endDate { get; set; }

        public virtual training_class_dto _class { get; set; }

        public virtual training_subject_dto subject { get; set; }

        public virtual ICollection<training_student_subject_marksheet_dto> training_student_subject_marksheets { get; set; } = new List<training_student_subject_marksheet_dto>();

    }
}
