using Quartz;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LELAdmin.QuzrtzJob
{
    public class ClearSalesVoilumesJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            
            GoodsService GoodsBLL = new GoodsService();
            //System.Threading.Thread.Sleep(5000);
            return GoodsBLL.ClearSalesVolumesAsync();
        }
    }
}