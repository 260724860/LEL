using Quartz;
using Service;
using System.Threading.Tasks;

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