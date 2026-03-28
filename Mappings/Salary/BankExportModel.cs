namespace mahadalzahrawebapi
{
    public class KotakBankExportModel
    {
        public string Client_Code { get; set; }
        public string Product_Code { get; set; }
        public string Payment_Type { get; set; }
        public string Payment_Ref_No { get; set; }
        public string Payment_Date { get; set; }
        public string Instrument_Date { get; set; }
        public string Dr_Ac_No { get; set; }
        public double Amount { get; set; }
        public string Bank_Code_Indicator { get; set; }
        public string Beneficiary_Code { get; set; }
        public string Beneficiary_Name { get; set; }
        public string Beneficiary_Bank { get; set; }
        public string Beneficiary_Branch_IFSC_Code { get; set; }
        public string Beneficiary_Acc_No { get; set; }
        public string Location { get; set; }
        public string Print_Location { get; set; }
        public string Instrument_Number { get; set; }
        public string Ben_Add1 { get; set; }
        public string Ben_Add2 { get; set; }
        public string Ben_Add3 { get; set; }
        public string Ben_Add4 { get; set; }
        public string Beneficiary_Email { get; set; }
        public string Beneficiary_Mobile { get; set; }
        public string Debit_Narration { get; set; }
        public string Credit_Narration { get; set; }
        public string Payment_Details_1 { get; set; }
        public string Payment_Details_2 { get; set; }
        public string Payment_Details_3 { get; set; }
        public string Payment_Details_4 { get; set; }
        public string Enrichment_1 { get; set; }
        public string Enrichment_2 { get; set; }
        public string Enrichment_3 { get; set; }
        public string Enrichment_4 { get; set; }
        public string Enrichment_5 { get; set; }
        public string Enrichment_6 { get; set; }
        public string Enrichment_7 { get; set; }
        public string Enrichment_8 { get; set; }
        public string Enrichment_9 { get; set; }
        public string Enrichment_10 { get; set; }
        public string Enrichment_11 { get; set; }
        public string Enrichment_12 { get; set; }
        public string Enrichment_13 { get; set; }
        public string Enrichment_14 { get; set; }
        public string Enrichment_15 { get; set; }
        public string Enrichment_16 { get; set; }
        public string Enrichment_17 { get; set; }
        public string Enrichment_18 { get; set; }
        public string Enrichment_19 { get; set; }
        public string Enrichment_20 { get; set; }
    }

    public class OtherBankBasicExportModel
    {
        public int srNo { get; set; }
        public string name { get; set; }
        public string accountNumber { get; set; }
        public string bankAccountName { get; set; }
        public string nameOfTheBank { get; set; }
        public string ifscCode { get; set; }
        public int? netSalary { get; set; }

    }
}
