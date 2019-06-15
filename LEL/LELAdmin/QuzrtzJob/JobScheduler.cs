using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LELAdmin.QuzrtzJob
{
    /// <summary>
    /// 调度程序
    /// </summary>
    public class JobScheduler
    {
        public async System.Threading.Tasks.Task startQuartzAsync()
        {

            //创建调度任务 
            StdSchedulerFactory ssf = new StdSchedulerFactory();
            IScheduler sched = await ssf.GetScheduler();
            JobDetailImpl jdBossReport = new JobDetailImpl("jdBossReport", typeof(ClearSalesVoilumesJob));

            ITrigger triggerBossReport = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                 //.WithCronSchedule("*/10 * * * * ?")
                 // .WithCronSchedule("0 0 1 * * *")//0 0 1 * * ?
                 // .WithCronSchedule("0/5 * * * * ?") //每隔5秒执行
                 //.WithCronSchedule("12 12 09 * * ?")//0 0 1 * * ?
                 .WithCronSchedule("0 59 23 L * ?")
                .Build(); ;

            await sched.ScheduleJob(jdBossReport, triggerBossReport);

            await sched.Start();

        }
    }
}