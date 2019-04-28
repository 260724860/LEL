using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Goods
{
    public class GroodsModelDto
    {
        public int GoodsID { get; set; }
        public string GoodsName { get; set; }

        public int GoodsGroups_ID { get; set; }
        /// <summary>
        /// 货号
        /// </summary>
        public string SerialNumber { get; set; }
        public string Image { get; set; }
        /// <summary>
        /// 规格单位
        /// </summary>
        public string Specifications { get; set; }
        public int IsShelves { get; set; }
        public int IsRecommend { get; set; }
        public int IsNewGoods { get; set; }
        public int IsHot { get; set; }
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
    }
    public class GroodsListDto
    {
        public List<GroodsModelDto> GroodsModel { get; set; }
        public int PageCount { get; set; }
    }
}
