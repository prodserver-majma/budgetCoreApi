namespace mahadalzahrawebapi.Mappings.Finance
{
    public class FacultyMedicalEnayatBillSubmit
    {
        public int id { get; set; }
        public int srNo { get; set; }
        public bool select { get; set; }
        public int? billPeriod { get; set; }
        public string billRequestFor { get; set; }
        public string billNumber { get; set; }
        public int relationItsId { get; set; }
        public string originalBillStatus { get; set; }
        public int? billRelation { get; set; }
        public string billType { get; set; }
        public Nullable<DateTime> billDate { get; set; }
        public string billFrom { get; set; }
        public string moze { get; set; }

        public int? billAmount { get; set; }
        public string illness { get; set; }
        public string billPeriodName { get; set; }
        public string billRelationName { get; set; }
        public bool? billStatus { get; set; }
        public string status { get; set; }
        public int? aplicantItsId { get; set; }
        public string aplicantName { get; set; }
        public int currency { get; set; }
        public string currencysymbol { get; set; }

        public string attachment { get; set; }

        public int amount_billClearance { get; set; }
        public string photo { get; set; }
        public string category { get; set; }
        public string rowCss { get; set; }

        public string originalBillStatusCss { get; set; }
        public string suratAuditStatus { get; set; }
        public string suratAuditStatuscss { get; set; }

        public bool ischangeoriginalBillStatus { get; set; }

        public int rowSpan { get; set; }

        public bool display { get; set; }
        public string term { get; set; }
        public string year { get; set; }
        public int sum { get; set; }


    }
}
