using System;
using System.Collections.Generic;

namespace DTO.Goods
{
    public class GroodsModelDto
    {
        public int GoodsID { get; set; }
        public string GoodsName { get; set; }

        public string GoodsGroupName { get; set; }

        public int GoodsGroups_ID { get; set; }
        /// <summary>
        /// 货号
        /// </summary>
       // public string SerialNumber { get; set; }
        public string Image { get; set; }
        /// <summary>
        /// 规格单位
        /// </summary>
        public string Specifications { get; set; }
        public int IsShelves { get; set; }
        public int IsRecommend { get; set; }
        public int IsNewGoods { get; set; }
        public int IsHot { get; set; }

        public int IsSeckill { get; set; }

        public string Describe { get; set; }
        public int Sort { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal OriginalPrice { get; set; }
        /// <summary>
        /// 特价
        /// </summary>
        public decimal SpecialOffer { get; set; }

        public int SupplierID { get; set; }

        public int? PackingNumber { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        public int? SalesVolumes { get; set; }
        public int? TotalSalesVolumes { get; set; }
        /// <summary>
        /// 限购
        /// </summary>
        public int? Quota { get; set; }

        /// <summary>
        /// 建议零售价
        /// </summary>
        public decimal MSRP { get; set; }

        /// <summary>
        /// 库存量
        /// </summary>
        public int? Stock { get; set; }
        public IList<GoodsValues> GoodsValueList { get; set; }
    }
    public class GoodsListDto
    {
        public List<GroodsModelDto> GoodsModel { get; set; }
        public int PageCount { get; set; }

    }
}
