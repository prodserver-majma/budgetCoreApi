
namespace mahadalzahrawebapi.Mappings
{
    public class venue_dto
    {
        public int Id { get; set; }

        public string? CampVenue { get; set; }

        public string? CampId { get; set; }

        public bool? ActiveStatus { get; set; }

        public int? CashBalance { get; set; }

        public string? currency { get; set; }

        public string? qismTahfeez { get; set; }

        public string? ho { get; set; }

        public string? displayName { get; set; }

        public int? qismId { get; set; }
        public List<pset_dto> pset { get; set; }

    }

    public class pset_dto
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
