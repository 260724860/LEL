using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    public class OrderSupplierListParams
    {
        public string OrderNo { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Offset { get; set; }
        public int Rows { get; set; }
        public DateTime? BeginPickUpTime { get; set; }

        public DateTime? EndPickUpTime { get; set; }

        public string KeyWords { get; set; }
    }
}
