using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    public class OrderSaveParamesExtend
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public List<OrderSaveGoodsInfoParames> GoodsInfo { get; set; }

        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderSaveParams OrderInfo { get; set; }

        public int Status { get; set; }
    }

    public class OrderSaveGoodsInfoParames
    {
        public int GoodsID { get; set; }
        public int GoodsValueID { get; set; }
        public int SupplierID { get; set; }
        public int GoodsCount { get; set; }
    }
}
