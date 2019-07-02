namespace DTO.Goods
{
    /// <summary>
    /// 商品查询参数
    /// </summary>
    public class GoodsSeachOptions
    {
        /// <summary>
        /// 搜索名
        /// </summary>
        public string KeyWords { get; set; }
        public int Offset { get; set; }
        public int Rows { get; set; }

        public int? IsShelves { get; set; }
        public int? IsRecommend { get; set; }
        public int? IsNewGoods { get; set; }
        public int? IsHot { get; set; }
        public int? GoodsGroupID { get; set; }
        public int? IsSeckill { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public GoodsSeachOrderByType SortKey { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public int? SupplierID { get; set; }

        public string SerialNumber { get; set; }
    }

    public enum GoodsSeachOrderByType
    {
        CreateTimeAsc = 1,
        CreateTimeDesc = 2,
        OriginalPriceAsc = 3,
        OriginalPriceDesc = 4,
        SortAsc = 5,
        SortDesc = 6,
        SalesVolumesASC = 7,
        SalesVolumesDesc = 8,
        TotalSalesVolumesASC = 9,
        TotalSalesVolumesDESC = 10,
        GoodsIDAsc=11,
        GoodsIDDesc=12,
    }

}
