namespace mahadalzahrawebapi.Mappings
{
    public class BMI_Model
    {
        public int? id { get; set; }
        public int? srNo { get; set; }
        public int? itsid { get; set; }
        public float? height_in_Centimeter { get; set; }
        public float? Weight_in_kilogram { get; set; }
        public float? bmi { get; set; }
        public Nullable<DateTime> createdOn { get; set; }
        public string? remarks { get; set; }
        public string? result { get; set; }

    }
}
