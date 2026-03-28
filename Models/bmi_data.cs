namespace mahadalzahrawebapi.Models
{
    public class bmi_data
    {
        public int id { get; set; }
        public int? itsId { get; set; }
        public float? height_in_cemtimeter { get; set; }
        public float? weight_in_kilogram { get; set; }
        public DateTime? createdOn { get; set; }
        public float? bmi { get; set; }
    }
}
