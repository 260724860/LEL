using System;
using System.Collections.Generic;

namespace DTO.Goods
{
    public class GoodsDetailedDto
    {
        /// <summary>
        /// Sort
        /// </summary>
        public int IsHot { get; set; }
        public int IsNewGoods { get; set; }
        public int IsRecommend { get; set; }
        public int IsShelves { get; set; }
        public int Sort { get; set; }
        public string Specifications { get; set; }
        public List<SupplierGoods> SupplierGoodsList { get; set; }
        public string GoodsGroupsName { get; set; }

        public int GoodsID { get; set; }
        public string Image { get; set; }

        public string Describe { get; set; }
        public int GoodsGroups_ID { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 货号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        public List<GoodsImg> ImgList { get; set; }

        /// <summary>
        /// 属性列表
        /// </summary>
        public List<GoodsValues> ValuesList { get; set; }

        public string ShelfLife { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal SpecialOffer { get; set; }

        public int IsSeckill { get; set; }
        public int? Quota { get; set; }
        public int? Stock { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public int? SalesVolumes { get; set; }

        public int? TotalSalesVolumes { get; set; }
        /// <summary>
        /// 装箱数
        /// </summary>
        public int? PackingNumber { get; set; }

        /// <summary>
        /// 是否散货 0否 1 是
        /// </summary>
        public int IsBulkCargo { get; set; } = 0;

        /// <summary>
        /// 建议零售价
        /// </summary>
        public decimal MSRP { get; set; } = 0;

        /// <summary>
        /// 是否送货上门
        /// </summary>
        public int IsDeliverHome { get; set; } = 0;

        /// <summary>
        /// 最小采购量
        /// </summary>
        public int MinimumPurchase { get; set; } = 1;


        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }
        /// <summary>
        /// 是否可以跨区进货
        /// </summary>
        public int IsCrossdomain { get; set; }
        /// <summary>
        /// 是否可以退货
        /// </summary>
        public int IsReturn { get; set; }
        /// <summary>
        /// 急单码
        /// </summary>
        public string UrgentOrder { get; set; }
        /// <summary>
        /// 秒杀开始时间
        /// </summary>
        public DateTime? SeckillBeginTime { get; set; }
        /// <summary>
        /// 秒杀结束时间
        /// </summary>
        public DateTime? SeckillEndTime { get; set; }

        /// <summary>
        /// 商品首字母
        /// </summary>
        public string Initial { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string PlaceofOrigin { get; set; } = "未知";
        /// <summary>
        /// 生产
        /// </summary>
        public DateTime? ProductionDate { get; set; }
        /// <summary>
        /// 虚拟数
        /// </summary>
        public int VirtualNumber { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 商品类型（1有条码2无条码3散货）
        /// </summary>
        public int GoodsType { get; set; }
        /// <summary>
        /// 商品品牌
        /// </summary>
        public int? GoodsBarand { get; set; }
        /// <summary>
        /// 满减（满）
        /// </summary>
        public decimal PriceFull { get; set; }
        /// <summary>
        /// 满减（减）
        /// </summary>
        public decimal PriceReduction { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public string TermOfValidity { get; set; }

       public decimal CountFull { get; set; }
                  
       public decimal CountReduction { get; set; }
    }
    public class GoodsValues
    {
        //public int CoodsValueMappingID { get; set; }
        public string GoodsValueName { get; set; }
        public int CategoryType { get; set; }

        public string SerialNumber { get; set; }
        //public string CategoryTypeName { get; set; }
        public int GoodsValueID { get; set; }
        public int GoodsID { get; set; }
    }

    public class AddGoodsValues
    {
        public int GoodsValueID { get; set; }
        public int CategoryType { get; set; }
    }
    public class GoodsImg
    {
        public string Src { get; set; }
        public int ID { get; set; }
    }
    public class SupplierGoods
    {
        public string SupplierName { get; set; }
        public int SupplierID { get; set; }
        public int IsDefalut { get; set; }
        public decimal Price { get; set; }
        public int GoodsMappingID { get; set; }
    }
}
