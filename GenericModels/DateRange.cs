namespace mahadalzahrawebapi.GenericModels
{

    public class DateRange
    {
        public DateRange() { }
        public DateRange(DateTime from, DateTime to)
        {
            this.FromDate = from;
            this.ToDate = to;
        }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public bool HasValue
        {
            get
            {
                return this.FromDate.HasValue && this.ToDate.HasValue;
            }
        }
    }
}
