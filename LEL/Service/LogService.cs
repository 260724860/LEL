﻿using DTO.Common;
using DTO.LogDto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class LogService
    {
        /// <summary>
        /// 获取订单行修改记录
        /// </summary>
        /// <param name="SeachOptions"></param>
        /// <param name="AdminID"></param>
        /// <param name="LinesRecordID"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<OrderLineLogDto> GetOrderLineLogList(SeachDateTimeOptions SeachOptions, int? AdminID, List<int> LinesRecordID, out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_orders_lines_log.Where(s => true);
                if (LinesRecordID.Count > 0)
                {
                    tempIq = tempIq.Where(s => LinesRecordID.Contains(s.OrderLineID));
                }
                if (!string.IsNullOrEmpty(SeachOptions.KeyWords))
                {
                    tempIq = tempIq.Where(s => s.le_admin.LoginName.Contains(SeachOptions.KeyWords)
                      || s.le_suppliers.SuppliersName.Contains(SeachOptions.KeyWords)
                      || s.le_users.UsersNickname.Contains(SeachOptions.KeyWords)

                    );
                }
                if (AdminID != null)
                {
                    tempIq = tempIq.Where(s => s.AdminID == AdminID);
                }
                var result = tempIq.Select(s => new OrderLineLogDto
                {
                    AdminName = s.le_admin.Nickname,
                    AfterMoney = s.AfterMoney,
                    AfterCount = s.AfterCount,
                    SupplierName = s.le_suppliers.SuppliersName,
                    AfterStatus = s.AfterStatus,
                    BeforeStatus = s.BeforeStatus,
                    BeforeCount = s.BeforeCount,
                    BeforeMoney = s.BeforeMoney,
                    CreateTime = s.CreateTime,
                    UserID = s.UserID,
                    UserName = s.le_users.UsersNickname,
                    OrderLineID = s.OrderLineID,
                    GoodsId=s.le_orders_lines.le_goods.GoodsID,
                    GoodsName=s.le_orders_lines.le_goods.GoodsName
                    
                });
                Count = result.Count();
                result = result.OrderByDescending(s => s.CreateTime);

                result = result.Skip(SeachOptions.Offset).Take(SeachOptions.Rows);

                return result.ToList();

            }
        }
        /// <summary>
        /// 获取订单头修改记录
        /// </summary>
        /// <param name="SeachOptions"></param>
        /// <param name="AdminID"></param>
        /// <param name="HeadRecordID"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<OrderHeadLogDto> GetOrderHeadLogList(SeachDateTimeOptions SeachOptions, int? AdminID, int? OrderHeadID, out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_orders_head_log.Where(s => true);
                if (OrderHeadID != null)
                {
                    tempIq = tempIq.Where(s => s.OrderHeadID == OrderHeadID);
                }
                if (!string.IsNullOrEmpty(SeachOptions.KeyWords))
                {
                    tempIq = tempIq.Where(s => s.le_admin.LoginName.Contains(SeachOptions.KeyWords)
                      || s.le_suppliers.SuppliersName.Contains(SeachOptions.KeyWords)
                      || s.le_users.UsersNickname.Contains(SeachOptions.KeyWords)

                    );
                }
                if (AdminID != null)
                {
                    tempIq = tempIq.Where(s => s.AdminID == AdminID);
                }
                var result = tempIq.Select(s => new OrderHeadLogDto
                {
                    AdminName = s.le_admin.Nickname,
                    AfterMoney = s.AfterAmount,
                    SupplierName = s.le_suppliers.SuppliersName,
                    AfterStatus = s.AfterStatus,
                    BeforeStatus = s.BeforeStatus,
                    BeforeCount = s.BeforeCount,
                    AfterCount = s.AfterCount,
                    OrderHeadID = s.OrderHeadID,
                    SupplierID = s.SupplierID,
                    AdminID = s.AdminID,
                    BeforeMoney = s.BeforeAmount,
                    CreateTime = s.CreateTime,
                    UserID = s.UserID,
                    UserName = s.le_users.UsersNickname,

                });
                result = result.OrderByDescending(s => s.CreateTime);
                Count = result.Count();
                result = result.Skip(SeachOptions.Offset).Take(SeachOptions.Rows);

                return result.ToList();

            }
        }

        /// <summary>
        /// 获取商品操作日志
        /// </summary>
        /// <param name="SeachOptions"></param>
        /// <param name="AdminID"></param>
        /// <param name="GoodsID"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<GoodsLogDto> GetGoodsLogList(SeachDateTimeOptions SeachOptions, int? AdminID, int? GoodsID, out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_goods_log.Where(s => true);
                if (AdminID != null)
                {
                    tempIq = tempIq.Where(s => s.AdminID == AdminID);
                }
                if (GoodsID != null)
                {
                    tempIq = tempIq.Where(s => s.GoodsID == GoodsID);
                }
                if (SeachOptions.BeginTime != null)
                {
                    tempIq = tempIq.Where(s => s.CreateTime >= SeachOptions.BeginTime);
                }
                if (SeachOptions.EndTime != null)
                {
                    tempIq = tempIq.Where(s => s.CreateTime <= SeachOptions.EndTime);
                }
                Count = tempIq.Count();

                var result = tempIq.Select(s => new GoodsLogDto
                {
                    AdminID = s.AdminID,
                    AdminName = s.le_admin.Nickname,
                    AfterGoodsName = s.AfterGoodsName,
                    AfterQuota = s.AfterQuota,

                    AfterSheLvesStatus = s.AfterSheLvesStatus,
                    AfterSpecialOffer = s.AfterSpecialOffer,
                    AfterStock = s.AfterStock,
                    BeforeSheLvesStatus = s.BeforeSheLvesStatus,
                    BeforeSpecialOffer = s.BeforeSpecialOffer,
                    BeforeStock = s.BeforeStock,
                    BeforeGoodsName = s.BeforeGoodsName,
                    BeforeQuota = s.BeforeQuota,
                    CreateTime = s.CreateTime,
                    GoodsID = s.GoodsID,
                    OperationRecordID = s.OperationRecordID,
                    AfterMinimumPurchase=s.AfterMinimumPurchase,
                    BeforeMinimumPurchase=s.BeforeMinimumPurchase
                    


                });
                result = result.OrderByDescending(s => s.CreateTime);
                result = result.Skip(SeachOptions.Offset).Take(SeachOptions.Rows);
                return result.ToList();
            }
            //    Count = 0;
            //return null;
        }

        /// <summary>
        /// 添加订单头日志
        /// </summary>
        /// <param name="Before"></param>
        /// <param name="After"></param>
        /// <param name="UserID"></param>
        /// <param name="AdminID"></param>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public le_orders_head_log AddOrderHeadLog(le_orders_head Before ,le_orders_head After,int? UserID,int? AdminID,int? SupplierID,string Actions)
        {
            le_orders_head_log log = new le_orders_head_log();
            log.CreateTime = DateTime.Now;
            log.BeforeCount = Before.DeliverCount;
            log.BeforeAmount = Before.RealAmount;
            log.BeforeStatus = Before.Status;
            log.HeadRecordID = Before.OrdersHeadID;
            log.UserID = UserID;
            log.AdminID = AdminID;
            log.SupplierID = SupplierID;
            log.BeforeSupplierAmout = Before.RealSupplyAmount;
            log.Actions = Actions;
            bool Isadd = false;
            if (Before.DeliverCount != After.DeliverCount)
            {
                Isadd = true;
                log.AfterCount = After.DeliverCount;
            }
            if (Before.RealAmount != After.RealAmount)
            {
                Isadd = true;
                log.AfterAmount = After.RealAmount;
            }
            if (Before.RealSupplyAmount != After.RealSupplyAmount)
            {
                Isadd = true;
                log.AfterSupplierAmout = After.RealSupplyAmount;
            }
            if (Before.Status != After.Status)
            {
                Isadd = true;
                log.AfterStatus= After.Status;
            }
            if (Isadd)
            {
                return log;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加订单行日志
        /// </summary>
        /// <param name="Before"></param>
        /// <param name="After"></param>
        /// <param name="UserID"></param>
        /// <param name="AdminID"></param>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public le_orders_lines_log AddOrderLines(le_orders_lines Before, int? UserID, int? AdminID, int? SupplierID,string Actions)
        {
            le_orders_lines_log log = new le_orders_lines_log();
            log.AdminID = AdminID;
            log.UserID = UserID;
            log.SupplierID = SupplierID;
            log.BeforeCount = Before.DeliverCount;
            log.BeforeMoney = Before.SupplyPrice;
            log.BeforeStatus = Before.Status;
            log.Actions = Actions;

            return log;
        }
    }
}
