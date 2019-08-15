using DTO.HqManager.OperationRecord;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    /// <summary>
    /// 操作记录方法
    /// </summary>
    public class OperationRecordService
    {
        #region 商品操作记录
        /// <summary>
        /// 增加 商品操作记录
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        //public bool AddGoodsOperationRecords(GoodsOperationRecordDTO dto , out string msg)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        try {
        //            if (dto == null)
        //            {
        //                msg = "参数为空，请确认后重试";
        //                return false;
        //            }

        //            le_goods_operationrecord model = new le_goods_operationrecord();
        //            model.AdminID = dto.AdminID;
        //            model.GoodsID = dto.GoodsID;
        //            model.PreGoodsName = dto.PreGoodsName;
        //            model.AfterGoodsName = dto.AfterGoodsName;
        //            model.PreOriginalPrice = dto.PreOriginalPrice;
        //            model.AfterOriginalPrice = dto.AfterOriginalPrice;
        //            model.PreSpecialOffer = dto.PreSpecialOffer;
        //            model.AfterSpecialOffer = dto.AfterSpecialOffer;

        //            ctx.le_goods_operationrecord.Add(model);
        //            if (ctx.SaveChanges() > 0)
        //            {
        //                msg = "新增成功";
        //                return true;
        //            }

        //            msg = "新增失败";
        //            return false;
        //        } catch (Exception ex)
        //        {
        //            msg = "新增失败,失败原因：" + ex.ToString();
        //            return false;
        //        }
        //    }
        //}

        /// <summary>
        /// 查询 商品操作记录
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="AdminID"></param>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        //public List<GoodsOperationRecordDTO> GetGoodsOperationRecordsList(out string msg,int AdminID = 0, int GoodsID = 0)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        try {
        //            List<GoodsOperationRecordDTO> List = new List<GoodsOperationRecordDTO>();
        //            var tempIq = from a in ctx.le_goods_operationrecord
        //                         join b in ctx.le_admin
        //                         on a.AdminID equals b.AdminID
        //                         join c in ctx.le_goods
        //                         on a.GoodsID equals c.GoodsID
        //                         select new GoodsOperationRecordDTO
        //                         {
        //                             OperationRecordID = a.OperationRecordID,
        //                             AdminID = a.AdminID,
        //                             AdminName = b.LoginName,
        //                             GoodsID = a.GoodsID,
        //                             GoodsName = c.GoodsName,
        //                             PreGoodsName = a.PreGoodsName,
        //                             AfterGoodsName = a.AfterGoodsName,
        //                             PreOriginalPrice = a.PreOriginalPrice,
        //                             AfterOriginalPrice = a.AfterOriginalPrice,
        //                             PreSpecialOffer = a.PreSpecialOffer,
        //                             AfterSpecialOffer = a.AfterSpecialOffer,
        //                             CreateTime = a.CreateTime
        //                         }; 

        //            if (AdminID > 0 && GoodsID > 0)
        //            {
        //                List = tempIq.Where(s => s.AdminID == AdminID && s.GoodsID == GoodsID).OrderByDescending(s => s.CreateTime).Select(s => new GoodsOperationRecordDTO
        //                {
        //                    OperationRecordID = s.OperationRecordID,
        //                    AdminID = s.AdminID,
        //                    AdminName = s.AdminName,
        //                    GoodsID = s.GoodsID,
        //                    GoodsName = s.GoodsName,
        //                    PreGoodsName = s.PreGoodsName,
        //                    AfterGoodsName = s.AfterGoodsName,
        //                    PreOriginalPrice = s.PreOriginalPrice,
        //                    AfterOriginalPrice = s.AfterOriginalPrice,
        //                    PreSpecialOffer = s.PreSpecialOffer,
        //                    AfterSpecialOffer = s.AfterSpecialOffer,
        //                    CreateTime = s.CreateTime
        //                }).ToList();
        //            }
        //            else if (AdminID > 0 && GoodsID == 0)
        //            {
        //                List = tempIq.Where(s => s.AdminID == AdminID).OrderByDescending(s => s.CreateTime).Select(s => new GoodsOperationRecordDTO
        //                {
        //                    OperationRecordID = s.OperationRecordID,
        //                    AdminID = s.AdminID,
        //                    AdminName = s.AdminName,
        //                    GoodsID = s.GoodsID,
        //                    GoodsName = s.GoodsName,
        //                    PreGoodsName = s.PreGoodsName,
        //                    AfterGoodsName = s.AfterGoodsName,
        //                    PreOriginalPrice = s.PreOriginalPrice,
        //                    AfterOriginalPrice = s.AfterOriginalPrice,
        //                    PreSpecialOffer = s.PreSpecialOffer,
        //                    AfterSpecialOffer = s.AfterSpecialOffer,
        //                    CreateTime = s.CreateTime
        //                }).ToList();
        //            }
        //            else if (GoodsID > 0 && AdminID == 0)
        //            {
        //                List = tempIq.Where(s => s.GoodsID == GoodsID).OrderByDescending(s => s.CreateTime).Select(s => new GoodsOperationRecordDTO
        //                {
        //                    OperationRecordID = s.OperationRecordID,
        //                    AdminID = s.AdminID,
        //                    AdminName = s.AdminName,
        //                    GoodsID = s.GoodsID,
        //                    GoodsName = s.GoodsName,
        //                    PreGoodsName = s.PreGoodsName,
        //                    AfterGoodsName = s.AfterGoodsName,
        //                    PreOriginalPrice = s.PreOriginalPrice,
        //                    AfterOriginalPrice = s.AfterOriginalPrice,
        //                    PreSpecialOffer = s.PreSpecialOffer,
        //                    AfterSpecialOffer = s.AfterSpecialOffer,
        //                    CreateTime = s.CreateTime
        //                }).ToList();
        //            }

        //            if (List.Count > 0)
        //            {
        //                msg = "SUCCESS";
        //                return List;
        //            }

        //            msg = "查询失败";
        //            return List;

        //        } catch (Exception ex)
        //        {
        //            msg = "查询失败,失败原因："+ ex.ToString();
        //            return null;
        //        }
        //    }
        //}

        #endregion

        #region 订单操作记录
        /// <summary>
        /// 增加 订单操作记录
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool AddOrdersOperationRecords(OrdersOperationRecordDTO dto, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    if (dto == null)
                    {
                        msg = "参数为空，请确认后重试";
                        return false;
                    }

                    le_orders_operationrecord model = new le_orders_operationrecord();
                    model.AdminID = dto.AdminID;
                    model.Orders_Head_ID = dto.Orders_Head_ID;
                    model.OperationAction = dto.OperationAction;

                    ctx.le_orders_operationrecord.Add(model);
                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "新增成功";
                        return true;
                    }

                    msg = "新增失败";
                    return false;
                }
                catch (Exception ex)
                {
                    msg = "新增失败,失败原因：" + ex.ToString();
                    return false;
                }
            }
        }

        /// <summary>
        /// 查询 订单操作记录
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="AdminID"></param>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        public List<OrdersOperationRecordDTO> GetOrdersOperationRecordsList(out string msg, int AdminID = 0, int Orders_Head_ID = 0)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    List<OrdersOperationRecordDTO> List = new List<OrdersOperationRecordDTO>();
                    var tempIq = from a in ctx.le_orders_operationrecord
                                 join b in ctx.le_admin
                                 on a.AdminID equals b.AdminID
                                 join c in ctx.le_orders_head
                                 on a.Orders_Head_ID equals c.OrdersHeadID
                                 select new OrdersOperationRecordDTO
                                 {
                                     OperationRecordID = a.OperationRecordID,
                                     AdminID = a.AdminID,
                                     Orders_Head_ID = a.Orders_Head_ID,
                                     OperationAction = a.OperationAction,
                                     CreateTime = a.CreateTime,
                                     AdminName = b.LoginName,
                                     Out_Trade_No = c.OutTradeNo
                                 };

                    if (AdminID > 0 && Orders_Head_ID > 0)
                    {
                        List = tempIq.Where(s => s.AdminID == AdminID && s.Orders_Head_ID == Orders_Head_ID).OrderByDescending(s => s.CreateTime).Select(s => new OrdersOperationRecordDTO
                        {
                            AdminID = s.AdminID,
                            Orders_Head_ID = s.Orders_Head_ID,
                            OperationAction = s.OperationAction,
                            Out_Trade_No = s.Out_Trade_No,
                            AdminName = s.AdminName,
                            CreateTime = s.CreateTime
                        }).ToList();
                    }
                    else if (AdminID > 0 && Orders_Head_ID == 0)
                    {
                        List = tempIq.Where(s => s.AdminID == AdminID).OrderByDescending(s => s.CreateTime).Select(s => new OrdersOperationRecordDTO
                        {
                            AdminID = s.AdminID,
                            Orders_Head_ID = s.Orders_Head_ID,
                            OperationAction = s.OperationAction,
                            Out_Trade_No = s.Out_Trade_No,
                            AdminName = s.AdminName,
                            CreateTime = s.CreateTime
                        }).ToList();
                    }
                    else if (Orders_Head_ID > 0 && AdminID == 0)
                    {
                        List = tempIq.Where(s => s.Orders_Head_ID == Orders_Head_ID).OrderByDescending(s => s.CreateTime).Select(s => new OrdersOperationRecordDTO
                        {
                            AdminID = s.AdminID,
                            Orders_Head_ID = s.Orders_Head_ID,
                            OperationAction = s.OperationAction,
                            Out_Trade_No = s.Out_Trade_No,
                            AdminName = s.AdminName,
                            CreateTime = s.CreateTime
                        }).ToList();
                    }

                    if (List.Count > 0)
                    {
                        msg = "SUCCESS";
                        return List;
                    }

                    msg = "查询失败";
                    return List;

                }
                catch (Exception ex)
                {
                    msg = "查询失败,失败原因：" + ex.ToString();
                    return null;
                }
            }
        }

        #endregion

        #region 订单头变动记录
        /// <summary>
        /// 新增订单头变动记录
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool AddOrderHeadChangeRecord(OrderHeadChangeRecordDTO dto, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    if (dto == null)
                    {
                        msg = "参数为空，请确认后重试";
                        return false;
                    }

                    le_orders_head_log model = new le_orders_head_log();
                    model.OrderHeadID = dto.OrderHeadID;
                    model.BeforeCount = dto.BeforeCount;
                    model.AfterCount = dto.AfterCount;
                    model.BeforeAmount = dto.BeforeMoney;
                    model.AfterAmount = dto.AfterMoney;
                    model.AdminID = dto.AdminID;

                    ctx.le_orders_head_log.Add(model);
                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "新增成功";
                        return true;
                    }

                    msg = "新增失败";
                    return false;
                }
                catch (Exception ex)
                {
                    msg = "新增失败,失败原因：" + ex.ToString();
                    return false;
                }
            }
        }

        //public List<OrderHeadChangeRecordDTO> GetOrderHeadChangeRecordList(out string msg, int AdminID = 0, int Orders_Head_ID = 0)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        var tempIq = from a in ctx.le_orders_head_log
        //                     join b in ctx.le_admin
        //                     on a.AdminID equals b.AdminID
        //                     join c in ctx.le_orders_head
        //                     on a.HeadRecordID equals c.Orders_Head_ID
        //                     select new OrderHeadChangeRecordDTO
        //                     {
        //                         HeadRecordID = a.HeadRecordID,
        //                         OrderHeadID = a.OrderHeadID,
        //                         BeforeCount = a.BeforeCount,
        //                         AfterCount = a.AfterCount,
        //                         BeforeMoney = a.BeforeMoney,
        //                         AfterMoney = a.AfterMoney,
        //                         CreateTime = a.CreateTime,
        //                         AdminID = a.AdminID,
        //                         AdminName = b.LoginName
        //                     };

        //        if (AdminID > 0)
        //        {
        //            tempIq = tempIq.Where(s => s.AdminID == AdminID);
        //        }

        //        if (Orders_Head_ID > 0)
        //        {
        //            tempIq = tempIq.Where(s => s.OrderHeadID == Orders_Head_ID);
        //        }

        //        var result = tempIq.OrderByDescending(s => s.CreateTime).Select(s => new OrderHeadChangeRecordDTO
        //        {
        //            HeadRecordID = s.HeadRecordID,
        //            OrderHeadID = s.OrderHeadID,
        //            BeforeCount = s.BeforeCount,
        //            AfterCount = s.AfterCount,
        //            BeforeMoney = s.BeforeMoney,
        //            AfterMoney = s.AfterMoney,
        //            CreateTime = s.CreateTime,
        //            AdminID = s.AdminID,
        //            AdminName = s.AdminName
        //        }).ToList();

        //        msg = "SUCCESS";
        //        return result;
        //    }
        //}
        #endregion

        #region 订单行变动记录

        //public bool AddOrderLineChangeRecord(OrderLineChangeRecordDTO dto, out string msg)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        try
        //        {
        //            if (dto == null)
        //            {
        //                msg = "参数为空，请确认后重试";
        //                return false;
        //            }

        //            le_orders_lines_log model = new le_orders_lines_log();
        //            model.OrderLineID = dto.OrderLineID;
        //            model.BeforeCount = dto.BeforeCount;
        //            model.AfterCount = dto.AfterCount;
        //            model.BeforeMoney = dto.BeforeMoney;
        //            model.AfterMoney = dto.AfterMoney;
        //            model.UserID = dto.UserID;
        //            model.UserType = dto.UserType;

        //            ctx.le_orders_lines_log.Add(model);
        //            if (ctx.SaveChanges() > 0)
        //            {
        //                msg = "新增成功";
        //                return true;
        //            }

        //            msg = "新增失败";
        //            return false;
        //        }
        //        catch (Exception ex)
        //        {
        //            msg = "新增失败,失败原因：" + ex.ToString();
        //            return false;
        //        }
        //    }
        //}

        #endregion
    }
}
