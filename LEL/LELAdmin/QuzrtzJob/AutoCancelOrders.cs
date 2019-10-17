using DTO.Suppliers;
using log4net;
using Quartz;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static DTO.Common.Enum;

namespace LELAdmin.QuzrtzJob
{
    /// <summary>
    /// 48小时内没有取货的订单在下午6点取消
    /// </summary>
    public class AutoCancelOrders: IJob
    {
        private static ILog log = LogManager.GetLogger(typeof(AutoCancelOrders));
        public async Task Execute(IJobExecutionContext context)
        {
            ShopOrderService ShopBLL = new ShopOrderService();

            GoodsService GoodsBLL = new GoodsService();
            var result =await ShopBLL.GetOrderReminderBy48Hour();

            List<UpdateOrderLineParamas> List = new List<UpdateOrderLineParamas>();
            foreach (var intem in result)
            {
                UpdateOrderLineParamas paramas = new UpdateOrderLineParamas();
                paramas.OrderLineID = intem.OrderLineID;
                paramas.SuppliersID = 291;
                paramas.OrderNo = intem.OrderNo;
                paramas.Status = (int)(OrderLineStatus.YiQuXiao);
                paramas.Notes = "取货时间超时，系统自动取消";
                List.Add(paramas);
            }
            var UpdateBool = ShopBLL.UpdateOrderLineStatus(List, out string Msg, 0, 291);
            if (!UpdateBool)
            {
                log.Error(DateTime.Now.ToString("F") + "执行错误|"+ Msg);
            }
            //System.Threading.Thread.Sleep(5000);
            log.Error("取消48小时内未取货的订单");
            //return ShopBLL.GetOrderReminderBy48Hour();//GoodsBLL.ClearSalesVolumesAsync();
        }
    }
}