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
        public async Task<List<OrdersLimitGroupby>>  GetOrderLimitForTimeSlot(DateTime TimeSlot,int? UserID=null)
        {
            using (Entities ctx=new Entities())
            {
                UserID = 64;
                var BeginTime = new DateTime(TimeSlot.Year, TimeSlot.Month, TimeSlot.Day, 0, 0, 0);
                var EndTime = new DateTime(TimeSlot.Year, TimeSlot.Month, TimeSlot.Day, 23, 59, 59);

                var groupby = ctx.le_orders_head.Where(s => s.ExpressType == 2 &&s.Status!=5 && s.OrderType != 2 && s.PickupTime >= BeginTime && s.PickupTime <= EndTime)
                    .GroupBy(k => new
                    {                      
                        k.PickupTime.Value.Hour 
                    }).Select(b => new OrdersLimitGroupby
                    {                       
                        CurrentOrderCount = b.Count(a=> a.OrdersHeadID>0),
                        TimeSlot = b.Key.Hour
                    });
                var list = groupby.ToList();
                // var CurrentCountList = list.Select(s => s.TimeSlot);
                //var LimitCountList = ctx.le_orders_timelimit.Where(s => CurrentCountList.Contains(s.TimeSlot))
                var LimitCountList = ctx.le_orders_timelimit.Where(s =>true)
                    .Select(b => new OrdersLimitGroupby
                    {
                       TimeSlot= b.TimeSlot,
                       LimitCount=  b.LimitOrderCount,
                       AdminID=b.AdminID,
                       AdminName=b.le_admin.Nickname,
                       CreateTime=b.CreateTime,
                       ID=b.ID,
                     //  LimitOrderCount=b.LimitOrderCount,
                       UpdateTime=b.UpdateTime
                    }).ToList();

                LimitCountList.ForEach(m => {
                    var exit = list.FirstOrDefault(s => s.TimeSlot == m.TimeSlot);
                    if (exit != null)
                    {
                        m.CurrentOrderCount = exit.CurrentOrderCount;
                    }
                });
                //if(UserID!=null&&UserID!=0)
                //{
                //    var Classify = ctx.le_users.Where(s => s.UsersID == UserID).Select(s => s.Classify).FirstOrDefault();
                //    var Exit291 = ctx.le_shop_cart.Any(s => s.UserID == UserID && s.le_goods.le_goods_suppliers.Any(k => k.SuppliersID == 291));//.Select(s => s.le_goods.le_goods_suppliers.Any(k => k.SuppliersID == 291));
                //    if( Exit291 && Classify=="lelshoptest")
                //    {
                //        LimitCountList = new List<OrdersLimitGroupby>();
                //    }
                //}
                var results = LimitCountList.OrderBy(s=>s.TimeSlot).ToList();
              //var results= list.Join(LimitCountList, a => a.TimeSlot, b => b.TimeSlot, (a, b) =>

                //      new OrdersLimitGroupby
                //      {   
                //          CurrentOrderCount=a.CurrentOrderCount,
                //          TimeSlot = b.TimeSlot,
                //          LimitCount = b.LimitCount,

                //      }).ToList();




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
                if(dto.TimeSlot<0||dto.TimeSlot>23)
                {
                    Msg = "时间段设置错误，请选择0点-23点之内时间段";
                    return false;
                }
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
                        model.LimitOrderCount = dto.LimitCount;
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
                    model.LimitOrderCount = dto.LimitCount;
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
