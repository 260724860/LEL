namespace DTO.Common
{
    public class Enum
    {
        /// <summary>
        /// 商品销量排序类型
        /// </summary>
        public enum GoodsSalesReportOrderByType
        {
            SalesAmountAsc = 1,
            SalesAmountDesc = 2,
            SalesCountAsc = 3,
            SalesCountDesc = 4,
            TransactiontimesAsc = 5,
            TransactiontimesDesc = 6,
            ProfitSumAsc = 7,
            ProfitSumDesc = 8,
        }
        /// <summary>
        /// 订单详细排序
        /// </summary>
        public enum OrderDetailOrderByType
        {
            UpdateTimeAsc=1,
            UpdateTimeDesc=2,
            GoodsID=3,
            GoodsPriceAsc=4,
            GoodsPriceDesc=5,
            ProfitAsc=6,
            ProfitDesc=7

        }

    }
}
