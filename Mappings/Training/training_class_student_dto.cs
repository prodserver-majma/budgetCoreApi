namespace mahadalzahrawebapi.Mappings.Training
{
    public class training_class_student_dto
    {
        public int id { get; set; }

        public int classId { get; set; }

        public int studentITS { get; set; }

        public int? rank { get; set; }

        public int? prevRank { get; set; }

        public string? mauze { get; set; }

        public int academicYear { get; set; }

        public int? marks { get; set; }

        public int? percentage { get; set; }
        public virtual training_class_dto _class { get; set; }
    }

}
