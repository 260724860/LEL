using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    public class OrderReminderDto
    {
        public string OrderNo { get; set; }
        public DateTime? PickUpTime { get; set; }
        public int UserID { get; set; }
    }

    public class GetWJDOrderDto
    {
        public string OrderNo { get; set; }
        /// <summary>
        /// 取货时间
        /// </summary>
        public DateTime? PickUpTime { get; set; }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public int? SupplierID { get; set; }

        public DateTime? OrderCreateTime{get;set;}
        public int OrderLineID { get; set; }
    }

    public class OrderReminderBy48Hour
    {
        public string OrderNo { get; set; }
        public int OrderLineID { get; set; }
    }
}
