using System;
using System.Collections.Generic;

namespace DTO.HqManager.Index
{
    /// <summary>
    /// 总部后台首页 DTO
    /// </summary>
    public class IndexDTO
    {
        /// <summary>
        /// 订单外部单据号
        /// </summary>
        public string Out_Trade_No { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 订单支付时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    public class SalesDTO
    {
        /// <summary>
        /// 今日订单数
        /// </summary>
        public int TodaySalesCount { get; set; }

        /// <summary>
        /// 今日销售额
        /// </summary>
        public decimal TodaySalesMoney { get; set; }

        /// <summary>
        /// 昨日销售额
        /// </summary>
        public decimal YesterdaySalesMoney { get; set; }

        /// <summary>
        /// 近七日销售额
        /// </summary>
        public decimal SevendaysSalesMoney { get; set; }

    }

    public class PendingTransDTO
    {

        /// <summary>
        /// 待付款订单数
        /// </summary>
        public int UnconfirmedCount { get; set; }

        /// <summary>
        /// 已完成订单数
        /// </summary>
        public int CompleteCount { get; set; }

        /// <summary>
        /// 待确认收货订单数
        /// </summary>
        public int ApprovedCount { get; set; }

        /// <summary>
        /// 新缺货登记
        /// </summary>
        public int WaitingListCount { get; set; }

        /// <summary>
        /// 带退款订单数
        /// </summary>
        public int HaveOrderCount { get; set; }

        /// <summary>
        /// 已发货订单数
        /// </summary>
        public int CancelledCount { get; set; }

    }

    public class GoodsStaticDTO
    {
        /// <summary>
        /// 已上架商品数
        /// </summary>
        public int ShelvesCount { get; set; }

        /// <summary>
        /// 已下架商品数
        /// </summary>
        public int TheShelvesCount { get; set; }

        /// <summary>
        /// 总商品数
        /// </summary>
        public int TotalGoodsCount { get; set; }
    }

    public class SalesChartDTO
    {
        /// <summary>
        /// 订单总数
        /// </summary>
        public int OrderTolCount { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal OrderTolMoney { get; set; }

        public List<SalesChartListDTO> SalesChartList { get; set; }
    }

    public class SalesChartListDTO
    {
        /// <summary>
        /// 订单支付时间
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// 订单数
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderMoney { get; set; }
    }

    public class TotalOrderStatusDTO
    {
        public int Orders_Head_ID { get; set; }

        public int Status { get; set; }
    }
}
