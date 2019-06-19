using System;

namespace DTO.Goods
{
    public class SupplierGoodsPriceDto
    {
        public string SuppliersName { get; set; }
        public int SuppliersID { get; set; }
        public int GoodsID { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateTime { get; set; }
        public string GoodsName { get; set; }
        public string SuppliersPhone { get; set; }
        public int GoodsMappingID { get; set; }
    }
}
