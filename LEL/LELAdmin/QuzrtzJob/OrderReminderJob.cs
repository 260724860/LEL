using log4net;
using Quartz;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LELAdmin.QuzrtzJob
{
    /// <summary>
    /// 订单超时提醒
    /// </summary>
    public class OrderReminderJob:IJob
    {
        private static ILog log = LogManager.GetLogger(typeof(ClearSalesVoilumesJob));
        public Task Execute(IJobExecutionContext context)
        {

            GoodsService GoodsBLL = new GoodsService();
            //System.Threading.Thread.Sleep(5000);
            log.Error("清空月销量字段");
            return GoodsBLL.ClearSalesVolumesAsync();
        }
    }
}