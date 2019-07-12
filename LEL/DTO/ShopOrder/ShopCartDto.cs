﻿using DTO.Goods;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// 最小起配数
        /// </summary>
        public int MinimumPurchase { get; set; }

        /// <summary>
        /// 装箱数
        /// </summary>
        public int? PackingNumber { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 满减（满）
        /// </summary>
        public decimal PriceFull { get; set; }
        /// <summary>
        /// 满减（减）
        /// </summary>
        public decimal PriceReduction { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }
        
        /// <summary>
        /// 退货数量
        /// </summary>
        public int? ReturnCount { get; set; }
    }

}
