using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Goods
{
    public class AddGoodsDto
    {       
        public int GoodsGroups_ID { get; set; }
        public string SerialNumber { get; set; }
        public string GoodsName { get; set; }
        public string Image { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Specifications { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Category5 { get; set; }
        public string Category4 { get; set; }
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

        public List<GoodsImage> GoodsImageList { get; set; }
    }

    public class GoodsImage {
        public int ID { get; set; }
        public string Src { get; set; }
        public DateTime CreatTime { get; set; }
        public int GoodsID { get; set; }

        #region 扩展属性

        public Byte[] ImageFlow { get; set; }//文件流 

        #endregion
    }
}
