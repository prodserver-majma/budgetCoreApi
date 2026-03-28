namespace mahadalzahrawebapi.Mappings
{
    public class IncomeDashBoardModel
    {
        public int totalIncome { get; set; }
        public int gTotalIncome { get; set; }

        public int cashAmount { get; set; }

        public int chequeAmount { get; set; }
        public string currency { get; set; }

        public int onlineAmount { get; set; }

        public int walletAmount { get; set; }
        public string psetName { get; set; }

        public float incomePercentage { get; set; }

        public List<ChartLabel> data { get; set; }

    }

    public class IncomeExpenseDashBoardModel
    {
        public int totalIncome { get; set; }
        public int totalExpense { get; set; }
        public int gTotalIncome { get; set; }

        public int cashAmount { get; set; }

        public int chequeAmount { get; set; }
        public string currency { get; set; }

        public int onlineAmount { get; set; }

        public int walletAmount { get; set; }
        public string psetName { get; set; }

        public float incomePercentage { get; set; }

        public List<ChartLabel> data { get; set; }

    }
}
