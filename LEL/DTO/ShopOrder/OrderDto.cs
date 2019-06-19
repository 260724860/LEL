using System;

namespace DTO.ShopOrder
{
    public class OrderDto
    {
        public int Orders_Head_ID { get; set; }
        public string Out_Trade_No { get; set; }
        public string RcAddr { get; set; }
        public string RcName { get; set; }
        public string RcPhone { get; set; }
        public decimal Money { get; set; }
        public int Status { get; set; }
        public string Head_Notes { get; set; }
        public DateTime? UpdateTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int? AdminID { get; set; }
        public int UsersID { get; set; }
        public string UserName { get; set; }
        public DateTime? CompleteTime { get; set; }
        public int LinesCount { get; set; }
        public int ExpressType { get; set; }
        public int OrderType { get; set; }
        public int SupplierID { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// 取货时间
        /// </summary>
        public DateTime? PickupTime { get; set; }
        /// <summary>
        /// 取货人
        /// </summary>
        public string PickUpMan { get; set; }
        /// <summary>
        /// 取货人联系电话
        /// </summary>
        public string PickUpPhone { get; set; }

        /// <summary>
        /// 供应商价格
        /// </summary>
        public decimal SupplyMoney { get; set; }

        public string AdminName { get; set; }

        public int AdminTelPhone { get; set; }
        /// <summary>
        /// 实际发货数
        /// </summary>
        public int DeliverCount { get; set; }
    }
}
