using log4net;
using Quartz;
using Service;
using System.Threading.Tasks;

namespace LELAdmin.QuzrtzJob
{
    public class ClearSalesVoilumesJob : IJob
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