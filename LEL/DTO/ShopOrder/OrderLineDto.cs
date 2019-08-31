
using DTO.Goods;
using System;
using System.Collections.Generic;

namespace DTO.ShopOrder
{
    /// <summary>
    /// 订单行Dto
    /// </summary>
    public class OrderLineDto
    {
        public string OrderNo { get; set; }
        public int OrderLineID { get; set; }
        public int OrderHeadID { get; set; }
        public int Goods_ID { get; set; }
        public decimal Money { get; set; }
        public int GoodsCount { get; set; }
        public int DeliverCount { get; set; }

        public string Notes { get; set; }
        public int? Status1 { get; set; }
        public int? Status2 { get; set; }
        public int? Status3 { get; set; }
        public int? AdminID { get; set; }
        public int UsersID { get; set; }
        
        public string UsersName { get; set; }
        public int? SuppliersID { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public System.DateTime CreateTime { get; set; }

        public decimal SupplyMoney { get; set; }

        public string GoodsName { get; set; }
        public string GoodsImage { get; set; }

        public int OrderType { get; set; }
        #region 扩展属性
        //public string GoodsName { get; set; }
        //public string GoodsImage { get; set; }
        public string AdminName { get; set; }

        public string SuppliersName { get; set; }
       
        public string StatusName { get; set; }
        public List<GoodsValues> GoodsValuesList { get; set; }

        public string RcName { get; set; }
        public string RcPhone { get; set; }
        public string Out_Trade_No { get; set; }

        public DateTime? PickupTime { get; set; }

        public string AdminTelPhone { get; set; }

        /// 订单状态(0：未派单 1：待接单 6：待发货 7：发货中 2：已发货 10：已完成 100：已结算 3：已取消)
        /// <summary>
        /// 未派发
        /// </summary>
        public int WeiPaiFaCount { get; set; }
        /// <summary>
        /// 待接单
        /// </summary>
        public int DaiJieDanCount { get; set; }
        /// <summary>
        /// 待发货
        /// </summary>
        public int DaiFaHuoCount { get; set; }
        /// <summary>
        /// 发货中
        /// </summary>
        public int FaHuoZhongCount { get; set; }
        /// <summary>
        /// 已发货
        /// </summary>
        public int YiFahuoCount { get; set; }
        /// <summary>
        /// 已完成
        /// </summary>
        public int YiWanChengCount { get; set; }
        /// <summary>
        /// 已结算
        /// </summary>
        public int YiJieSuanCount { get; set; }
        /// <summary>
        /// 已取消
        /// </summary>
        public int YiQuXiaoCount { get; set; }

        /// <summary>
        /// 订单行
        /// </summary>
        public int OrderLineCount { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// 取货地址
        /// </summary>
        public string RcAddr { get; set; }

        /// <summary>
        /// 物流类型
        /// </summary>
        public int ExpressType { get; set; }
        #endregion
    }
}
