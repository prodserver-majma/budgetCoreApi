
namespace mahadalzahrawebapi.Mappings
{
    public class employee_e_attendence_dto
    {

        public int itsId { get; set; }

        public DateOnly date { get; set; }

        public DateTime? entryMorning { get; set; }

        public DateTime? exitMorning { get; set; }

        public DateTime? entryEvening { get; set; }

        public DateTime? exitEvening { get; set; }

        public int? extraHour { get; set; }

        public string? logJson { get; set; }

    }
}
