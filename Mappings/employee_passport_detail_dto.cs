namespace mahadalzahrawebapi.Mappings
{
    public class employee_passport_detail_dto
    {
        public int id { get; set; }

        public int itsId { get; set; }

        public string? passportName { get; set; }

        public string? passportNo { get; set; }

        public string? dateOfIssue { get; set; }

        public string? dateOfExpiry { get; set; }

        public string? placeOfIssue { get; set; }

        public string? passportPlaceOfBirth { get; set; }

        public string? dobPassport { get; set; }

        public string? passportCopy { get; set; }
    }
}
