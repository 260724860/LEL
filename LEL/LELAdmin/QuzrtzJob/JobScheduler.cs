using Quartz;
using Quartz.Impl;

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
            
            //清空月销量
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

            //查询订货超时
            //JobDetailImpl jdBossReport2 = new JobDetailImpl("jdBossReport2", typeof(OrderReminderJob));
            //ITrigger triggerBossReport2 = TriggerBuilder.Create()
            //    .WithIdentity("trigger12", "group12")
            //     //.WithCronSchedule("*/10 * * * * ?")
            //     // .WithCronSchedule("0 0 1 * * *")//0 0 1 * * ?
            //     // .WithCronSchedule("0/5 * * * * ?") //每隔5秒执行
            //     //.WithCronSchedule("12 12 09 * * ?")//0 0 1 * * ?
            //     .WithCronSchedule("0 * 14,18 * * ?") //每天10点钟每隔5分钟执行一次
            //    .Build(); ;
            //await sched.ScheduleJob(jdBossReport2, triggerBossReport2);

            //下午六点钟取消取货时间48小时内订单
            JobDetailImpl jdBossReport3 = new JobDetailImpl("jdBossReport3", typeof(AutoCancelOrders));
            ITrigger triggerBossReport3 = TriggerBuilder.Create()
                .WithIdentity("trigger123", "group123")
                 //.WithCronSchedule("*/10 * * * * ?")
                 // .WithCronSchedule("0 0 1 * * *")//0 0 1 * * ?
                 // .WithCronSchedule("0/5 * * * * ?") //每隔5秒执行
                 //.WithCronSchedule("12 12 09 * * ?")//0 0 1 * * ?
                 .WithCronSchedule("0 0 23 * * ?") //每天10点钟每隔5分钟执行一次
                .Build(); ;
            await sched.ScheduleJob(jdBossReport3, triggerBossReport3);

            await sched.Start();
            
            
            

        }
    }
}