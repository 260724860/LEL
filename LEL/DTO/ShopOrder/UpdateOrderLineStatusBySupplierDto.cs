using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
   public class UpdateOrderLineStatusBySupplierDto
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public  string OrderNO { get; set; }
        /// <summary>
        /// 供应商id列表
        /// </summary>
        public List<int> SuppliersID { get; set; }
        /// <summary>
        /// 订单行状态限定范围 {100已结算,2已发货，3已取消 }
        /// </summary>
        public int Status { get; set; }
    }
}
