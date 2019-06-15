using DTO.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    public class ShopCartDto
    {
        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品图
        /// </summary>
        public string GoodsImg { get; set; }

        public int GoodsID { get; set; }
       
        public int CartID { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int GoodsCount { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public int GoodsGroups_ID { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
       // public int SuppliersID { get; set; }

        /// <summary>
        /// 供应商价格
        /// </summary>
       // public decimal Supplyprice { get; set; }

        public IEnumerable<GoodsValues> GoodsValueList { get; set; }
        /// <summary>
        /// 限购
        /// </summary>
        public int? Quota { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int? Stock { get; set; }

        public decimal SpecialOffer { get; set; }
        public DateTime RowVersion { get; set; }

        public List<SupplierGoods> SupplierGoodsList { get; set; }

        public int MinimumPurchase { get; set; }
    }
   
}
