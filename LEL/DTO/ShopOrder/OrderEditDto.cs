using System;

namespace DTO.ShopOrder
{
    /// <summary>
    /// 
    /// </summary>
    public class EditReceiptInfo
    {
        public string Out_Trade_No { get; set; }

        public string RcAddr { get; set; }

        public string RcName { get; set; }

        public string RcPhone { get; set; }

    }

    public class EditLinesInfo
    {

        public int Orders_Lines_ID { get; set; }
        public int SuppliersID { get; set; }
        public int GoodsCount { get; set; }
        public int UserType { get; set; }
        public int UserID { get; set; }
        public string Notes { get; set; }
        public int OrderHeadID { get; set; }
    }

    public class OrderEditInfo
    {
        public string OrderNo { get; set; }
        public int? Status { get; set; }
        public string Notes { get; set; }
        public int OrderType { get; set; }
        /// <summary>
        /// 提货时间
        /// </summary>
        public DateTime? PickupTime { get; set; }

        /// <summary>
        /// 取货人
        /// </summary>
        public string PickUpMan { get; set; }
        /// <summary>
        /// 取货电话
        /// </summary>
        public string PickUpPhone { get; set; }
        /// <summary>
        /// 取货车牌号
        /// </summary>
        public string CarNumber { get; set; }
    }

}
