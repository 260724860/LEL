﻿using log4net;
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
    /// 供应商超时送货提示 1小时
    /// </summary>
    public class OrderSupplierReminder : IJob
    {
        private static ILog log = LogManager.GetLogger(typeof(ClearSalesVoilumesJob));
        public Task Execute(IJobExecutionContext context)
        {

            return new ShopOrderService().GetWDJOrderList();


            GoodsService GoodsBLL = new GoodsService();
            //System.Threading.Thread.Sleep(5000);
            log.Error("清空月销量字段");
            return GoodsBLL.ClearSalesVolumesAsync();
        }

      
    }
}