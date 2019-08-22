﻿using System;
using static DTO.Common.Enum;

namespace DTO.ShopOrder
{
    public class OrderSeachParams
    {
        /// <summary>
        /// 分页开始
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 分页行
        /// </summary>
        public int Rows { get; set; }
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        ///订单状态(0.未确认；1.已完成；2.已审核；3.待接单；4.已接单；5.已取消)
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        public string Out_Trade_No { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public int? UserID { get; set; }

        public int? SupplierID { get; set; }

        public int? AdminID { get; set; }

        /// <summary>
        /// 订单类型/ 1订货单 2 退货单 3 加急单
        /// </summary>
        public int? OrderType { get; set; }

        public int? ExpressType { get; set; }
        
        /// <summary>
        /// 订单列表排序
        /// </summary>
        public OrderListOrderByType? orderByType { get; set; }
    }
}
