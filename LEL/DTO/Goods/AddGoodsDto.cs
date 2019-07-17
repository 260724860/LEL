using System;
using System.Collections.Generic;

namespace DTO.Goods
{
    public class AddGoodsDto
    {
        public int GoodsID { get; set; }
        public int GoodsGroups_ID { get; set; }

        // public string SerialNumber { get; set; }
        public string GoodsName { get; set; }

        public int IsSeckill { get; set; }
        public int Stock { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Specifications { get; set; }

        public int IsShelves { get; set; }
        public int IsRecommend { get; set; }
        public int IsNewGoods { get; set; }
        public int IsHot { get; set; }
        public string Describe { get; set; }
        public int Sort { get; set; }
        public int SupplierID { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal OriginalPrice { get; set; }
        /// <summary>
        /// 特价
        /// </summary>
        public decimal SpecialOffer { get; set; }

        public List<string> GoodsImgList { get; set; }

        public List<GoodsValues> GoodsValueList { get; set; }

        public List<AddSupplierDto> GoodsSuplierPriceList { get; set; }

        public string HeadImage { get; set; } = null;
        public string ShelfLife { get; set; }
        /// <summary>
        /// 取货车牌号
        /// </summary>
        public int? PackingNumber { get; set; }
        /// <summary>
        /// 限购
        /// </summary>
        public int? Quota { get; set; }

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
        /// 满减（满）金额
        /// </summary>
        public decimal PriceFull { get; set; }
        /// <summary>
        /// 满减（减）金额
        /// </summary>
        public decimal PriceReduction { get; set; }

        /// <summary>
        /// 满减（满）数量
        /// </summary>
        public decimal CountFull {get;set;}
        /// <summary>
        /// 满减（减）数量
        /// </summary>
        public decimal CountReduction { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public string TermOfValidity { get; set; }
    }

    public class GoodsImage
    {

        public int ID { get; set; }
        public string Src { get; set; }

        public DateTime CreatTime { get; set; }
        public int GoodsID { get; set; }

        #region 扩展属性

        public string Base64Src { get; set; }

        public Byte[] ImageFlow { get; set; } = null;//文件流 

        #endregion
    }

    public class AddGoodsAttachImg1to
    {
        public int GoodsID { get; set; }
        public int ImgID { get; set; }
        public string Src { get; set; }
    }
    public class AddSupplierDto
    {
        public int SupplierID { get; set; }
        public decimal Price { get; set; }
        public int IsDefalut { get; set; }
    }

    //public class GoodsValueMapping
    //{
    //    public int ID { get; set; }

    //    public int GoodsID { get; set; }

    //    /// <summary>
    //    /// 1（例：口味）2（例：颜色）3（例：尺寸）4未定 5 未定
    //    /// </summary>
    //    public int CategoryType { get; set; }



    //    #region 扩展属性
    //    public List<GoodsValue> GoodsValueList { get; set; }

    //    #endregion
    //}

    //public class GoodsValue {
    //    public int ID { get; set; }

    //    public int CoodsValueMappingID { get; set; }
    //    public string GoodsValueName { get; set; }

    //    /// <summary>
    //    /// 商品条码
    //    /// </summary>
    //    public string SerialNumber { get; set; }
    //    #region 扩展字段

    //    /// <summary>
    //    /// 1（例：口味）2（例：颜色）3（例：尺寸）4未定 5 未定
    //    /// </summary>
    //    public int CategoryType { get; set; }

    //    public int GoodsID { get; set; }

    //    #endregion
    //}
}
