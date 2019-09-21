using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO.Common.Enum;

namespace DTO.ShopOrder
{
    public class AddBackOrderDto
    {
        public string Barcode { get; set; } = "";
        public string GoodsName { get; set; } = "";
        public string PurchasePrice { get; set; } = "";
        public string SellingPrice { get; set; } = "";
        public string Specifications { get; set; } = "";
        public int GoodsCount { get; set; } = 0;
        public string Merchant { get; set; } = "";
        public string MerchantCode { get; set; } = "";
        public string Classify { get; set; } = "";
        public string ClassifyCode { get; set; } = "";
        public int UsersID { get; set; } = 0;
        public string Flag { get; set; } = "";
        public string Remark { get; set; } = "";
        public string InStock { get; set; } = "";
        public int ID { get; set; } = 0;
       
    }
    
}
