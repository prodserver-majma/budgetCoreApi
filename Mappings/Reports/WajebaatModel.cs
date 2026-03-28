namespace mahadalzahrawebapi.Mappings
{
    public class WajebaatModel
    {
        public int id { get; set; }
        public bool select { get; set; }

        public int? itsId { get; set; }
        public int? hijriYear { get; set; }
        public int? niyyatAmount { get; set; }

        public float takhmeenAmount { get; set; }
        public string? takhmeenAmountString { get; set; }
        public int? paidAmount { get; set; }
        public string? currency { get; set; }
        public string? bankName { get; set; }
        public string? draftNo { get; set; }
        public DateTime? draftDate { get; set; }
        public DateTime? createdOn { get; set; }
        public string? createdBy { get; set; }
        public DateTime? updatedOn { get; set; }
        public string? updatedBy { get; set; }
        public string? officeRemarks { get; set; }
        public string? userRemarks { get; set; }

        public string? name { get; set; }
        public string? gender { get; set; }
        public string? age { get; set; }

        public string? khidmatMoze { get; set; }

        public string? mzIdara { get; set; }
        public float lastYearWajebaat { get; set; }
        public float thisYear_currencyrate { get; set; }
        public float lastYear_currencyrate { get; set; }
        public string? thisYear_currency { get; set; }
        public string? lastYear_currency { get; set; }
        public string? wajebaatType { get; set; }
        public string? stage { get; set; }

    }
}
