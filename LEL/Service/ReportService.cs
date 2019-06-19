using DTO.Common;
using DTO.Report;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DTO.Common.Enum;

namespace Service
{
    /// <summary>
    /// 报表相关
    /// </summary>
    public class ReportService
    {
        /// <summary>
        /// 商品销量报表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="GoodsID"></param>
        /// <param name="GoodsGroupsID"></param>
        /// <returns></returns>
        public List<GoodsSaleDTO> GetGoodsSalesReport(SeachDateTimeOptions options, GoodsSalesReportOrderByType orderType, int? GoodsID, int? GoodsGroupsID, int? SupplierID, int? UserID, int? AdminID)
        {
            using (Entities ctx = new Entities())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" select b.GoodsName,c.Name GoodsGroupName, b.GoodsGroupsID, b.GoodsID , sum(a.Money * a.DeliverCount) SalesAmount , sum(a.DeliverCount) SalesCount,Count(a.OrdersLinesID) Transactiontimes,sum(a.profit) profitsum");
                sb.Append(" from le_orders_lines a");
                sb.Append(" left join le_goods b on a.GoodsID = b.GoodsID");
                sb.Append(" left join le_goodsgroups c on b.GoodsGroupsID = c.ID ");
                sb.Append(" left join le_orders_head d on a.OrderHeadID=d.OrdersHeadID  ");

                if (AdminID != null)
                {
                    sb.Append(" left join  le_admin_re_users e  on a.UsersID=e.UserID ");
                }

                //if(UserID!=null)
                //{
                //    sb.Append(" inner join le_users f on a.UsersID=f.UsersID and f.UsersID=@UsersID");
                //}

                sb.Append("  where 1=1 and a.Status=2  and OrderType!=2");
                if (GoodsGroupsID != null)
                {
                    sb.Append(" and b.GoodsGroupsID = @GoodsGroupsID");
                }
                if (GoodsID != null)
                {
                    sb.Append(" and b.GoodsID = @GoodsID");
                }
                if (SupplierID != null)
                {
                    sb.Append(" and a.SuppliersID=@SupplierID");
                }
                if (UserID != null)
                {
                    sb.Append(" and a.UsersID=@UsersID");
                }

                if (AdminID != null)
                {
                    sb.Append(" and e.AdminID=@AdminID");
                }

