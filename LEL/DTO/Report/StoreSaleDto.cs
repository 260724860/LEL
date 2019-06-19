namespace DTO.Report
{
    /// <summary>
    /// 商品销量统计
    /// </summary>
    public class StoreSaleDto
    {
        public double SalesAmount { get; set; }
        public int SalesCount { get; set; }
        public int Transactiontimes { get; set; }
        public int usersid { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        public double? ProfitSum { get; set; }
        public string UsersName { get; set; }
        public string UsersNickname { get; set; }
        public string UsersMobilePhone { get; set; }

    }
}
