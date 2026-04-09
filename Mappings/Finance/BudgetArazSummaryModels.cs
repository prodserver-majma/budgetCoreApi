using Amazon.S3.Model.Internal.MarshallTransformations;
using mahadalzahrawebapi.GenericModels;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.InteropServices;


namespace mahadalzahrawebapi.Mappings
{
    public class MahadIncome_Branch
    {
        public List<MahadIncome> Elearning { get; set; }
        public List<MahadIncome> Talim_Tamam { get; set; }
        public List<MahadIncome> Atfaal { get; set; }
        public List<MahadIncome> Talim_al_Quran { get; set; }

        public List<MahadIncome> Camp { get; set; }
        public List<MahadIncome> Mauze_Tahfeez { get; set; }

        public List<MahadIncome> Nisab { get; set; }

    }

    public class ChartLabel
    {
        public List<int> data { get; set; }

        public string label { get; set; }
    }

    public class BudgetArazItem
    {
        public int id { get; set; }
        public int itemId { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public string? uom { get; set; }
        public int srno { get; set; } = 0;
        public string? description { get; set; }
        public float? perUnitAmt { get; set; } = 0;
        public int? quantity { get; set; } = 1;
        public float total { get; set; } = 0;
        public float rate { get; set; }
        public bool verified { get; set; } = false;
        public bool isConcerning { get; set; } = false;
        public bool hasIssues { get; set; } = false;
        public string? remark { get; set; }
        public string? baseItemName { get; set; }
        public string? deptVenueName { get; set; }
        public string? stage { get; set; }
        public string? createdOn { get; set; }
        public string? createdBy { get; set; }
        public bool isExpense { get; set; } = true;
        public bool? isNonOpIncome { get; set; }
        public bool? isDawatReceipt { get; set; }
        public bool? isOtherReceipt { get; set; }
        public bool? isOtherPayment { get; set; }
        public string? frequency { get; set; }
        public List<mz_expense_budget_araz_monthly>? months { get; set; } = new List<mz_expense_budget_araz_monthly>();
        public List<studentFeesMonthly>? incomeMonths { get; set; } = new List<studentFeesMonthly>();
    }

    public class BudgetSmartGoal
    {
        public int id { get; set; }
        public int itsId { get; set; }
        public string category { get; set; }
        public string specific { get; set; }
        public string measurable { get; set; }
        public string attainable { get; set; }
        public string relevant { get; set; }
        public DateRange time { get; set; }
        public string timeFrom { get; set; }
        public string timeTo { get; set; }
        public int srno { get; set; }
        public string description { get; set; }
        public bool verified { get; set; }
        public bool isConcerning { get; set; }
        public bool hasIssues { get; set; }
        public string remark { get; set; }
        public string deptVenueName { get; set; }
        public int deptVenueId { get; set; }
        public string stage { get; set; }
        public string createdOn { get; set; }
        public string createdBy { get; set; }
    }

    public class School
    {
        public String school_name { get; set; }
        public List<total_section> total_section { get; set; } = new List<total_section>();
    }
    public class total_section
    {
        public string section { get; set; }
        public float total { get; set; }
    }

    public class BudgetArazExpenseHead
    {
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<bool> status { get; set; }
        public bool isCapital { get; set; }
        public bool isConcerning { get; set; }
        public bool isExpense { get; set; }
        public bool? isNonOpIncome { get; set; }
        public bool? isDawatReceipt { get; set; }
        public bool? isOtherReceipt { get; set; }
        public bool? isOtherPayment { get; set; }
        public bool verified { get; set; }
        public float total { get; set; }

        public List<total_section> total_section { get; set; } = new List<total_section>();
        //public Dictionary<String, int>? total_section { get; set; } = new Dictionary<string, int>();

        public List<BudgetArazItem> items { get; set; }
    }

    public class BudgetArazDeptVenue
    {
        public int id { get; set; }
        public string name { get; set; }
        public int deptId { get; set; }
        public string masterDeptName { get; set; }
        public string deptName { get; set; }
        public string venueName { get; set; }
        public int venueId { get; set; }
        public int? classId { get; set; }

        public List<BudgetArazExpenseHead> expenseHeads { get; set; }
        public float total { get; set; }

        public int? pset { get; set; }
        public string? className { get; set; }
    }

    public class BudgetArazDept
    {
        public int id { get; set; }
        public string name { get; set; }
        public float total { get; set; }
        public int? pset { get; set; }

        public List<BudgetArazDeptVenue> deptVenues { get; set; }
    }

    public class BudgetSummaryColHeaderGroup
    {
        public int id { get; set; }
        //public int groupId { get; set; }
        public int count { get; set; }
        public string name { get; set; }
        public string deptVenueName { get; set; }
        public bool isIncluded { get; set; }
        public string color { get; set; }
        public bool show { get; set; }
        public string? school { get; set; }
    }

    public class BudgetSummaryColHeader
    {
        public int id { get; set; }
        public int count { get; set; }

        public int? psetId {get; set;}
        public int groupId { get; set; }
        public string name { get; set; }
        public string deptVenueName { get; set; }
        public bool isIncluded { get; set; }
        public bool show { get; set; }
        public string color { get; set; }
        public string? school { get; set; }
        public string? className { get; set; }

