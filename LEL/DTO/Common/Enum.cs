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
        /// 订单状态(0.未确认；1.已完成；2.已审核；3.待接单；4.已接单；5.已取消;6已结算
        /// </summary>
        public enum OrderStatus
        {
            /// <summary>
            /// 未确认
            /// </summary>
            WeiQueRen = 0,
            /// <summary>
            /// 2.已完成
            /// </summary>
            YiWanCheng = 1,
            /// <summary>
            /// 2.已审核
            /// </summary>
            YiShengHe = 2,
            /// <summary>
            /// 3.待接单
            /// </summary>
            DaiJieDan = 3,
            /// <summary>
            /// 4.已接单
            /// </summary>
            YiJieDan = 4,
            /// <summary>
            /// 5.已取消
            /// </summary>
            YiQuXiao = 5,

            /// <summary>
            /// 待发货
            /// </summary>
            DaiFaHuo=6,

            /// <summary>
            /// 6已结算
            /// </summary>
            YiJieSuan = 7
        }

        /// <summary>
        ///  订单行状态(0.未派发；1.待接单；2.已接单；3.已取消 4.待发货.5发货中6.已完成 7已结算)
        /// </summary>
        public enum OrderLineStatus
        {
            /// <summary>
            /// 未派发
            /// </summary>
            WeiPaiFa = 0,
            /// <summary>
            /// 1.待接单 派发订单操作时
            /// </summary>
            DaiJieDan = 1,
            /// <summary>
            /// 2.已接单
            /// </summary>
            YiJieDan = 2,
            /// <summary>
            /// 3.已取消
            /// </summary>
            YiQuXiao = 3,
            /// <summary>
            /// 4待发货
            /// </summary>
            DaiFaHuo = 4,
            /// <summary>
            /// 5发货中
            /// </summary>
            FaHuoZhong = 5,
            /// <summary>
            /// 6.已完成
            /// </summary>
            YiWanCheng = 6,
            /// <summary>
            /// 已结算
            /// </summary>
            YiJieSuan = 7,


        }
    }
}
