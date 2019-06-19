namespace DTO.Report
{
    /// <summary>
    /// 供应商销量统计
    /// </summary>
    public class SupplierSaleDto
    {
        public double SalesAmount { get; set; }
        public int SalesCount { get; set; }
        public int Transactiontimes { get; set; }
        public int SuppliersID { get; set; }
        public string ResponPeople { get; set; }
        public string SuppliersName { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        public double? ProfitSum { get; set; }
        public string MobilePhone { get; set; }
    }
}
