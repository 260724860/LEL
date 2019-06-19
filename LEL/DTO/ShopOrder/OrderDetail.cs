using DTO.Goods;
using System.Collections.Generic;

namespace DTO.ShopOrder
{
    public class OrderDetail
    {
        public string GoodsName { get; set; }
        public int GoodsID { get; set; }
        public string GoodsImg { get; set; }
        public decimal SpecialOffer { get; set; }

        public string Notes { get; set; }
        public int Status { get; set; }
        public string SuppliersName { get; set; }
        public int GoodsCount { get; set; }
        public string DefultSupplier { get; set; }
        public int DefultSupplierID { get; set; }

        public int Orders_Lines_ID { get; set; }
        public string SupplierPhone { get; set; }
        public int SupplierID { get; set; }

        /// <summary>
        /// 供应商价格
        /// </summary>
        public decimal SupplyPrice { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Money { get; set; }

        public decimal OriginalPrice { get; set; }

        public string SerialNumber { get; set; }

        public List<GoodsValues> GoodsValuesList { get; set; }

        /// <summary>
        /// 规格单位
        /// </summary>
        public string Specifications { get; set; }

        /// <summary>
        /// 装箱数
        /// </summary>
        public int? PackingNumber { get; set; }

        /// <summary>
        /// 实际发货数
        /// </summary>
        public int DeliverCount { get; set; }

        /// <summary>
        /// 最小起配数
        /// </summary>
        public int MinimumPurchase { get; set; } 
    }
}
