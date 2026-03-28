
namespace mahadalzahrawebapi.Mappings
{
    public class employee_log_dto
    {

        public int id { get; set; }

        public int itsId { get; set; }

        public int updatedby { get; set; }

        public DateTime updatedon { get; set; }

        public string? status { get; set; }
    }
}
