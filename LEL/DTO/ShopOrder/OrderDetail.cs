using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    public class OrderDetail
    {
        public string GoodsName { get; set; }
        public int GoodsID { get; set; }
        public string GoodsImg { get; set; }
        public decimal SpecialOffer { get; set; }

        public string Notes { get; set; }
        public int Status { get; set; }
        public string SuppliersName { get; set; }
        public int GoodsCount { get; set; }
        public string DefultSupplier { get; set; }

        public string CategoryName1 { get; set; }
        public string CategoryName2 { get; set; }
        public string CategoryName3 { get; set; }
        public string CategoryName4 { get; set; }
        public string CategoryName5 { get; set; }


    }
}
