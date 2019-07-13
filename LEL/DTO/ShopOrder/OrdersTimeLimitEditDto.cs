using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    public class OrdersTimeLimitEditDto
    {
        public int? ID { get; set; }

        public int TimeSlot { get; set; }
        public int LimitOrderCount { get; set; }
       // public int CurrentOrderCount { get; set; }
       

    }
}
