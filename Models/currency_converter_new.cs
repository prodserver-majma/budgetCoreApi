namespace mahadalzahrawebapi.Models
{
    public class currency_converter_new
    {
        public int id { get; set; }
        public string fromCurrencyName { get; set; }
        public string toCurrencyName { get; set; }
        public float? value { get; set; }
    }
}
