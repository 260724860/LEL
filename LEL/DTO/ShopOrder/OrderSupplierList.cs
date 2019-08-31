using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    /// <summary>
    /// 订单供应商统计
    /// </summary>
    public class OrderSupplierList
    {
        public int Status { get; set; }
        public decimal TotalSupplyPrice { get; set; }
        public int GoodsCount { get; set; }
        public int DeliverCount { get; set; }
        public string SupplierName { get; set; }
        public string MobilePhone { get; set; }
        
        public string OrderNo { get; set; }
        public int? SupplierID { get; set; }
    }
}
