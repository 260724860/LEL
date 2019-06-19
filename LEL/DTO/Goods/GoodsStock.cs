using System;

namespace DTO.Goods
{
    public class GoodsStock
    {
        public int GoodsID { get; set; }
        public int? Stock { get; set; }
        public DateTime RowVersion { get; set; }
        public int GoodsCount { get; set; }
    }
}
