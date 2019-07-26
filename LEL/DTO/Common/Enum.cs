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

        /// <summary>
        /// 订单头状态(未派发(0),待接单(3),待发货(6),发货中(7),已发货(20),已完成(1),已结算(100),已取消(5))
        /// </summary>
        public enum OrderHeadStatus
        {
            /// <summary>
            /// 未派发
            /// </summary>
            WeiPaiFa = 0,
            /// <summary>
            /// 待接单
            /// </summary>
            DaiJieDan = 3,
            /// <summary>
            /// 待发货
            /// </summary>
            DaiFaHuo = 6,

            /// <summary>
            /// 发货中
            /// </summary>
            FaHuoZhong = 7,

            /// <summary>
            /// 已发货
            /// </summary>
            YiFaHuo = 4,

            /// <summary>
            /// 已完成
            /// </summary>
            YiWanCheng = 1,

            /// <summary>
            /// 已结算
            /// </summary>
            YiJieSuan = 100,

            /// <summary>
            /// 5.已取消
            /// </summary>
            YiQuXiao = 5,

            

        }

        /// <summary>
        ///  订单行状态(未派发(0),待接单(1),待发货(6),发货中(7),已发货(2),已完成(10),已结算(100),已取消(3))
        /// </summary>
        public enum OrderLineStatus
        {
            /// <summary>
            /// 未派发
            /// </summary>
            WeiPaiFa = 0,
            /// <summary>
            /// 待接单 
            /// </summary>
            DaiJieDan = 1,

            /// <summary>
            /// 待发货
            /// </summary>
            DaiFaHuo = 6,

            /// <summary>
            /// 发货中
            /// </summary>
            FaHuoZhong = 7,

            /// <summary>
            /// 已发货
            /// </summary>
            YiFahuo = 2,

            /// <summary>
            /// 已完成
            /// </summary>
            YiWanCheng = 10,

            /// <summary>
            /// 3.已取消
            /// </summary>
            YiQuXiao = 3,

            /// <summary>
            /// 已结算
            /// </summary>
            YiJieSuan = 100,


        }
    }
}
