using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <summary>
        /// 排序字段
        /// </summary>
        public GoodsSeachOrderByType SortKey { get; set; }
     
    }

    public enum GoodsSeachOrderByType
    {
        CreateTimeAsc = 1, CreateTimeDesc = 2, SpecialOfferAsc = 3, SpecialOfferDesc = 4, SortAsc = 5, SortDesc=6
    }
 
}
