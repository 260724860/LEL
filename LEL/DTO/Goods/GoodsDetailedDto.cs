using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Goods
{
    public class GoodsDetailedDto
    {
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
    }
    public class GoodsValues
    {
        public string GoodsValueName { get; set; }
        public int CategoryType { get; set; }
        public decimal Price { get; set; }
        public int GoodsValueID { get; set; }
    }
    public class GoodsImg
    {
        public string Src { get; set; }
        public int ID { get; set; }
    }
}
