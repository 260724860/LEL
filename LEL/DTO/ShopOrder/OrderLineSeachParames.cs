using System;

namespace DTO.ShopOrder
{
    public class OrderLineSeachParames
    {
        public int Offset { get; set; }
        public int Rows { get; set; }
        public string KeyWords { get; set; }
        public string OrderNo { get; set; }
        public int? Status { get; set; }
        public int? AdminID { get; set; }
        public int? UserID { get; set; }
        public int? SuppliersID { get; set; }
        public int? OrderType { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTinme { get; set; }
    }
}