        public dept_venue deptVenue { get; set; }
    }

    public class BudgetSummaryRowHeader
    {
        public int id { get; set; }
        public float Total { get; set; }
        public string name { get; set; }
        public bool isExpense { get; set; }
        public bool isCapital { get; set; }
        public bool? isNonOpIncome { get; set; }
        public bool? isDawatReceipt { get; set; }
        public bool? isOtherReceipt { get; set; }
        public bool? isOtherPayment { get; set; }
        public bool isIncluded { get; set; }
        public bool show { get; set; }
        public string[]? section { get; set; }
        public string? school { get; set; }

        public List<School> schools { get; set; } = new List<School>();
        //public List<total_section> total_section { get; set; } = new List<total_section>();
        //public Dictionary<String, int>? total_section { get; set; } = new Dictionary<string, int>();
        public mz_expense_procurement_baseitem expensehead { get; set; }
    }

    public class BudgetArazSummaryModels
    {

        public List<BudgetSummaryRowHeader> rowHeader { get; set; }
        public List<BudgetSummaryColHeaderGroup> colHeaderGroup { get; set; }
        public List<BudgetSummaryColHeader> colHeader { get; set; }

        public List<BudgetSummarySection> sectionHeader { get; set; }
        public List<BudgetArazDept> summary { get; set; }
    }

    public class BudgetSummarySection
    {
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
    }
    public class BudgetArazExpenseItemModel
    {
        public int arazId { get; set; }
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int quantity { get; set; }
        public string uom { get; set; }
        public float consumedBudget { get; set; }
        public float transferedAmount { get; set; }
        public float arazAmount { get; set; }
        public int? currQuarterAmount { get; set; }
        public int arazQuantity { get; set; }
        public int arazPricePerQuantity { get; set; }
        public string arazJustification { get; set; }
        public string transferedFromLog { get; set; }
        public string transferedToLog { get; set; }
        public float? consumedQuantity { get; set; }
        public float? averagePerUnitCost { get; set; }
        public string billRemarks { get; set; }
        public List<mz_expense_bill_item> billItems { get; set; }
    }
    public class BudgetArazExpenseModel
    {
        public int id { get; set; }
        public int? venueId { get; set; }
        public bool select { get; set; }
        public int deptVenueId { get; set; }
        public int psetId { get; set; }
        public int baseItemId { get; set; }
        public int itemId { get; set; }
        public float amountPerUom { get; set; }
        public int quantity { get; set; } = 1;
        public string? uom { get; set; }
        public string? justification { get; set; }
        public DateTime? createdOn { get; set; }
        public string? createdBy { get; set; }
        public string? remarks_admin { get; set; }
        public DateTime? updatedOn { get; set; }
        public string? updatedBy { get; set; }
        public string? stage { get; set; }
        public bool verified { get; set; } = false;
        public bool isConcerning { get; set; } = false;
        public bool itemLock { get; set; } = true;

        public int? financialYear { get; set; }

        public string? deptVenueName { get; set; }
        public string? baseItemName { get; set; }
        public string? itemName { get; set; }

        public float totalAmount { get; set; } = 0;
        public int transferredAmount { get; set; } = 0;

        public float totalExpense { get; set; } = 0;

        public string? adminWorkStatusString { get; set; }

        public int quarter { get; set; }

        public int? currQuarterSanctionAmount { get; set; }
        public int? classId { get; set; }
        public List<BudgetArazExpenseItemModel>? items { get; set; }

    }

    public class BudgetArazExpenseMonthlyModel
    {
        public string? month { get; set; }
        public int? quantity { get; set; }
        public double? amount { get; set; }
        public double? total { get; set; }
    }

    public class BudgetRequestModel
    {
        public BudgetArazExpenseModel Expense { get; set; } = new BudgetArazExpenseModel();
        public List<BudgetArazExpenseMonthlyModel> month { get; set; } = new List<BudgetArazExpenseMonthlyModel>();
    }

    public class IncomeRequestModel
    {
        public BudgetArazItem Income { get; set; } = new BudgetArazItem();
        public List<BudgetArazExpenseMonthlyModel> month { get; set; } = new List<BudgetArazExpenseMonthlyModel>();
    }

    public class BudgetTransferLogModel
    {
        public long? Id { get; set; }
        public string? FromBdtag { get; set; }
        public string? ToBdtag { get; set; }
        public int Amount { get; set; }
        public int from_baseitem { get; set; }
        public string? from_baseitemName { get; set; }
        public int from_deptvenueid { get; set; }
        public string? from_deptvenueName { get; set; }
        public int from_itemid { get; set; }
        public string? from_itemName { get; set; }

        public int to_baseitem { get; set; }
        public string? to_baseitemName { get; set; }
        public int to_deptvenueid { get; set; }
        public string? to_deptvenueName { get; set; }
        public int to_itemid { get; set; }
        public string? to_itemName { get; set; }
        public int? to_quantity { get; set; }


        public string? remarks { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
    }
}
