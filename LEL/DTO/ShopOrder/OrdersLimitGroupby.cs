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
    }
}
