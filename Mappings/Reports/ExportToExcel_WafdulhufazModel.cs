namespace mahadalzahrawebapi.Mappings
{
    public class ExportToExcel_WafdulhufazModel
    {
        public List<int> model { get; set; }
        public List<ExportToExcelModel>? toRemove { get; set; }
        public string? categoryName { get; set; }
        public int? categoryId { get; set; }
        public bool? status { get; set; }

    }
}
