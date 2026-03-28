namespace mahadalzahrawebapi.GenericModels
{
    public class CalenderModel
    {
        public int engDay { get; set; }
        public int engMonth { get; set; }
        public int engYear { get; set; }
        public DateTime engDate { get; set; }

        public int hijDay { get; set; }
        public int hijMonth { get; set; }
        public int hijYear { get; set; }

        public string? hijMonthName { get; set; }
        public int acedemicYear { get; set; }
        public string? acedemicYearName { get; set; }
    }

    public class DayModel
    {
        public int id { get; set; }
        public int gregDay { get; set; }
        public int gregMonth { get; set; }
        public string gregMonthName { get; set; }
        public int hijriDay { get; set; }
        public int hijriMonth { get; set; }
        public int hijriYear { get; set; }
        public string hijriMonthName { get; set; }
        public int dayOfWeek { get; set; }
        public bool isCurrentMonth { get; set; }
        public bool isChecked { get; set; }
        public bool isSelectable { get; set; }
        public bool isToday { get; set; }
        public bool isPast { get; set; }
        public string engdate { get; set; }

        public List<eventsModel> events { get; set; }
    }
    public class eventsModel
    {
        public int id { get; set; }
        public int srn { get; set; }
        public string eventName { get; set; }
        public string eventType { get; set; }
        public string details { get; set; }
    }
    public class WeekModel
    {
        public int weekNumber { get; set; }
        public List<DayModel> days { get; set; }
    }

    public class MonthlyCallenderModel
    {
        public List<WeekModel> weeks { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int hijriYear { get; set; }
        public int hijriMonth { get; set; }
    }
}
