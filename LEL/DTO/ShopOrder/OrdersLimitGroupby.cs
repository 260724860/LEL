using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    public class OrdersLimitGroupby
    {
        public string key { get; set; }
        public int TimeSlot { get; set; }

        public int CurrentOrderCount { get; set; }

        public int LimitCount { get; set; }

        public int ID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int AdminID { get; set; }
        public System.DateTime UpdateTime { get; set; }
      
        //public int LimitOrderCount { get; set; }

        public string AdminName { get; set; }
    }
}