                if (!string.IsNullOrEmpty(options.KeyWords))
                {
                    sb.Append(" and c.Name like @KeyWords or goodsName like @KeyWords ");
                }
                sb.Append(" and a.CreateTime > @BeginTime and a.CreateTime < @EndTime ");
                sb.Append(" group by b.GoodsID ");
                switch (orderType)
                {
                    case GoodsSalesReportOrderByType.ProfitSumAsc:
                        sb.Append(" order by sum(a.profit) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.ProfitSumDesc:
                        sb.Append(" order by sum(a.profit) Desc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesAmountAsc:
                        sb.Append(" order by sum(a.Money * a.DeliverCount) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesAmountDesc:
                        sb.Append(" order by sum(a.Money * a.DeliverCount) Desc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesCountAsc:
                        sb.Append(" order by sum(a.GoodsCount) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesCountDesc:
                        sb.Append(" order by sum(a.GoodsCount) Desc ");
                        break;
                    case GoodsSalesReportOrderByType.TransactiontimesAsc:
                        sb.Append(" order by Count(a.OrdersLinesID) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.TransactiontimesDesc:
                        sb.Append(" order by Count(a.OrdersLinesID) Desc ");
                        break;
                    default:
                        sb.Append(" order by Count(a.OrdersLinesID) Desc ");
                        break;
                }
                sb.Append(" limit @Offset,@Rows ");
                MySqlParameter[] parameters = {
                    new MySqlParameter("@SupplierID", MySqlDbType.Int32),
                    new MySqlParameter("@UsersID", MySqlDbType.Int32),
                    new MySqlParameter("@GoodsGroupsID", MySqlDbType.Int32),
                    new MySqlParameter("@GoodsID", MySqlDbType.Int32),
                    new MySqlParameter("@KeyWords", MySqlDbType.VarChar),
                    new MySqlParameter("@BeginTime", MySqlDbType.DateTime),
                    new MySqlParameter("@EndTime",MySqlDbType.DateTime),
                    new MySqlParameter("@Offset",MySqlDbType.Int32),
                    new MySqlParameter("@Rows",MySqlDbType.Int32),
                    new MySqlParameter("@AdminID", MySqlDbType.Int32),
                };

                parameters[0].Value = SupplierID;
                parameters[1].Value = UserID;
                parameters[2].Value = GoodsGroupsID;
                parameters[3].Value = GoodsID;
                parameters[4].Value = "%" + options.KeyWords + "%";
                parameters[5].Value = options.BeginTime;
                parameters[6].Value = options.EndTime;
                parameters[7].Value = options.Offset;
                parameters[8].Value = options.Rows;
                parameters[9].Value = AdminID;
                var result = ctx.Database.SqlQuery<GoodsSaleDTO>(sb.ToString(), parameters).ToList();
                return result;
                return null;
            }

        }

        /// <summary>
        /// 加盟店销量统计
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public List<StoreSaleDto> GetStoreSaleReport(SeachDateTimeOptions options, int? UserID, GoodsSalesReportOrderByType orderType, int? AdminID)
        {
            using (Entities ctx = new Entities())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" select sum(a.Money* a.GoodsCount ) SalesAmount ,sum(a.DeliverCount) SalesCount,Count(a.OrdersLinesID) Transactiontimes, a.usersid,b.UsersName,b.UsersNickname,b.UsersMobilePhone,sum(a.profit) profitsum");
                sb.Append(" from le_orders_lines a");
                sb.Append(" left join le_users b on  a.UsersID = b.UsersID ");
                sb.Append(" left join le_orders_head c on a.OrderHeadID=c.OrdersHeadID ");
                if (AdminID != null)
                {
                    sb.Append(" left join  le_admin_re_users e  on a.UsersID=e.UserID ");
                }
                sb.Append(" where 1=1 and a.Status=2 and c.OrderType!=2");
                if (UserID != null)
                {
                    sb.Append(" and a.UsersID=@UsersID");
                }
                if (AdminID != null)
                {
                    sb.Append(" and e.AdminID=@AdminID");
                }
                sb.Append(" and a.CreateTime > @BeginTime and a.CreateTime < @EndTime ");
                if (!string.IsNullOrEmpty(options.KeyWords))
                {
                    sb.Append(" and b.UsersName like @KeyWords or  b.UsersNickname like @KeyWords or b.UsersMobilePhone like @KeyWords");
                }
                sb.Append(" group by a.usersid");
                switch (orderType)
                {
                    case GoodsSalesReportOrderByType.ProfitSumAsc:
                        sb.Append(" order by sum(a.profit) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.ProfitSumDesc:
                        sb.Append(" order by sum(a.profit) Desc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesAmountAsc:
                        sb.Append(" order by sum(a.Money * a.DeliverCount) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesAmountDesc:
                        sb.Append(" order by sum(a.Money * a.DeliverCount) Desc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesCountAsc:
                        sb.Append(" order by sum(a.DeliverCount) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesCountDesc:
                        sb.Append(" order by sum(a.DeliverCount) Desc ");
                        break;
                    case GoodsSalesReportOrderByType.TransactiontimesAsc:
                        sb.Append(" order by Count(a.OrdersLinesID) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.TransactiontimesDesc:
                        sb.Append(" order by Count(a.OrdersLinesID) Desc ");
                        break;
                    default:
                        sb.Append(" order by Count(a.OrdersLinesID) Desc ");
                        break;
                }

                sb.Append(" limit @Offset,@Rows");

                MySqlParameter[] parameters = {
                    new MySqlParameter("@UsersID", MySqlDbType.Int32),
                    new MySqlParameter("@BeginTime", MySqlDbType.DateTime),
                    new MySqlParameter("@EndTime",MySqlDbType.DateTime),
                    new MySqlParameter("@KeyWords", MySqlDbType.VarChar),
                    new MySqlParameter("@Offset",MySqlDbType.Int32),
                    new MySqlParameter("@Rows",MySqlDbType.Int32),
                    new MySqlParameter("@AdminID",MySqlDbType.Int32),
                };

                parameters[0].Value = UserID;
                parameters[1].Value = options.BeginTime;
                parameters[2].Value = options.EndTime;
                parameters[3].Value = "%" + options.KeyWords + "%";
                parameters[4].Value = options.Offset;
                parameters[5].Value = options.Rows;
                parameters[6].Value = AdminID;
                var result = ctx.Database.SqlQuery<StoreSaleDto>(sb.ToString(), parameters).ToList();
                return result;
                return null;

            }
        }

        /// <summary>
        /// 供应商销量统计
        /// </summary>
        /// <param name="options"></param>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public List<SupplierSaleDto> GetSupplierSaleReport(SeachDateTimeOptions options, int? SupplierID, GoodsSalesReportOrderByType orderType)
        {
            using (Entities ctx = new Entities())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" select sum(a.Money* a.DeliverCount ) SalesAmount ,sum(a.DeliverCount) SalesCount,Count(a.OrdersLinesID) Transactiontimes,b.SuppliersID,");
                sb.Append(" b.ResponPeople,b.SuppliersName,b.MobilePhone,sum(a.profit) profitsum");
                sb.Append(" from le_orders_lines a ");
                sb.Append(" left join le_suppliers b on a.SuppliersID = b.SuppliersID");

                sb.Append(" left join le_orders_head c on a.OrderHeadID=c.OrdersHeadID ");
                sb.Append(" where 1 = 1 and a.Status=2 and ordertype!=2");
                if (SupplierID != null)
                {
                    sb.Append(" and a.SuppliersID=@SuppliersID");
                }
                sb.Append(" and a.CreateTime > @BeginTime and a.CreateTime < @EndTime ");
                if (!string.IsNullOrEmpty(options.KeyWords))
                {
                    sb.Append(" and b.ResponPeople like @KeyWords or  b.SuppliersName like @KeyWords or b.MobilePhone like @KeyWords");
                }
                sb.Append(" group by SuppliersID");
                switch (orderType)
                {
                    case GoodsSalesReportOrderByType.ProfitSumAsc:
                        sb.Append(" order by sum(a.profit) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.ProfitSumDesc:
                        sb.Append(" order by sum(a.profit) Desc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesAmountAsc:
                        sb.Append(" order by sum(a.Money * a.DeliverCount) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesAmountDesc:
                        sb.Append(" order by sum(a.Money * a.DeliverCount) Desc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesCountAsc:
                        sb.Append(" order by sum(a.DeliverCount) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.SalesCountDesc:
                        sb.Append(" order by sum(a.DeliverCount) Desc ");
                        break;
                    case GoodsSalesReportOrderByType.TransactiontimesAsc:
                        sb.Append(" order by Count(a.OrdersLinesID) Asc ");
                        break;
                    case GoodsSalesReportOrderByType.TransactiontimesDesc:
                        sb.Append(" order by Count(a.OrdersLinesID) Desc ");
                        break;
                    default:
                        sb.Append(" order by Count(a.OrdersLinesID) Desc ");
                        break;
                }

                // sb.Append(" order by Count(a.OrdersLinesID) desc");
                sb.Append(" limit @Offset,@Rows");
                MySqlParameter[] parameters = {
                    new MySqlParameter("@SuppliersID", MySqlDbType.Int32),
                    new MySqlParameter("@BeginTime", MySqlDbType.DateTime),
                    new MySqlParameter("@EndTime",MySqlDbType.DateTime),
                    new MySqlParameter("@KeyWords", MySqlDbType.VarChar),
                    new MySqlParameter("@Offset",MySqlDbType.Int32),
                    new MySqlParameter("@Rows",MySqlDbType.Int32),
                };
                parameters[0].Value = SupplierID;
                parameters[1].Value = options.BeginTime;
                parameters[2].Value = options.EndTime;
                parameters[3].Value = "%" + options.KeyWords + "%";
                parameters[4].Value = options.Offset;
                parameters[5].Value = options.Rows;
                var result = ctx.Database.SqlQuery<SupplierSaleDto>(sb.ToString(), parameters).ToList();
                return result;
                return null;
            }
        }
    }
}
