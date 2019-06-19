using DTO.ShopOrder;
using DTO.Suppliers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service
{
    /// <summary>
    /// 后台订单管理
    /// </summary>
    public class AdminOrderService
    {
        private OperationRecordService ORservice = new OperationRecordService();
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="seachParams"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<OrderDto> GetListOrder(OrderSeachParams seachParams, out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_orders_head.Where(s => s.CreateTime >= seachParams.BeginTime && s.CreateTime <= seachParams.EndTime);
                if (seachParams.Status != null)
                {
                    tempIq = tempIq.Where(s => s.Status == seachParams.Status);
                }
                if (!string.IsNullOrEmpty(seachParams.KeyWords))
                {
                    tempIq = tempIq.Where(s => s.RcName.Contains(seachParams.KeyWords)
                      || s.RcPhone.Contains(seachParams.KeyWords)
                      || s.Head_Notes.Contains(seachParams.KeyWords)
                      || s.RcAddr.Contains(seachParams.KeyWords)
                      || s.le_users.UsersNickname.Contains(seachParams.KeyWords)
                    );
                }
                if (!string.IsNullOrEmpty(seachParams.Out_Trade_No))
                {
                    tempIq = tempIq.Where(s => s.OutTradeNo == seachParams.Out_Trade_No);
                }
                if (seachParams.Status != null)
                {
                    tempIq = tempIq.Where(s => s.Status == seachParams.Status);
                }
                if (seachParams.UserID != null)
                {
                    tempIq = tempIq.Where(s => s.UsersID == seachParams.UserID);
                }
                var result = tempIq.Select(s => new OrderDto
                {
                    AdminID = s.AdminID,
                    CompleteTime = s.CompleteTime,
                    CreateTime = s.CreateTime,
                    Head_Notes = s.Head_Notes,
                    LinesCount = s.GoodsCount,
                    DeliverCount = s.DeliverCount,
                    Money = s.Money,
                    Orders_Head_ID = s.OrdersHeadID,
                    Out_Trade_No = s.OutTradeNo,
                    RcAddr = s.RcAddr,
                    Status = s.Status,
                    RcName = s.RcName,
                    RcPhone = s.RcPhone,
                    UpdateTime = s.UpdateTime,
                    UserName = s.le_users.UsersNickname,
                    UsersID = s.UsersID,
                    ExpressType = s.ExpressType,
                    OrderType = s.OrderType
                }
                );
                result = result.OrderByDescending(s => s.CreateTime);
                Count = result.Count();
                result = result.Skip(seachParams.Offset).Take(seachParams.Rows);

                return result.ToList();
            }
        }

        /// <summary>
        /// 修改订单收货信息
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool EditReceiptInfo(EditReceiptInfo dto, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    var model = ctx.le_orders_head.Where(s => s.OutTradeNo == dto.Out_Trade_No).FirstOrDefault();

                    if (model == null)
                    {
                        msg = "该订单不存在，请确认后重试";
                        return false;
                    }

                    model.RcAddr = dto.RcAddr;
                    model.RcName = dto.RcName;
                    model.RcPhone = dto.RcPhone;

                    ctx.Entry<le_orders_head>(model).State = EntityState.Modified;

                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }
                    else
                    {
                        msg = "修改失败，请稍后重试";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    msg = "修改异常，异常信息：" + ex.ToString();
                    return false;
                }
            }
        }

        /// <summary>
        /// 更新订单头状态
        /// </summary>
        /// <param name="Out_Trade_No"></param>
        /// <param name="Status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        //public bool UpdateOrderStatus(string Out_Trade_No, int Status, out string msg)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        msg = "";
        //        try
        //        {
        //            var model = ctx.le_orders_head.Where(s => s.Out_Trade_No == Out_Trade_No).FirstOrDefault();
        //            if (model == null)
        //            {
        //                msg = "该订单不存在，请确认后重试";
        //                return false;
        //            }
        //            var OrderlineList = ctx.le_orders_lines.Where(s => s.Out_Trade_No == Out_Trade_No).ToList();

        //            if (OrderlineList.Any(s => s.Status == 1 || s.Status == 2) && model.Status == 5)
        //            {
        //                msg = "取消订单失败,已有订单在运行";
        //                return false;
        //            }
        //            if (model.Status == 1)
        //            {
        //                msg = "该订单已完成，请勿重复操作";
        //                return false;
        //            }
        //            model.Status = Status;
        //            model.UpdateTime = DateTime.Now;
        //            model.CompleteTime = DateTime.Now;
        //            ctx.Entry<le_orders_head>(model).State = EntityState.Modified;

        //            if (ctx.SaveChanges() > 0)
        //            {
        //                msg = "SUCCESS";
        //                return true;
        //            }
        //            else
        //            {
        //                msg = "修改失败，请稍后重试";
        //                return false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            msg = "修改异常，异常信息：" + ex.ToString();
        //            return false;
        //        }
        //    }

        //}

        /// <summary>
        /// 修改订单行状态
        /// </summary>
        /// <param name="Orders_Lines_ID"></param>
        /// <param name="Status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UpdateLineStatus(int Orders_Lines_ID, int Status, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    var model = ctx.le_orders_lines.Where(s => s.OrdersLinesID == Orders_Lines_ID).FirstOrDefault();

                    if (model == null)
                    {
                        msg = "该订单行不存在，请确认后重试";
                        return false;
                    }

                    if (model.Status == 2)
                    {
                        msg = "该订单行已接单，请勿重复操作";
                        return false;
                    }

                    if (model.Status == 3)
                    {
                        msg = "该订单行已取消，请勿重复操作";
                        return false;
                    }

                    model.Status = Status;

                    ctx.Entry<le_orders_lines>(model).State = EntityState.Modified;

                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }
                    else
                    {
                        msg = "修改失败，请稍后重试";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    msg = "修改异常，异常信息：" + ex.ToString();
                    return false;
                }
            }
        }

        /// <summary>
        /// 修改订单备注信息
        /// </summary>
        /// <param name="Out_Trade_No"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool EditOrderHead_Notes(string Out_Trade_No, string Head_Notes, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                msg = "";
                try
                {
                    var model = ctx.le_orders_head.Where(s => s.OutTradeNo == Out_Trade_No).FirstOrDefault();

                    if (model == null)
                    {
                        msg = "该订单不存在，请确认后重试";
                        return false;
                    }

                    model.Head_Notes = Head_Notes;

                    ctx.Entry<le_orders_head>(model).State = EntityState.Modified;

                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }
                    else
                    {
                        msg = "修改失败，请稍后重试";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    msg = "修改异常，异常信息：" + ex.ToString();
                    return false;
                }
            }
        }

        /// <summary>
        /// 批量修改订单行供货商/商品数量
        /// </summary>
        /// <param name="List"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool BatchEditLinesInfo(List<EditLinesInfo> List, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                msg = "";
                if (List.Count <= 0)
                {
                    msg = "请确认需要编辑的信息";
                    return false;
                }
                try
                {
                    foreach (var data in List)
                    {
                        decimal BeforeMoney = 0; decimal AfterMoney = 0; int BeforeCount = 0; int AfterCount = 0;

                        decimal SupplyPrice;//供货价格

                        var Linemodel = ctx.le_orders_lines.Where(s => s.OrdersLinesID == data.Orders_Lines_ID).FirstOrDefault();

                        if (Linemodel == null)
                        {
                            msg = data.Orders_Lines_ID + "订单不存在，请确认后重试";
                            return false;
                        }
                        if (!string.IsNullOrEmpty(data.Notes))
                        {
                            Linemodel.Notes = data.Notes;
                        }
                        if (Linemodel.SuppliersID != data.SuppliersID)
                        {
                            SupplyPrice = ctx.le_goods_suppliers.Where(s => s.SuppliersID == data.SuppliersID && s.GoodsID == Linemodel.GoodsID).Select(s => s.Supplyprice).FirstOrDefault();
                            Linemodel.SuppliersID = data.SuppliersID;
                        }
                        if (Linemodel.GoodsCount != data.GoodsCount)
                        {

                            //更新订单行商品数，需要重新计算订单行价格                       
                            BeforeCount = Linemodel.GoodsCount;//更新前订单行数量
                            AfterCount = data.GoodsCount; //更新之后订单行金额
                            Linemodel.GoodsCount = AfterCount;
                            // var modgood = ctx.le_goods.Where(s => s.GoodsID == Linemodel.Goods_ID).FirstOrDefault();

                            //更新订单头金额
                            var modhead = Linemodel.le_orders_head;// ctx.le_orders_head.Where(s => s.Out_Trade_No == Linemodel.Out_Trade_No).FirstOrDefault();

                            BeforeMoney = modhead.Money;//更新前订单金额

                            if (modhead != null)
                            {
                                if (Linemodel.GoodsCount <= 0)  //判断所有订单是否为已取消状态 是 改变订单头为已取消
                                {
                                    var LineList = modhead.le_orders_lines.Where(s => s.OrdersLinesID != Linemodel.OrdersLinesID).ToArray();
                                    if (LineList.Count(s => s.Status == 3) == LineList.Count())
                                    {
                                        modhead.Status = 5;
                                    }
                                    Linemodel.GoodsCount = 0;
                                    Linemodel.Status = 3;
                                }

                                modhead.SupplyMoney = modhead.SupplyMoney + (AfterCount - BeforeCount) * Linemodel.Money;
                                modhead.Money = BeforeMoney + (AfterCount - BeforeCount) * Linemodel.Money;
                                modhead.DeliverCount = AfterCount; // modhead.LinesCount + (model.GoodsCount - BeforeCount);
                                if (modhead.DeliverCount < 0)
                                {
                                    modhead.DeliverCount = 0;
                                }
                                ctx.Entry<le_orders_head>(modhead).State = EntityState.Modified;
                            }

                            #region 添加订单行变动记录
                            //OrderLineChangeRecordDTO dto = new OrderLineChangeRecordDTO();
                            //dto.OrderLineID = data.Orders_Lines_ID;
                            //dto.BeforeCount = BeforeCount;
                            //dto.AfterCount = AfterCount;
                            //dto.BeforeMoney = Linemodel.Money * BeforeCount;
                            //dto.AfterMoney = Linemodel.Money * AfterCount;
                            //dto.UserID = data.UserID;
                            //dto.UserType = data.UserType;
                            //var bol = ORservice.AddOrderLineChangeRecord(dto, out string MSG);

                            //if (!bol)
                            //{
                            //    msg = MSG;
                            //    return false;
                            //}

                            #endregion
                        }

                        ctx.Entry<le_orders_lines>(Linemodel).State = EntityState.Modified;


                    }

                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }
                    else
                    {
                        msg = "修改失败，请稍后重试";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    msg = "修改异常，异常信息：" + ex.ToString();
                    return false;
                }
            }
        }

        /// <summary>
        /// 查询商品的供货商信息
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public List<GoodsSuppliersInfoDto> GetGoodsSuppliersList(int GoodsID, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    string sql = string.Format(@"select lg.GoodsID,lg.GoodsName,ls.Suppliers_Name,ls.Suppliers_MobilePhone,lgs.Price,ls.SuppliersID from le_suppliers ls 
left join le_goods_suppliers lgs on lgs.SuppliersID = ls.SuppliersID
left join le_goods lg on lg.GoodsID=lgs.GoodsID
where lg.GoodsID=@GoodsID");

                    MySqlParameter[] parameters =
                    {
                    new MySqlParameter("@GoodsID", MySqlDbType.Int32),
                };
                    parameters[0].Value = GoodsID;

                    var resule = ctx.Database.SqlQuery<GoodsSuppliersInfoDto>(sql, parameters).ToList();
                    msg = "SUCCESS";
                    return resule;
                }
                catch (Exception ex)
                {
                    msg = "查询异常，信息：" + ex.ToString();
                    return null;
                }
            }
        }

        /// <summary>
        /// 用户取消订单
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public bool UserCancelOrder(int UserID, string OrderNo)
        {
            using (Entities ctx = new Entities())
            {
                var orderModel = ctx.le_orders_head.Where(s => s.UsersID == UserID && s.OutTradeNo == OrderNo).FirstOrDefault();
                if (orderModel == null)
                {
                    return false;
                }
                if (orderModel.Status != 0)
                {
                    return false;
                }
                orderModel.Status = 5;
                ctx.Entry<le_orders_head>(orderModel).State = EntityState.Modified;
                if (ctx.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        /// <summary>
        /// 查询订单行列表
        /// </summary>
        /// <param name="seachParams"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        //public List<OrderLineDto> GetOrderLineList(OrderSeachParams seachParams, out int Count)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        var tempIq = ctx.le_orders_lines.Where(s => s.CreateTime >= seachParams.BeginTime && s.CreateTime <= seachParams.EndTime);
        //        if (seachParams.Status != null)
        //        {
        //            tempIq = tempIq.Where(s => s.Status == seachParams.Status);
        //        }
        //if (!string.IsNullOrEmpty(seachParams.KeyWords))
        //{
        //    tempIq = tempIq.Where(s => s..Contains(seachParams.KeyWords)
        //      || s.RcPhone.Contains(seachParams.KeyWords)
        //      || s.Head_Notes.Contains(seachParams.KeyWords)
        //      || s.RcAddr.Contains(seachParams.KeyWords)
        //      || s.le_users.UsersNickname.Contains(seachParams.KeyWords)
        //    );
        //}
        //        if (!string.IsNullOrEmpty(seachParams.Out_Trade_No))
        //        {
        //            tempIq = tempIq.Where(s => s.Out_Trade_No == seachParams.Out_Trade_No);
        //        }
        //        if (seachParams.Status != null)
        //        {
        //            tempIq = tempIq.Where(s => s.Status == seachParams.Status);
        //        }
        //        if (seachParams.UserID != null)
        //        {
        //            tempIq = tempIq.Where(s => s.UsersID == seachParams.UserID);
        //        }
        //        var result = tempIq.Select(s => new OrderLineDto
        //        {
        //            OrderLineID = s.Orders_Lines_ID,
        //            OrderNo = s.Out_Trade_No,
        //            Goods_ID = s.Goods_ID,
        //            GoodsName = s.le_goods.GoodsName,
        //            GoodsImage = s.le_goods.Image,
        //            Money = s.Money,
        //            GoodsCount = s.GoodsCount,
        //            Notes = s.Notes,
        //            Status = s.Status,
        //            AdminID = s.AdminID,
        //            UsersID = s.UsersID,
        //            UsersName = s.le_users.UsersName,
        //            SuppliersID = s.SuppliersID,
        //            UpdateTime = s.UpdateTime,
        //            CreateTime = s.CreateTime,
        //            Category1 = s.le_goods_value.g,
        //            Category2 = s.Category2,
        //            Category3 = s.Category3,
        //            Category4 = s.Category4,
        //            Category5 = s.Category5
        //        }
        //        );
        //        result = result.OrderByDescending(s => s.CreateTime);
        //        Count = result.Count();
        //        result = result.Skip(seachParams.Offset).Take(seachParams.Rows);

        //        return result.ToList();
        //    }
        //}

        /// <summary>
        /// 查询订单行明细
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        //public OrderLineDto GetOrderLineDetail(int ID, out string msg)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        OrderLineDto model = new OrderLineDto();
        //        var result = ctx.le_orders_lines.Where(s => s.Orders_Lines_ID == ID)
        //            .Select(s => new OrderLineDto
        //        {
        //            OrderLineID = s.Orders_Lines_ID,
        //            OrderNo = s.Out_Trade_No,
        //            Goods_ID = s.Goods_ID,
        //            GoodsName = s.le_goods.GoodsName,
        //            GoodsImage = s.le_goods.Image,
        //            Money = s.Money,
        //            GoodsCount = s.GoodsCount,
        //            Notes = s.Notes,
        //            Status = s.Status,
        //            AdminID = s.AdminID,
        //            UsersID = s.UsersID,
        //            UsersName = s.le_users.UsersName,
        //            SuppliersID = s.SuppliersID,
        //            UpdateTime = s.UpdateTime,
        //            CreateTime = s.CreateTime,
        //            Category1 = s.Category1,
        //            Category2 = s.Category2,
        //            Category3 = s.Category3,
        //            Category4 = s.Category4,
        //            Category5 = s.Category5
        //        }
        //        ).ToList();

        //        if (result.Count <= 0)
        //        {
        //            msg = "该记录不存在";
        //            return null;
        //        }

        //        model = result[0];

        //        if (model.AdminID != null && model.AdminID > 0)
        //        {
        //            model.AdminName = ctx.le_admin.Where(s => s.AdminID == model.AdminID).ToList()[0].loginname;
        //        }

        //        if (model.SuppliersID != null && model.SuppliersID > 0)
        //        {
        //            model.SuppliersName = ctx.le_suppliers.Where(s => s.SuppliersID == model.SuppliersID).ToList()[0].SuppliersName;
        //        }

        //        List<GoodsValues> GoodsValuesList = new List<GoodsValues>();

        //        if (model.Category1 != null && model.Category1 > 0) {
        //            GoodsValues group = new GoodsValues();
        //            group = GetValueByID((int)model.Category1);
        //            GoodsValuesList.Add(group);
        //        }
        //        if (model.Category2 != null && model.Category2 > 0)
        //        {
        //            GoodsValues group = new GoodsValues();
        //            group = GetValueByID((int)model.Category2);
        //            GoodsValuesList.Add(group);
        //        }
        //        if (model.Category3 != null && model.Category3 > 0)
        //        {
        //            GoodsValues group = new GoodsValues();
        //            group = GetValueByID((int)model.Category3);
        //            GoodsValuesList.Add(group);
        //        }

        //        model.GoodsValuesList = GoodsValuesList;
        //        model.StatusName = OrderLineStatusSwitch(model.Status);
        //        msg = "SUCCESS";
        //        return model;
        //    }
        //}

        //public GoodsValues GetValueByID(int ID)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        GoodsValues model = new GoodsValues();

        //        var result = (from a in ctx.le_goods_value
        //                     join b in ctx.le_goods_value_mapping 
        //                     on a.CoodsValueMappingID equals b.ID
        //                     where a.ID == ID
        //                     select new GoodsValues
        //                     {
        //                         CoodsValueMappingID = b.ID,
        //                         GoodsValueName = a.GoodsValue,
        //                         CategoryType = b.CategoryType,
        //                         GoodsValueID = a.ID
        //                     }).ToList();

        //        if (result.Count > 0)
        //        {
        //            model = result[0];
        //            model.CategoryTypeName = GoodsValueSwitch(model.CategoryType);
        //        }

        //        return model;
        //    }
        //}

        public string GoodsValueSwitch(int Num)
        {
            switch (Num)
            {
                case 1:
                    return "口味";
                    break;

                case 2:
                    return "颜色";
                    break;

                case 3:
                    return "尺寸";
                    break;
                default:
                    return "";
                    break;
            }
        }

        public string OrderLineStatusSwitch(int Num)
        {
            switch (Num)
            {
                case 0:
                    return "未派发";
                    break;

                case 1:
                    return "待接单";
                    break;

                case 2:
                    return "已接单";
                    break;

                case 3:
                    return "已取消";
                    break;

                default:
                    return "";
                    break;
            }
        }
    }
}
