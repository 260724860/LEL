using System;

namespace DTO.ShopOrder
{
    /// <summary>
    /// 订单打包数
    /// </summary>
    public class OrderPackCount
    {
        /// <summary>
        /// 单号
        /// </summary>
       // public string Out_Trade_No { get; set; }
        public int PackCount { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Suppliers_Name { get; set; }
        public int SuppliersID { get; set; }
        public int OrderHeadID { get; set; }
    }
}
