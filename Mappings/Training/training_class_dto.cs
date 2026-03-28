namespace mahadalzahrawebapi.Mappings.Training
{
    public class training_class_dto
    {

        public int id { get; set; }
        public string? className { get; set; }
        public int? masoolIts { get; set; }
        public virtual ICollection<training_class_student_dto> training_class_students { get; set; } = new List<training_class_student_dto>();
        public virtual ICollection<training_class_subject_teacher_dto> training_class_subject_teachers { get; set; } = new List<training_class_subject_teacher_dto>();

    }
}
