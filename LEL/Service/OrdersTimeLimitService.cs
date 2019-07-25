using DTO.ShopOrder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrdersTimeLimitService
    {

        /// <summary>
        /// 获取当前时间段内下单数
        /// </summary>
        /// <param name="TimeSlot"></param>
        /// <returns></returns>
        public async Task<List<OrdersLimitGroupby>>  GetOrderLimitForTimeSlot(DateTime TimeSlot)
        {
            using (Entities ctx=new Entities())
            {
                int hour = TimeSlot.Hour;
                //   DateTime EndTime = TimeSlot.AddHours(1);
              //  OrdersLimitCount result = new OrdersLimitCount();

                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month;
                var day = DateTime.Now.Day;
                var BeginTime = new DateTime(TimeSlot.Year, TimeSlot.Month, TimeSlot.Day, 0, 0, 0);
                var EndTime = new DateTime(TimeSlot.Year, TimeSlot.Month, TimeSlot.Day, 23, 59, 59);

                var groupby = ctx.le_orders_head.Where(s => s.ExpressType == 2 && s.OrderType != 2 && s.PickupTime >= BeginTime && s.PickupTime <= EndTime)
                    .GroupBy(k => new
                    {                      
                        k.PickupTime.Value.Hour 
                    }).Select(b => new OrdersLimitGroupby
                    {                       
                        CurrentOrderCount = b.Count(a=> a.OrdersHeadID>0),
                        TimeSlot = b.Key.Hour
                    });
                var list = groupby.ToList();
                var CurrentCountList = list.Select(s => s.TimeSlot);
                var LimitCountList = ctx.le_orders_timelimit.Where(s => CurrentCountList.Contains(s.TimeSlot))
                    .Select(b => new OrdersLimitGroupby
                    {
                       TimeSlot= b.TimeSlot,
                       LimitCount=  b.LimitOrderCount
                    }).ToList();

              var results= list.Join(LimitCountList, a => a.TimeSlot, b => b.TimeSlot, (a, b) =>
                
                    new OrdersLimitGroupby
                    {   
                        CurrentOrderCount=a.CurrentOrderCount,
                        TimeSlot = b.TimeSlot,
                        LimitCount = b.LimitCount
                    }).ToList();
            



                return  results;
            }
        }

        public List<OrdersTimeLimitDto> GetOrdersTimeLimitList()
        {
            using (Entities ctx=new Entities())
            {
                var temp = ctx.le_orders_timelimit.Where(s => true).Select(s=>new OrdersTimeLimitDto {
                    AdminID=s.AdminID,
                    TimeSlot=s.TimeSlot,
                    CreateTime=s.CreateTime,
                   // CurrentOrderCount=s.CurrentOrderCount,
                    LimitOrderCount=s.LimitOrderCount,
                    UpdateTime=s.UpdateTime,
                    ID=s.ID,
                    AdminName=s.le_admin.Nickname

                }).ToList();
                return temp;
            }
        }

        public bool CreateOrUpdate(int AdminID,OrdersTimeLimitEditDto dto,out string Msg)
        {
            using (Entities ctx = new Entities())
            {
                if (dto.ID == 0 || dto.ID == null)
                {
                    var temp = ctx.le_orders_timelimit.Any(s => s.TimeSlot == dto.TimeSlot);
                    if(temp)
                    {
                        Msg = "已存在相同得时间段设置";
                        return false;
                    }
                    else
                    {
                        le_orders_timelimit model = new le_orders_timelimit();
                        model.LimitOrderCount = dto.LimitOrderCount;
                        model.TimeSlot = dto.TimeSlot;
                        model.CreateTime = DateTime.Now;
                        model.UpdateTime = DateTime.Now;
                        model.AdminID = AdminID;
                        model.CurrentOrderCount = 0;
                        ctx.le_orders_timelimit.Add(model);
                    }
                }
                else
                {
                    var model = ctx.le_orders_timelimit.Where(s => s.ID == dto.ID).FirstOrDefault();
                    if(model==null)
                    {
                        Msg = string.Format("ID：{0}错误,请检查",dto.ID);
                        return false;
                    }
                    var temp = ctx.le_orders_timelimit.Any(s => s.TimeSlot == dto.TimeSlot&&s.ID!=dto.ID);
                    if (temp)
                    {
                        Msg = "已存在相同得时间段设置";
                        return false;
                    }
                    model.UpdateTime = DateTime.Now;
                    model.TimeSlot = dto.TimeSlot;
                    model.LimitOrderCount = dto.LimitOrderCount;
                    model.AdminID = AdminID;
                    ctx.Entry<le_orders_timelimit>(model).State = System.Data.Entity.EntityState.Modified;

                }
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        Msg = "SUCCESS";
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    
                    Msg = ex.Message;
                    return false;
                }
                Msg = "未知错误";
                return false;
            }
        }

        /// <summary>
        /// 获取当前下单时间限制
        /// </summary>
        /// <param name="TimeSlot"></param>
        /// <returns></returns>
        public int GetTimeLimitCount(int TimeSlot)
        {
            using (Entities ctx = new Entities())
            {
                var temp = ctx.le_orders_timelimit.Where(s => s.TimeSlot == TimeSlot).Select(s=>s.LimitOrderCount).FirstOrDefault();
                return temp;
            }
        }
    }
}
