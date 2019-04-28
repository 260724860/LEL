using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    public class OrderGoodsList
    {
        public int GoodsID { get; set; }
        public int GoodsCount { get; set; }
        public List<GoodsValueList> ValueList { get; set; }
    }
    public class GoodsValueList
    {
        public int GoodsValueID { get; set; }
        public int CategoryType { get; set; }
        
    }

}
