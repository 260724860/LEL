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
        /// <summary>
        /// 商品属性名
        /// </summary>
        public string GoodsValueName { get; set; }
        public int CartID { get; set; }
        public int GoodsCount { get; set; }
        public decimal Price { get; set; }
    }
}
