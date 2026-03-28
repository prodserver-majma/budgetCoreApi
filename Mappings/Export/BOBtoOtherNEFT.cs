namespace mahadalzahrawedapi.Mappings.Export
{
    public class BOBtoOtherNEFT
    {
        public int srNo { get; set; }
        public int? amount { get; set; }
        public string ifsc { get; set; }

        public string bankAccountNumber { get; set; }
        public string bankAccountName { get; set; }
        public string text { get; set; }

    }

    public class BOBToBOBNEFT
    {
        public string accountNumber1 { get; set; }
        public string accountNumber2 { get; set; }
        public string bankAccountName { get; set; }
        public string ifsc { get; set; }
        public string sol { get; set; }
        public int amount { get; set; }
        public string text { get; set; }
    }
}
