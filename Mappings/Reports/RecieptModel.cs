namespace mahadalzahrawebapi.Mappings
{
    public class Report_FiltersModel
    {
        public int id { get; set; }
        public int? reciptId { get; set; }
        public string? itsId { get; set; }
        public int? deptVenueId { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }


        public int? hijriMonth { get; set; }
        public int? hijriYear { get; set; }
        public bool? isHijri { get; set; }

        public string? type { get; set; }
        public string? copyType { get; set; }
        public suppressDetails? supress { get; set; }


    }

    public class suppressDetails
    {
        public bool basic { get; set; }
        public bool contact { get; set; }

        public bool academic { get; set; }

        public bool khidmat { get; set; }

        public bool house { get; set; }

        public bool family { get; set; }

        public bool qualification { get; set; }

        public bool cw { get; set; }

        public bool lp { get; set; }

        public bool foi { get; set; }

        public bool personality { get; set; }

        public bool aboutyourself { get; set; }

        public bool pastmm { get; set; }

        public bool otheridara { get; set; }

        public bool khidmatm { get; set; }

        public bool salary { get; set; }

        public bool strengthsWeakness { get; set; }
        public bool aspiration { get; set; }
        public bool perfomance { get; set; }
    }

    public class RecieptModel
    {
        public int id { get; set; }
        public int itsId { get; set; }

        public string? chequeNo { get; set; }
        public DateOnly? recieptDate { get; set; }
        public DateOnly? chequeDate { get; set; }
        public DateTime recieptDate_print { get; set; } = DateTime.Now;
        public DateTime printDate { get; set; } = DateTime.Now;
        public DateTime? cancelDate { get; set; }

        public string? bankName { get; set; }
        public string? createdBy { get; set; }
        public string? feePaidAmount { get; set; }
        public string? name { get; set; }
        public string? paymentMode { get; set; }

        public string? receiptId { get; set; }
        public string? receiptNo { get; set; }
        public string? amountInWord { get; set; }
        public string? fromDate { get; set; }
        public string? toDate { get; set; }
        public string? itsCsv { get; set; }
        public string? account { get; set; }
        public string? note { get; set; }
        public string? collectionCenter { get; set; }
        public string? status { get; set; }
        public string? purpose { get; set; }

        public int? studentId { get; set; }
        public int? tdsAmount { get; set; }
    }
}
