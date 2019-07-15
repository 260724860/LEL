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

        /// <summary>
        /// 时间段
        /// </summary>
        public int TimeSlot { get; set; }
        /// <summary>
        /// 限制下单数
        /// </summary>
        public int LimitOrderCount { get; set; }
       // public int CurrentOrderCount { get; set; }
       

    }
}
