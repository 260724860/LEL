using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    public class OrderSaveParams
    {
        /// <summary>
        /// 登陆用户ID, 内部获取
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 地址ID 如为自提则为系统地址ID,快递则为用户地址ID
        /// </summary>
        public int AddressID { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 订单类型/ 1订货单 2 退货单 3 加急单
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 快递类型/1快递物流 2 自提
        /// </summary>
        public int ExpressType { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// 取货时间
        /// </summary>
        public DateTime? PickupTime { get; set; }
        /// <summary>
        /// 取货人
        /// </summary>
        public string PickUpMan { get; set; }
        /// <summary>
        /// 取货人联系电话
        /// </summary>
        public string PickUpPhone { get; set; }
    }
   
}
