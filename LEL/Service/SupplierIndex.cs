using DTO.HqManager.Index;
using MySql.Data.MySqlClient;
using System;
using System.Linq;

namespace Service
{
    public class SupplierIndex
    {
        /// <summary>
        /// 查询后台首页业绩统计
        /// </summary>
        /// <returns></returns>
        public DTO.HqManager.Index.SalesDTO GetSalesDTO(int SupplierID)
        {
            using (Entities ctx = new Entities())
            {
                SalesDTO DTO = new SalesDTO();

                var NowDate = DateTime.Now;
                var SevenDate = Convert.ToDateTime(NowDate.AddDays(-6).ToShortDateString());
                var tempIq = ctx.le_orders_lines.Where(s => s.UpdateTime >= SevenDate && s.SuppliersID == SupplierID && s.le_orders_head.Status == 1);

                var TDresult = tempIq.Select(s => new IndexDTO
                {
                    // Out_Trade_No = s.Out_Trade_No,
                    Money = s.GoodsPrice,
                    UpdateTime = s.UpdateTime,
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
        public GoodsStaticDTO GetGoodsStaticDTO(int SupplierID)
        {
            using (Entities ctx = new Entities())
            {
                GoodsStaticDTO DTO = new GoodsStaticDTO();
                var list = ctx.le_goods_suppliers.Where(s => s.SuppliersID == SupplierID).Select(k => k.le_goods.IsShelves).ToList();

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
        public SalesChartDTO GetSalesChartDTO(string StartTime, string EndTime, int SupplierID)
        {
            using (Entities ctx = new Entities())
            {
                SalesChartDTO DTO = new SalesChartDTO();

                string sql = string.Format(@" select count(a.OrdersLinesID) OrderCount,sum(a.GoodsCount*a.money) OrderMoney, date_format(a.UpdateTime, '%Y-%m-%d') OrderTime 
 from le_orders_lines a  left join le_orders_head b on a.OrderHeadID=b.OrdersHeadID
where a.UpdateTime>@StartTime and a.UpdateTime<@EndTime 
and a.status=2 and b.status=1 and suppliersid=@SupplierID
group by OrderTime");

                MySqlParameter[] parameters =
                 {
                new MySqlParameter("@StartTime", MySqlDbType.DateTime),
                new MySqlParameter("@EndTime", MySqlDbType.DateTime),
                new MySqlParameter("@SupplierID", MySqlDbType.Int32),
                  };
                parameters[0].Value = StartTime;
                parameters[1].Value = EndTime;
                parameters[2].Value = SupplierID;
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
        public PendingTransDTO GetPendingTransDTO(int SupplierID)
        {
            using (Entities ctx = new Entities())
            {
                PendingTransDTO dto = new PendingTransDTO();

                var result = ctx.le_orders_lines.Where(s => s.SuppliersID == SupplierID).Select(s => new TotalOrderStatusDTO
                {
                    Orders_Head_ID = s.OrderHeadID,
                    Status = s.Status
                }).ToList();

                var goupby = result.GroupBy(s => s.Orders_Head_ID).Select(k => new TotalOrderStatusDTO
                {
                    Orders_Head_ID = k.Key,
                    Status = k.Max(p => p.Status)
                }).ToList();
                if (goupby.Count > 0)
                {
                    //dto.UnconfirmedCount = result.Where(a => a.Status == 0).ToList().Count;//未确认订单数0
                    dto.CompleteCount = goupby.Where(a => a.Status == 2).ToList().Count;//已完成订单数1
                                                                                        // dto.ApprovedCount = result.Where(a => a.Status == 2).ToList().Count;//已审核订单数2
                    dto.WaitingListCount = goupby.Where(a => a.Status == 1).ToList().Count;//待接单订单数3
                    dto.HaveOrderCount = goupby.Where(a => a.Status == 3).ToList().Count;//已接单订单数4
                    //dto.CancelledCount = result.Where(a => a.Status == 5).ToList().Count;//已取消订单数5
                }

                return dto;
            }
        }
    }
}
