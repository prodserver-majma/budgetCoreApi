namespace mahadalzahrawebapi.Mappings
{
    public class AcrualIncomeDashBoardModel
    {
        public int totalIncome { get; set; }
        public int gTotalIncome { get; set; }


        public string psetName { get; set; }

        public float incomePercentage { get; set; }

        public List<FeesAllotmentModel> data { get; set; }
    }
}
