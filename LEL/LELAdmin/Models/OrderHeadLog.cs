using DTO.Common;

namespace LELAdmin.Models
{
    public class OrderHeadLogParams
    {
        public SeachDateTimeOptions SeachOptions { get; set; }
        public int? AdminID { get; set; }

        public int OrderHeadID { get; set; }
    }
}