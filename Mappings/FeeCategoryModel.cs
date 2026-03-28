namespace mahadalzahrawebapi.Mappings
{
    public class FeeCategoryModel
    {
        public int id { get; set; }
        public int? categoryId { get; set; }
        public string? categoryName { get; set; }
        public string? psetName { get; set; }

        public int? psetId { get; set; }
        public int? amount { get; set; }
        public string? currency { get; set; }
        public int? studentCount { get; set; }
        public string? frequency { get; set; }


    }

    public class studentFeesMonthly
    {
        public string month { get; set; }
        public int student_count { get; set; }
        public int fees_per_student { get; set; }
    }

    public class incomeEstimate
    {
        public FeeCategoryModel income { get; set; } = new FeeCategoryModel();
        public List<studentFeesMonthly> month { get; set; } = new List<studentFeesMonthly>();
    }
}
