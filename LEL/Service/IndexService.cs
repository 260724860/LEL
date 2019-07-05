using DTO.HqManager.Index;
using MySql.Data.MySqlClient;
using System;
using System.Linq;

namespace Service
{
    /// <summary>
    /// 后台管理系统 首页
    /// </summary>
    public class IndexService
    {
        /// <summary>
        /// 查询后台首页业绩统计
        /// </summary>
        /// <returns></returns>
        public SalesDTO GetSalesDTO()
        {
            using (Entities ctx = new Entities())
            {
                SalesDTO DTO = new SalesDTO();

                var NowDate = DateTime.Now;
                var SevenDate = Convert.ToDateTime(NowDate.AddDays(-6).ToShortDateString());
                var tempIq = ctx.le_orders_head.Where(s => s.CompleteTime >= SevenDate && s.Status == 1);

                var TDresult = tempIq.Select(s => new IndexDTO
                {
                    Out_Trade_No = s.OutTradeNo,
                    Money = s.RealAmount,
                    UpdateTime = s.CompleteTime,
                    CreateTime = s.CreateTime
                }).ToList();

                DTO.SevendaysSalesMoney = TDresult.Sum(a => a.Money);//近七日销售额
                var YesDateEn = Convert.ToDateTime(NowDate.ToShortDateString());
                var YesDateSt = Convert.ToDateTime(NowDate.AddDays(-1).ToShortDateString());
                DTO.YesterdaySalesMoney = TDresult.Where(a => a.UpdateTime <= YesDateEn && a.UpdateTime >= YesDateSt).Sum(b => b.Money);
                DTO.TodaySalesCount = TDresult.Where(a => a.UpdateTime >= Convert.ToDateTime(NowDate.ToShortDateString())).Count();
                DTO.TodaySalesMoney = TDresult.Where(a => a.UpdateTime >= Convert.ToDateTime(NowDate.ToShortDateString())).Sum(b => b.Money);

                return DTO;
            }
        }

        /// <summary>
        /// 查询商品总览
        /// </summary>
        /// <returns></returns>
        public GoodsStaticDTO GetGoodsStaticDTO()
        {
            using (Entities ctx = new Entities())
            {
                GoodsStaticDTO DTO = new GoodsStaticDTO();
                var list = ctx.le_goods.Select(s => s.IsShelves).ToList();

                var Shelves = list.Count(s => s == 0);
                var TheShelves = list.Count(s => s == 1);

                DTO.ShelvesCount = list.Count(s => s == 0);//下架
                DTO.TheShelvesCount = list.Count(s => s == 1);//上架
                DTO.TotalGoodsCount = DTO.ShelvesCount + DTO.TheShelvesCount;//总计

                return DTO;
            }
        }

        /// <summary>
        /// 查询选择时间段内销售订单数据
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public SalesChartDTO GetSalesChartDTO(string StartTime, string EndTime)
        {
            using (Entities ctx = new Entities())
            {
                SalesChartDTO DTO = new SalesChartDTO();

                string sql = string.Format(@"select count(loh.OrdersHeadID) OrderCount,round(sum(loh.Money),2) OrderMoney,date_format(loh.CompleteTime, '%Y-%m-%d') OrderTime from le_orders_head loh 
where loh.`Status`=1 and loh.CompleteTime between @StartTime and @EndTime
group by OrderTime");

                MySqlParameter[] parameters =
            {
                new MySqlParameter("@StartTime", MySqlDbType.VarChar),
                new MySqlParameter("@EndTime", MySqlDbType.VarChar),
            };
                parameters[0].Value = StartTime;
                parameters[1].Value = EndTime;

                var QueryList = ctx.Database.SqlQuery<SalesChartListDTO>(sql, parameters).ToList();

                if (QueryList.Count > 0)
                {
                    DTO.SalesChartList = QueryList;
                    DTO.OrderTolCount = QueryList.Sum(a => a.OrderCount);
                    DTO.OrderTolMoney = Math.Round(QueryList.Sum(a => a.OrderMoney), 2);
                }
                return DTO;
            }
        }

        /// <summary>
        /// 查询代办事项
        /// </summary>
        /// <returns></returns>
        public PendingTransDTO GetPendingTransDTO()
        {
            using (Entities ctx = new Entities())
            {
                PendingTransDTO dto = new PendingTransDTO();

                var result = ctx.le_orders_head.Select(s => new TotalOrderStatusDTO
                {
                    Orders_Head_ID = s.OrdersHeadID,
                    Status = s.Status
                }).ToList();

                if (result.Count > 0)
                {
                    dto.UnconfirmedCount = result.Where(a => a.Status == 0).ToList().Count;//未确认订单数0
                    dto.CompleteCount = result.Where(a => a.Status == 1).ToList().Count;//已完成订单数1
                    dto.ApprovedCount = result.Where(a => a.Status == 2).ToList().Count;//已审核订单数2
                    dto.WaitingListCount = result.Where(a => a.Status == 3).ToList().Count;//待接单订单数3
                    dto.HaveOrderCount = result.Where(a => a.Status == 4).ToList().Count;//已接单订单数4
                    dto.CancelledCount = result.Where(a => a.Status == 5).ToList().Count;//已取消订单数5
                }

                return dto;
            }
        }
    }
}
