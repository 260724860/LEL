using DTO.Common;

namespace LELAdmin.Models
{
    public class GoodsLogParam
    {
        public SeachDateTimeOptions SeachOptions { get; set; }
        public int? AdminID { get; set; }

        public int? GoodsID { get; set; }
    }
}