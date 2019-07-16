using Common;
using DTO.Goods;
using DTO.Others;
using DTO.ShopOrder;
using DTO.Suppliers;
using DTO.User;
using log4net;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using static DTO.Common.Enum;

namespace Service
{
    public class ShopOrderService
    {
        private static ILog log = LogManager.GetLogger(typeof(ShopOrderService));
        private SortedList<string, le_sysconfig> GetSysConfigList = SysConfig.Get().values;
        string BasePath = SysConfig.Get().values.Values.Where(s => s.Name == "HeadQuartersDomain").FirstOrDefault().Value;
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <param name="GoodValueID"></param>
        /// <param name="GoodsCount"></param>
        /// <param name="Mes"></param>
        /// <param name="cumulation">是否累加</param>
        /// <returns></returns>
        public int AddCart(int GoodsID, List<AddGoodsValues> GoodValueID, int GoodsCount, int UserID, bool cumulation, out string Mes, int? ReturnCount = 1)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    bool IsAdd = true;
                    le_shop_cart ShopCarModel = new le_shop_cart();
                    var UserCartList = ctx.le_shop_cart.Where(s => s.GoodsID == GoodsID && s.UserID == UserID).ToList();
                    foreach (var CartModel in UserCartList)
                    {
                        var existGoodsValue = CartModel
                        .le_cart_goodsvalue.Select(k => new AddGoodsValues { CategoryType = k.CategoryType, GoodsValueID = k.GoodsValueID }).ToList();
                        if (existGoodsValue != null)
                        {
                            var isExit = IsEqual(existGoodsValue, GoodValueID);
                            if (isExit)
                            {
                                ShopCarModel = CartModel;
                                IsAdd = false;
                                break;
                            }
                        }
                    }

                    var ListGoodValueID = GoodValueID.Select(s => s.GoodsValueID).ToList();

                    //var GoodsValuePrice = GoodValueMaping[0];
                    var GoodsModel = ctx.le_goods.Where(s => s.GoodsID == GoodsID).Select(s => new
                    {
                        s.SpecialOffer,
                        s.Stock,
                        s.Quota
                    }).FirstOrDefault();

                    //增加
                    if (IsAdd)
                    {

                        le_shop_cart model = new le_shop_cart();
                        model.Createtime = DateTime.Now;
                        model.GoodsCount = GoodsCount;
                        model.GoodsID = GoodsID;
                        model.Price = GoodsModel.SpecialOffer;
                        model.UserID = UserID;
                        model.ReturnCount = ReturnCount.Value;
                        foreach (var inde in GoodValueID)
                        {
                            le_cart_goodsvalue le_Cart_Goodsvalue = new le_cart_goodsvalue();
                            le_Cart_Goodsvalue.GoodsValueID = inde.GoodsValueID;
                            le_Cart_Goodsvalue.CategoryType = inde.CategoryType;
                            model.le_cart_goodsvalue.Add(le_Cart_Goodsvalue);
                        }
                        if (GoodsModel.Quota != -1 && model.GoodsCount > GoodsModel.Quota)
                        {
                            Mes = string.Format("该商品每人限购{0}件", GoodsModel.Quota);
                            return 0;
                        }
                        ctx.le_shop_cart.Add(model);
                        if (ctx.SaveChanges() > 0)
                        {
                            Mes = "SUCCESS";
                            return model.CartID;
                        }
                    }
                    //修改
                    else
                    {

                        if (cumulation)//是否累加
                        {
                            ShopCarModel.GoodsCount += GoodsCount;
                            ShopCarModel.ReturnCount = ReturnCount.Value;
                        }
                        else
                        {
                            ShopCarModel.GoodsCount = GoodsCount;
                            ShopCarModel.ReturnCount = ReturnCount.Value;
                        }
                        if (GoodsModel.Quota != -1 && ShopCarModel.GoodsCount > GoodsModel.Quota)
                        {
                            Mes = string.Format("该商品每人限购{0}件", GoodsModel.Quota);
                            return 0;
                        }
                        ctx.Entry<le_shop_cart>(ShopCarModel).State = EntityState.Modified;
                        if (ctx.SaveChanges() > 0)
                        {
                            Mes = "SUCCESS";
                            return ShopCarModel.CartID;
                        }
                        else
                        {
                            Mes = "修改失败";
                            return 0;
                        }
                    }
                    Mes = "操作失败";
                    return 0;
                }
                catch (Exception ex)
                {
                    log.Error(GoodsID, ex);
                }
            }
            Mes = "操作失败";
            return 0;
        }

        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="CartID"></param>
        /// <returns></returns>
        public bool DeleteCart(List<int> CartList, int? UserID)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_shop_cart.Where(s => CartList.Contains(s.CartID));
                List<le_shop_cart> modelList;
                if (UserID != null)
                {
                    modelList = tempIq.Where(s => s.UserID == UserID).ToList();
                }
                else
                {
                    modelList = tempIq.ToList();
                }
                if (modelList == null || modelList.Count <= 0)
                {
                    return false;
                }

                foreach (var model in modelList)
                {
                    var CartValue = model.le_cart_goodsvalue.ToList();
                    foreach (var index in CartValue)
                    {
                        ctx.Entry<le_cart_goodsvalue>(index).State = EntityState.Deleted;
                    }

                    ctx.le_shop_cart.Remove(model);
                }
                // ctx.le_shop_cart.RemoveRange(modelList);

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
        /// 获取购物车
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<ShopCartDto> GetCartList(int UserID)
        {
            using (Entities ctx = new Entities())
            {
                var result = ctx.le_shop_cart.Where(s => s.UserID == UserID).Select(s => new ShopCartDto
                {
                    CartID = s.CartID,
                    GoodsCount = s.GoodsCount,
                    GoodsID = s.GoodsID,
                    GoodsImg = BasePath + s.le_goods.Image,
                    GoodsName = s.le_goods.GoodsName,
                    //SuppliersID = s.le_goods.le_goods_suppliers.Where(k=>k.IsDefalut==1).FirstOrDefault().SuppliersID,
                    Price = s.le_goods.SpecialOffer,
                    Stock = s.le_goods.Stock,
                    Quota = s.le_goods.Quota,
                    MinimumPurchase = s.le_goods.MinimumPurchase,
                    RowVersion = s.le_goods.RowVersion,
                    //Supplyprice=s.le_goods.le_goods_suppliers.Where(k => k.IsDefalut == 1).FirstOrDefault().Supplyprice,
                    SpecialOffer = s.le_goods.SpecialOffer,
                    PackingNumber=s.le_goods.PackingNumber,
                    Discount=s.le_goods.Discount,
                    Integral=s.le_goods.Integral,
                    PriceFull=s.le_goods.PriceFull,
                    PriceReduction=s.le_goods.PriceReduction,
                    ReturnCount=s.ReturnCount,
                    GoodsValueList = s.le_cart_goodsvalue
                    .Select(k => new GoodsValues
                    {
                        SerialNumber = k.le_goods_value.SerialNumber,
                        CategoryType = k.CategoryType,
                        GoodsValueName = k.le_goods_value.GoodsValue,
                        GoodsValueID = k.GoodsValueID

                    }),
                    SupplierGoodsList = s.le_goods.le_goods_suppliers.Select(k => new SupplierGoods
                    {
                        IsDefalut = k.IsDefalut,
                        SupplierID = k.SuppliersID,
                        Price = k.Supplyprice
                    }).ToList(),
                    GoodsGroups_ID = s.le_goods.GoodsGroupsID,


                }).ToList();
                return result;
            }
            return null;
        }

        /// <summary>
        ///  保存订单
        /// </summary>
        /// <param name="orderGoodsList"></param>
        /// <param name="UserID"></param>
        /// <param name="AddressID"></param>
        /// <param name="Notes"></param>
        /// <param name="OrderType">订单类型/ 1订货单 2 退货单</param>
        /// <param name="ExpressType">快递类型/1快递物流 2 自提</param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public int OrderSave(OrderSaveParams ParamasData, out string Msg, out List<ShopCartDto> FailCartList)
        {
            Msg = "未知错误";
            using (Entities ctx = new Entities())
            {
                FailCartList = new List<ShopCartDto>();
                List<ShopCartDto> CartList = GetCartList(ParamasData.UserID);
                //  List<ShopCartDto> FialCartList = new List<ShopCartDto>();//下单失败的购物车对象

                if (CartList == null || CartList.Count == 0)
                {
                    Msg = "获取购物车失败";
                    FailCartList = null;
                    return 0;
                }

                AddressDto AddressModel = new AddressDto();
                if (ParamasData.ExpressType == 1)
                {                   
                    AddressModel = ctx.le_user_address.Where(s => s.AddressID == ParamasData.AddressID && s.UserID == ParamasData.UserID && s.Status == 1).
                        Select(k => new AddressDto
                        {
                            ReceiveAddress = k.ReceiveAddress,
                            ReceivePhone = k.ReceivePhone,
                            ReceiveArea = k.ReceiveArea,
                            ReceiveName = k.ReceiveName
                        }).
                        FirstOrDefault();
                    if (AddressModel == null)
                    {
                        Msg = "地址输入错误";
                        FailCartList = null;
                        return 0;
                    }
                  
                }
                if (ParamasData.ExpressType == 2)
                {
                    if (!ParamasData.PickupTime.HasValue)
                    {
                        Msg = "请选择下单时间";
                        FailCartList = null;
                        return 0;
                    }
                    AddressModel = ctx.le_sys_address.Where(s => s.AddressID == ParamasData.AddressID && s.Status == 1).
                     Select(k => new AddressDto
                     {
                         ReceiveAddress = k.ReceiveAddress,
                         ReceivePhone = k.ReceivePhone,
                         ReceiveArea = k.ReceiveArea,
                         ReceiveName = k.ReceiveName
                     }).FirstOrDefault();
                    if (AddressModel == null)
                    {
                        Msg = "地址输入错误";
                        FailCartList = null;
                        return 0;
                    }
                    //判断当前时间内的下单数
                   
                    string BeginStr = DateTime.Now.ToString("yyyy-MM-dd HH") + ":00:00";
                    string EndStr = DateTime.Now.ToString("yyyy-MM-dd HH") + ":59:59";

                    //DateTime BeginTime = Convert.ToDateTime(BeginStr);
                    //DateTime EndTime = Convert.ToDateTime(EndStr);

                    DateTime EndTime = ParamasData.PickupTime.Value.AddHours(1);
                    var hour = ParamasData.PickupTime.Value.Hour;
                    var CurrentTime = DateTime.Now;
                    //  var CurrentOrderCountSetting = ctx.le_orders_timelimit.Where(s => s.TimeSlot == hour).Select(s => s.LimitOrderCount).FirstOrDefault();

                    //var CurrentOrderCount = ctx.le_orders_head.Where( s => s.Status != 5 && s.Status != 1 && s.PickupTime >= ParamasData.PickupTime.Value && s.PickupTime <= EndTime).Count();
                    //if (CurrentOrderCountSetting <= CurrentOrderCount)
                    //{
                    //    Msg = "当前时间下单数已满,请选择其他时间";
                    //    //  log.Debug(Msg);
                    //    return 0;
                    //}
                }

                var QuotaGoodsList = CartList.GroupBy(s => s.GoodsID).Select(g => new { GoodsID = g.Key, GoodsCount = g.Sum(s => s.GoodsCount) });

                List<le_orders_lines> OrderLinesList = new List<le_orders_lines>();
                string Trade_no = Common.RandomUtils.GenerateOutTradeNo("LEL");
                List<GoodsStock> goodsStocksList = new List<GoodsStock>();

                le_orders_head le_Orders_Head = new le_orders_head();

                foreach (var goodsModel in CartList)
                {
                    var QuotaGoods = QuotaGoodsList.Where(s => s.GoodsID == goodsModel.GoodsID).FirstOrDefault();
                    int AlreadyBuyCount = 0;

                    //已经购买的数量
                    var BuyCountIquery = ctx.le_orders_lines.Where(s => s.UsersID == ParamasData.UserID && s.GoodsID == QuotaGoods.GoodsID).Select(k => new { k.GoodsCount }).ToList();
                    if (BuyCountIquery != null)
                    {
                        AlreadyBuyCount = BuyCountIquery.Sum(g => g.GoodsCount);
                    }

                    if ((QuotaGoods != null && goodsModel.Quota != -1 && goodsModel.Quota - AlreadyBuyCount <= 0) || (goodsModel.Quota - QuotaGoods.GoodsCount < 0 && goodsModel.Quota != -1))
                    {
                        Msg = string.Format("该商品没人限购{0}件", goodsModel.Quota);
                        log.Debug(Msg);
                        return 0;
                    }
                    if (goodsModel.Stock - QuotaGoods.GoodsCount < 0)
                    {
                        Msg = "库存不足，请稍后再试";
                        return 0;
                    }
                    var DefaulSuplier = goodsModel.SupplierGoodsList.Where(s => s.IsDefalut == 1).FirstOrDefault();
                    if (DefaulSuplier == null)
                    {
                        Msg = "该商品未设置商品默认供应商,下单失败.商品ID:" + goodsModel.GoodsID.ToString();
                        log.Debug(Msg);
                        return 0;
                    }
                    int OrderGoodsCount=0;//下单商品数量
                    if (ParamasData.OrderType==1|| ParamasData.OrderType==3)
                    {
                        OrderGoodsCount = goodsModel.GoodsCount;
                    }
                    if (ParamasData.OrderType ==2)
                    {
                        OrderGoodsCount = goodsModel.ReturnCount.Value;
                    }
                    GoodsStock goodsStock = new GoodsStock();
                    goodsStock.Stock = goodsModel.Stock;
                    goodsStock.RowVersion = goodsModel.RowVersion;
                    goodsStock.GoodsID = goodsModel.GoodsID;
                    goodsStock.GoodsCount = goodsModel.GoodsCount;
                    goodsStocksList.Add(goodsStock);

                    le_orders_lines linesModel = new le_orders_lines();
                    linesModel.CreateTime = DateTime.Now;
                    linesModel.GoodsCount = OrderGoodsCount;//goodsModel.GoodsCount;
                    linesModel.DeliverCount = OrderGoodsCount;// goodsModel.GoodsCount;
                    linesModel.GoodsPrice = goodsModel.SpecialOffer;
                    linesModel.SupplyPrice = DefaulSuplier.Price;
                    linesModel.Profit = goodsModel.Price - DefaulSuplier.Price;
                 
                    linesModel.Status = 0;
                    linesModel.UpdateTime = DateTime.Now;
                    linesModel.UsersID = ParamasData.UserID;
                    linesModel.SuppliersID = DefaulSuplier.SupplierID;
                    linesModel.GoodsID = goodsModel.GoodsID;
                    foreach (var GoodsValue in goodsModel.GoodsValueList)
                    {
                        le_orderline_goodsvalue linValue = new le_orderline_goodsvalue();
                        linValue.CategoryType = GoodsValue.CategoryType;
                        linValue.GoodsValueID = GoodsValue.GoodsValueID;
                        linesModel.le_orderline_goodsvalue.Add(linValue);
                    }
                    OrderLinesList.Add(linesModel);

                    le_Orders_Head.le_orders_lines.Add(linesModel);
                }

              

                le_Orders_Head.CreateTime = DateTime.Now;
                le_Orders_Head.OrderAmout = OrderLinesList.Sum(s => s.GoodsPrice * s.GoodsCount);
                le_Orders_Head.RealAmount = le_Orders_Head.OrderAmout;
                le_Orders_Head.OrderSupplyAmount = OrderLinesList.Sum(s => s.SupplyPrice * s.GoodsCount);
                le_Orders_Head.RealSupplyAmount = le_Orders_Head.OrderSupplyAmount;
                le_Orders_Head.OutTradeNo = Trade_no;
                le_Orders_Head.RcAddr = AddressModel.ReceiveArea + "-" + AddressModel.ReceiveAddress;
                le_Orders_Head.RcName = AddressModel.ReceiveName;
                le_Orders_Head.RcPhone = AddressModel.ReceivePhone;
                le_Orders_Head.Status = 0;
                le_Orders_Head.UsersID = ParamasData.UserID;
                le_Orders_Head.UpdateTime = DateTime.Now;
                le_Orders_Head.GoodsCount = OrderLinesList.Sum(s => s.GoodsCount);
                le_Orders_Head.DeliverCount = le_Orders_Head.GoodsCount;
                le_Orders_Head.Head_Notes = CartList[0].GoodsName + "$" + ParamasData.Notes;
                le_Orders_Head.OrderType = ParamasData.OrderType;
                le_Orders_Head.ExpressType = ParamasData.ExpressType;
                le_Orders_Head.CarNumber = ParamasData.CarNumber;
                le_Orders_Head.PickUpPhone = ParamasData.PickUpPhone;
                le_Orders_Head.PickupTime = ParamasData.PickupTime;
                le_Orders_Head.PickUpMan = ParamasData.PickUpMan;
                

                ctx.le_orders_head.Add(le_Orders_Head);

                //去重复
                var NORepeat = goodsStocksList.GroupBy(s => s.GoodsID).Select(g => new GoodsStock { GoodsID = g.Key, GoodsCount = g.Sum(s => s.GoodsCount), Stock = g.Max(k => k.Stock), RowVersion = g.Max(k => k.RowVersion) }).ToList();
                bool IsHaveStock = true;

                //退货单不计销量
                if (ParamasData.OrderType != 2)
                {
                    IsHaveStock = CheckStock(NORepeat);
                }
                if (!IsHaveStock)
                {

                    Msg = "访问人次太多,请稍后再试!";
                    log.Debug(Msg + goodsStocksList[0].GoodsID.ToString());
                    return 0;
                }

                try
                {
                    //删除购物车,待优化
                    var CartLists = ctx.le_shop_cart.Where(s => s.UserID == ParamasData.UserID).ToList();
                    foreach (var CartModel in CartLists)
                    {

                        var CartValue = CartModel.le_cart_goodsvalue.ToList();
                        foreach (var index in CartValue)
                        {
                            ctx.le_cart_goodsvalue.Remove(index);
                            //ctx.Entry<le_cart_goodsvalue>(index).State = EntityState.Deleted;
                        }
                        ctx.le_shop_cart.Remove(CartModel);
                    }

                    if (ctx.SaveChanges() > 0)
                    {
                        //用户下单，推送消息给所有总部人员
                        var AdminIDList = ctx.le_admin.Where(s => s.Status == 1).Select(s => s.AdminID).ToList();
                        foreach (var AdminId in AdminIDList)
                        {
                            new OtherService().UpdatePushMsg(AdminId, le_Orders_Head.OutTradeNo, 3);
                        }
                        Msg = "订单提交成功";
                        return le_Orders_Head.OrdersHeadID;
                    }
                    else
                    {
                        Msg = "订单提交失败";
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    var str = ExceptionHelper.GetInnerExceptionMsg(ex);

                    Msg = str;
                    log.Error(str, ex);

                    return 0;

                }
                Msg = "FAIL";
                return 0;

            }
            return 0;
        }

        /// <summary>
        /// 排序扩展
        /// DistinctBy不是.net framework提供的扩展方法，是第三方的扩展方法
        /// </summary>
        /// <typeparam name="t"></typeparam>
        /// <param name="list"></param>
        /// <param name="propertySelector"></param>
        /// <returns></returns>
        //public static IEnumerable<t> DistinctBy<t>(this IEnumerable<t> list, Func<t, object> propertySelector)
        //{
        //    return list.GroupBy(propertySelector).Select(x => x.First());

        //}
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
                var tempIq = ctx.le_orders_head.Where(s=>true);

                if (seachParams.BeginTime == null && seachParams.EndTime == null && string.IsNullOrEmpty(seachParams.Out_Trade_No))
                {

                    seachParams.BeginTime = DateTime.Now.AddDays(-3);
                    seachParams.EndTime = DateTime.Now;
                }

                if (seachParams.BeginTime!=null)
                {
                    tempIq = tempIq.Where(s => s.CreateTime >= seachParams.BeginTime);
                }
                if (seachParams.EndTime != null)
                {
                    tempIq = tempIq.Where(s => s.CreateTime <= seachParams.EndTime);
                }

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
                //if (seachParams.Status != null)
                //{
                //    tempIq = tempIq.Where(s => s.Status == seachParams.Status);
                //}
                if (seachParams.UserID != null)
                {
                    tempIq = tempIq.Where(s => s.UsersID == seachParams.UserID);
                }
                if (seachParams.OrderType != null)
                {
                    tempIq = tempIq.Where(s => s.OrderType == seachParams.OrderType);
                }
                if (seachParams.AdminID != null)
                {
                    tempIq = tempIq.Where(s => s.AdminID == seachParams.AdminID);
                }
                if (seachParams.ExpressType != null)
                {
                    tempIq = tempIq.Where(s => s.ExpressType == seachParams.ExpressType);
                }
                var result = tempIq.Join(ctx.le_admin_re_users, o => o.UsersID, p => p.UserID, (p, o) => new OrderDto
                {
                    AdminID = p.AdminID,
                    AdminName = p.le_admin.Nickname,
                    CompleteTime = p.CompleteTime,
                    CreateTime = p.CreateTime,
                    Head_Notes = p.Head_Notes,
                    LinesCount = p.GoodsCount,

                    RealAmount = p.RealAmount,
                    OrderAmout=p.OrderAmout,

                    OrderSupplyAmount = p.OrderSupplyAmount,
                    RealSupplyAmount=p.RealSupplyAmount,
                   

                    Orders_Head_ID = p.OrdersHeadID,
                    Out_Trade_No = p.OutTradeNo,
                    RcAddr = p.RcAddr,
                    Status = p.Status,
                    RcName = p.RcName,
                    RcPhone = p.RcPhone,
                    UpdateTime = p.UpdateTime,
                    UserName = p.le_users.UsersNickname,
                    UsersID = p.UsersID,
                    ExpressType = p.ExpressType,
                    OrderType = p.OrderType,
                    CarNumber = p.CarNumber,
                    PickUpMan = p.PickUpMan,
                    PickupTime = p.PickupTime,
                    PickUpPhone = p.PickUpPhone,
                 
                    DeliverCount = p.DeliverCount,

                }).Distinct();

                //var result = tempIq.Select(s => new OrderDto
                //{                   
                //    AdminID = s.AdminID,
                //    AdminName=s.le_admin.Nickname,                   
                //    CompleteTime = s.CompleteTime,
                //    CreateTime = s.CreateTime,
                //    Head_Notes = s.Head_Notes,
                //    LinesCount = s.GoodsCount,
                //    Money = s.Money,
                //    Orders_Head_ID = s.OrdersHeadID,
                //    Out_Trade_No = s.OutTradeNo,
                //    RcAddr = s.RcAddr,
                //    Status = s.Status,
                //    RcName = s.RcName,
                //    RcPhone = s.RcPhone,
                //    UpdateTime = s.UpdateTime,
                //    UserName = s.le_users.UsersNickname,
                //    UsersID = s.UsersID,
                //    ExpressType=s.ExpressType,
                //    OrderType=s.OrderType,
                //    CarNumber=s.CarNumber,
                //    PickUpMan=s.PickUpMan,
                //    PickupTime=s.PickupTime,
                //    PickUpPhone=s.PickUpPhone,
                //    SupplyMoney=s.SupplyMoney,
                //    DeliverCount=s.DeliverCount,

                //}
                //);

                result = result.OrderByDescending(s => s.CreateTime);
                Count = result.Count();
                result = result.Skip(seachParams.Offset).Take(seachParams.Rows);

                return result.ToList();
            }
        }

        #region 向原生sql低头
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="seachParams"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        //public List<OrderDto> GetListOrderforSuppier(OrderSeachParams seachParams, out int Count)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        var tempIq = ctx.le_orders_lines.Join(ctx.le_orders_head, a => a.OutTradeNo, b => b.OutTradeNo,
        //            (a, b) =>
        //                new OrderDto
        //                {
        //                    AdminID = b.AdminID,
        //                    CompleteTime = b.CompleteTime,
        //                    CreateTime = a.CreateTime,
        //                    Head_Notes = b.Head_Notes,
        //                    LinesCount = b.LinesCount,
        //                    Money = b.Money,
        //                    Orders_Head_ID = b.Orders_Head_ID,
        //                    OutTradeNo = b.OutTradeNo,
        //                    RcAddr = b.RcAddr,
        //                    Status = a.Status,
        //                    RcName = b.RcName,
        //                    RcPhone = b.RcPhone,
        //                    UpdateTime = b.UpdateTime,
        //                    //UserName = b.le_users.UsersNickname,
        //                    UsersID = b.UsersID,
        //                    ExpressType = b.ExpressType,
        //                    OrderType = b.OrderType,
        //                    SupplierID = a.SuppliersID,
        //                    CarNumber = b.CarNumber,
        //                    PickUpMan = b.PickUpMan,
        //                    PickupTime = b.PickupTime,
        //                    PickUpPhone = b.PickUpPhone,
        //                });

        //        tempIq = tempIq.Where(s => s.CreateTime > seachParams.BeginTime && s.CreateTime <= seachParams.EndTime);
        //        if (seachParams.Status != null)
        //        {
        //            tempIq = tempIq.Where(s => s.Status == seachParams.Status);
        //        }
        //        if (!string.IsNullOrEmpty(seachParams.KeyWords))
        //        {
        //            tempIq = tempIq.Where(s => s.RcPhone.Contains(seachParams.KeyWords)
        //              || s.Head_Notes.Contains(seachParams.KeyWords)
        //              || s.RcAddr.Contains(seachParams.KeyWords)
        //            );
        //        }
        //        if (!string.IsNullOrEmpty(seachParams.OutTradeNo))
        //        {
        //            tempIq = tempIq.Where(s => s.OutTradeNo == seachParams.OutTradeNo);
        //        }
        //        if (seachParams.Status != null)
        //        {
        //            tempIq = tempIq.Where(s => s.Status == seachParams.Status);
        //        }
        //        if(seachParams.Status == null)
        //        {
        //            tempIq = tempIq.Where(s => s.Status !=0);
        //        }
        //        if (seachParams.UserID != null)
        //        {
        //            tempIq = tempIq.Where(s => s.UsersID == seachParams.UserID);
        //        }
        //        if (seachParams.SupplierID != null)
        //        {
        //            tempIq = tempIq.Where(s => s.SupplierID == seachParams.SupplierID);
        //        }
        //        if(seachParams.OrderType!=null)
        //        {
        //            tempIq = tempIq.Where(s => s.OrderType == seachParams.OrderType);
        //        }
        //        //.OrderByDescending(s => s.CreateTime).Skip(seachParams.Offset).Take(seachParams.Rows)

        //        var tempIGroup = tempIq.GroupBy(s => s.OutTradeNo);//.ToList();
        //        //var aa = tempIGroup.ToList();
        //        Count = tempIGroup.Count();
        //        var result = tempIGroup.Select(g => g.FirstOrDefault()).OrderByDescending(s => s.CreateTime).Skip(seachParams.Offset).Take(seachParams.Rows);

        //        return result.ToList();
        //    }
        //}
        #endregion


        /// <summary>
        /// 获取订单详细
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public List<OrderDetail> GetOrderDetails(string OrderNo, int SupperID = 0)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_orders_lines
                    .Where(s => s.le_orders_head.OutTradeNo == OrderNo);
                if (SupperID != 0)
                {
                    tempIq = tempIq.Where(s => s.SuppliersID == SupperID);
                }
                var result = tempIq.Select(s => new OrderDetail
                {
                    SupplierID = s.SuppliersID,
                    GoodsCount = s.GoodsCount,
                    GoodsID = s.GoodsID,
                    GoodsImg = BasePath + s.le_goods.Image,
                    GoodsName = s.le_goods.GoodsName,
                    Notes = s.Notes,
                    SpecialOffer = s.le_goods.SpecialOffer,
                    Discount=s.le_goods.Discount,
                    PriceReduction=s.le_goods.PriceReduction,
                    PriceFull=s.le_goods.PriceFull,
                    Integral=s.le_goods.Integral,
                 
                    Status = s.Status,
                    SuppliersName = s.le_suppliers.SuppliersName,
                    GoodsValuesList = s.le_orderline_goodsvalue.Select(k => new GoodsValues()
                    {
                        SerialNumber = k.le_goods_value.SerialNumber,
                        CategoryType = k.CategoryType,
                        GoodsValueID = k.GoodsValueID,
                        GoodsValueName = k.le_goods_value.GoodsValue
                    }).ToList(),
                    Orders_Lines_ID = s.OrdersLinesID,
                    SupplierPhone = s.le_suppliers.MobilePhone,
                    SupplyPrice = s.SupplyPrice,
                    GoodsPrice = s.GoodsPrice,
                    OriginalPrice = s.le_goods.OriginalPrice,
                    Specifications = s.le_goods.Specifications,
                    PackingNumber = s.le_goods.PackingNumber,
                    DeliverCount = s.DeliverCount,
                    MinimumPurchase=s.le_goods.MinimumPurchase
                }).OrderBy(s=>s.SupplierID).OrderBy(s=>s.GoodsID);

                return result.ToList();
            }
            return null;
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
        /// 修改订单备注信息
        /// </summary>
        /// <param name="OutTradeNo"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool EditOrderHead_Notes(string OutTradeNo, string Head_Notes, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                msg = "";
                try
                {
                    var model = ctx.le_orders_head.Where(s => s.OutTradeNo == OutTradeNo).FirstOrDefault();

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
        public bool BatchEditLinesInfo(List<EditLinesInfo> List, LoginInfo info, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                msg = "";
                if (List.Count <= 0)
                {
                    msg = "请确认需要编辑的信息";
                    return false;
                }
                int OrderHeadID = List[0].OrderHeadID;
                List<le_orders_lines> OrderLineList = new List<le_orders_lines>();
                le_orders_head modhead = ctx.le_orders_head.Where(s => s.OrdersHeadID == OrderHeadID).FirstOrDefault();
                if (modhead == null)
                {
                    msg = "单号不存在,请输入正确的单号";
                    return false;
                }

                le_orders_head_log OrderHeadLogModel = new le_orders_head_log();
                if (info.UserType == 3)
                {
                    OrderHeadLogModel.AdminID = info.UserID;
                }
                if (info.UserType == 2)
                {
                    OrderHeadLogModel.SupplierID = info.UserID;
                }
                if (info.UserType == 1)
                {
                    OrderHeadLogModel.UserID = info.UserID;
                }
                OrderHeadLogModel.BeforeCount = modhead.GoodsCount;
                OrderHeadLogModel.AfterCount= modhead.GoodsCount;

                OrderHeadLogModel.BeforeMoney = modhead.RealAmount;
                OrderHeadLogModel.AfterMoney = modhead.RealAmount;

                OrderHeadLogModel.BeforeStatus = modhead.Status;
                OrderHeadLogModel.AfterStatus = modhead.Status;

                OrderHeadLogModel.CreateTime = DateTime.Now;
                OrderHeadLogModel.OrderHeadID = modhead.OrdersHeadID;
               
                try
                {
                    foreach (var data in List)
                    {

                        decimal SupplyPrice;//供货价格
                        var Linemodel = ctx.le_orders_lines.Where(s => s.OrdersLinesID == data.Orders_Lines_ID).FirstOrDefault();
                       
                        le_orders_lines_log OrderLineLogModel = new le_orders_lines_log();
                        if (info.UserType == 3)
                        {
                            OrderLineLogModel.AdminID = info.UserID;
                        }
                        if (info.UserType == 2)
                        {
                            data.SuppliersID = info.UserID;
                            OrderLineLogModel.SupplierID = info.UserID;
                        }
                        if (info.UserType == 1)
                        {
                            OrderLineLogModel.UserID = info.UserID;
                        }
                        OrderLineLogModel.BeforeCount = Linemodel.GoodsCount;
                        OrderLineLogModel.BeforeMoney = Linemodel.GoodsPrice;
                        OrderLineLogModel.BeforeStatus = Linemodel.Status;
                        OrderLineLogModel.CreateTime = DateTime.Now;
                        OrderLineLogModel.OrderLineID = Linemodel.OrdersLinesID;

                        if (Linemodel == null)
                        {
                            msg = data.Orders_Lines_ID + "订单不存在，请确认后重试";
                            return false;
                        }
                        if (!string.IsNullOrEmpty(data.Notes))
                        {
                            Linemodel.Notes = data.Notes;
                        }
                        if (Linemodel.SuppliersID != data.SuppliersID) //更改供应商
                        {
                            SupplyPrice = ctx.le_goods_suppliers.Where(s => s.SuppliersID == data.SuppliersID && s.GoodsID == Linemodel.GoodsID).Select(s => s.Supplyprice).FirstOrDefault();
                            Linemodel.SuppliersID = data.SuppliersID;
                            Linemodel.SupplyPrice = SupplyPrice;
                            Linemodel.Profit = Linemodel.GoodsPrice - SupplyPrice;
                        }
                        if (Linemodel.DeliverCount != data.GoodsCount)//更改商品数量
                        {

                            ///更改订单数量 更新商品库存                         
                            Linemodel.le_goods.Stock += (Linemodel.DeliverCount - data.GoodsCount);
                            Linemodel.le_goods.SalesVolumes -= (Linemodel.DeliverCount - data.GoodsCount);
                            Linemodel.le_goods.TotalSalesVolume -= (Linemodel.DeliverCount - data.GoodsCount);

                            Linemodel.DeliverCount = data.GoodsCount;
                            if (Linemodel.DeliverCount <= 0)
                            {
                                Linemodel.DeliverCount = 0;
                            }

                            OrderLineLogModel.AfterCount = Linemodel.DeliverCount;
                            OrderLineLogModel.AfterMoney = Linemodel.GoodsPrice;
                            OrderLineLogModel.AfterStatus = Linemodel.Status;

                            if(OrderLineLogModel.AfterCount != OrderLineLogModel.BeforeCount|| OrderLineLogModel.BeforeMoney!= OrderLineLogModel.AfterMoney)
                            {
                                ctx.le_orders_lines_log.Add(OrderLineLogModel);
                            }
                                          
                           
                        }
                        OrderLineList.Add(Linemodel);

                        ctx.Entry<le_orders_lines>(Linemodel).State = EntityState.Modified;

                    }

                    modhead.DeliverCount = OrderLineList.Sum(s => s.DeliverCount);
                    modhead.RealSupplyAmount = OrderLineList.Sum(s => s.SupplyPrice * s.DeliverCount);
                    modhead.RealAmount = OrderLineList.Sum(s => s.GoodsPrice * s.DeliverCount);

                    if (modhead.DeliverCount < 0)
                    {
                        modhead.DeliverCount = 0;
                    }
                    OrderHeadLogModel.AfterCount = modhead.DeliverCount;
                    OrderHeadLogModel.AfterMoney = modhead.RealAmount;
                    OrderHeadLogModel.AfterStatus = modhead.Status;

                    if (OrderHeadLogModel.AfterCount != OrderHeadLogModel.BeforeCount || OrderHeadLogModel.BeforeMoney != OrderHeadLogModel.BeforeMoney)
                    {
                        ctx.le_orders_head_log.Add(OrderHeadLogModel);
                    }
                   
                    ctx.Entry<le_orders_head>(modhead).State = EntityState.Modified;
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
                    log.Error(ex.Message, ex);
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
//        public List<GoodsSuppliersInfoDto> GetGoodsSuppliersList(int GoodsID, out string msg)
//        {
//            using (Entities ctx = new Entities())
//            {
//                try
//                {
//                    string sql = string.Format(@"select lg.GoodsID,lg.GoodsName,ls.Suppliers_Name,lgs.Price,ls.SuppliersID from le_suppliers ls 
//left join le_goods_suppliers lgs on lgs.SuppliersID = ls.SuppliersID
//left join le_goods lg on lg.GoodsID=lgs.GoodsID
//where lg.GoodsID=@GoodsID");

//                    MySqlParameter[] parameters =
//                    {
//                    new MySqlParameter("@GoodsID", MySqlDbType.Int32),
//                    };
//                    parameters[0].Value = GoodsID;

//                    var resule = ctx.Database.SqlQuery<GoodsSuppliersInfoDto>(sql, parameters).ToList();
//                    msg = "SUCCESS";
//                    return resule;
//                }
//                catch (Exception ex)
//                {
//                    msg = "查询异常，信息：" + ex.ToString();
//                    return null;
//                }
//            }
//        }

        /// <summary>
        /// 用户取消订单
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        //public bool UserCancelOrder(int UserID, string OrderNo)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        var orderModel = ctx.le_orders_head.Where(s => s.UsersID == UserID && s.OutTradeNo == OrderNo).FirstOrDefault();
        //        if (orderModel == null)
        //        {
        //            return false;
        //        }
        //        if (orderModel.Status != 0)
        //        {
        //            return false;
        //        }
        //        orderModel.Status = 5;
        //        ctx.Entry<le_orders_head>(orderModel).State = EntityState.Modified;
        //        if (ctx.SaveChanges() > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //}

        /// <summary>
        /// 获取订单行列表
        /// </summary>
        /// <param name="SeachOptions"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<OrderLineDto> GetOrderlineList(OrderLineSeachParames SeachOptions, out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_orders_lines.Where(s => true);
                if (SeachOptions.AdminID != null)
                {
                    tempIq = tempIq.Where(s => s.AdminID == SeachOptions.AdminID.Value);
                }
                if (!string.IsNullOrEmpty(SeachOptions.OrderNo))
                {
                    tempIq = tempIq.Where(s => s.le_orders_head.OutTradeNo == SeachOptions.OrderNo);
                }
                if (SeachOptions.BeginTime != null)
                {
                    tempIq = tempIq.Where(s => s.CreateTime >= SeachOptions.BeginTime);
                }
                if (SeachOptions.EndTinme != null)
                {
                    tempIq = tempIq.Where(s => s.CreateTime <= SeachOptions.EndTinme);
                }
                if (SeachOptions.UserID != null)
                {
                    tempIq = tempIq.Where(s => s.UsersID == SeachOptions.UserID.Value);
                }
                if (SeachOptions.SuppliersID != null)
                {
                    tempIq = tempIq.Where(s => s.SuppliersID == SeachOptions.SuppliersID.Value);
                }
                if (SeachOptions.OrderType != null)
                {
                    tempIq = tempIq.Where(s => s.le_orders_head.OrderType == SeachOptions.OrderType);
                }
                if (SeachOptions.Status != null)
                {
                    tempIq = tempIq.Where(s => s.Status == SeachOptions.Status.Value);
                }
                else
                {
                    tempIq = tempIq.Where(s => s.Status != 0);
                }
                if (!string.IsNullOrEmpty(SeachOptions.KeyWords))
                {
                    tempIq = tempIq.Where(s => s.le_goods.GoodsName.Contains(SeachOptions.KeyWords));
                }
                var result = tempIq.Select(s => new OrderLineDto
                {
                    AdminName = s.le_admin.Nickname,
                    AdminTelPhone = s.le_admin.TelePhone,
                    AdminID = s.AdminID,
                    OrderHeadID = s.OrderHeadID,
                    CreateTime = s.CreateTime,
                    GoodsCount = s.GoodsCount,
                    DeliverCount = s.DeliverCount,
                    GoodsImage = BasePath + s.le_goods.Image,
                    GoodsName = s.le_goods.GoodsName,
                    Goods_ID = s.GoodsID,
                    //RcName = s.le_orders_head.RcName,
                    //RcPhone = s.le_orders_head.RcName,
                    SupplyMoney = s.SupplyPrice,
                    Notes = s.Notes,
                    OrderLineID = s.OrdersLinesID,
                    Status1 = s.Status,
                    Status2 = s.Status,
                    Status3 = s.Status,
                    SuppliersID = s.SuppliersID,
                    UpdateTime = s.UpdateTime,
                    UsersID = s.UsersID,
                    OrderType = s.le_orders_head.OrderType,
                    Out_Trade_No = s.le_orders_head.OutTradeNo,
                    PickupTime = s.le_orders_head.PickupTime,

                });
                result = result.OrderByDescending(s => s.CreateTime);
                var GroupResult = result.GroupBy(k => k.OrderHeadID).Select(k => new OrderLineDto
                {
                    AdminName = k.Max(p => p.AdminName),
                    AdminTelPhone = k.Max(p => p.AdminTelPhone),
                    AdminID = k.Max(p => p.AdminID),
                    OrderHeadID = k.Max(p => p.OrderHeadID),
                    CreateTime = k.Max(p => p.CreateTime),
                    GoodsCount = k.Sum(p => p.GoodsCount),
                    DeliverCount = k.Sum(p => p.DeliverCount),
                    GoodsImage = k.Max(p => p.GoodsImage),
                    GoodsName = k.Max(p => p.GoodsName),
                    Goods_ID = k.Max(p => p.Goods_ID),
                    //RcName = k.Max(p => p.RcName),
                    //RcPhone = k.Max(p => p.RcPhone),
                    SupplyMoney = k.Sum(p => p.SupplyMoney * p.DeliverCount),
                    Notes = k.Max(p => p.Notes),
                    OrderLineID = k.Max(p => p.OrderLineID),
                    Status1 = k.Min(p => p.Status1),
                    Status2 = k.Max(p => p.Status1) - 1 == 0 ? 1 : 1,
                    Status3 = k.Max(p => p.Status1),
                    SuppliersID = k.Max(p => p.SuppliersID),
                    UpdateTime = k.Max(p => p.UpdateTime),
                    UsersID = k.Max(p => p.UsersID),

                    OrderType = k.Max(p => p.OrderType),

                    Out_Trade_No = k.Max(p => p.Out_Trade_No),
                    PickupTime = k.Max(p => p.PickupTime),
                });
                GroupResult = GroupResult.OrderByDescending(s => s.CreateTime);
                Count = GroupResult.Count();
                GroupResult = GroupResult.Skip(SeachOptions.Offset).Take(SeachOptions.Rows);
                var filter = GroupResult.ToList();
                List<OrderLineDto> ResultList = new List<OrderLineDto>();
                foreach (var index in filter)
                {
                    if (index.Status1 == index.Status3)
                    {
                        index.Status2 = index.Status1;
                    }
                    ResultList.Add(index);
                }

                //if(filter.Count==2)
                //{
                //    foreach (var index in filter)
                //    {
                //        if (index.Status1 == index.Status3)
                //        {
                //            index.Status2 = index.Status1;
                //        }
                //        ResultList.Add(index);
                //    }
                //}
                return ResultList;
            }
        }

        /// <summary>
        /// 修改订单行状态  待优化
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="OrdersLinesID"></param>
        /// <param name="Notes"></param>
        /// <param name="Msg"></param>
        /// <param name="AdminID"></param>
        /// <param name="SuppliersID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool UpdateOrderLineStatus(List<UpdateOrderLineParamas> OrderLineList, out string Msg, int AdminID = 0, int SuppliersID = 0)
        {
          
            using (Entities ctx = new Entities())
            {
                var OrderNo = OrderLineList.Select(s => s.OrderNo).FirstOrDefault();
                var OrderHeadModel = ctx.le_orders_head.Where(s => s.OutTradeNo == OrderNo).FirstOrDefault();
                OrderLineStatus Status=OrderLineStatus.WeiPaiFa;
                int OrderLinesId = 0;
                if (OrderHeadModel == null)
                {
                    Msg = "订单不存在";
                    return false;
                }

                if (OrderHeadModel.Status == 5)
                {
                    Msg = "订单已被取消,操作失败";
                    return false;
                }
               
                if (OrderHeadModel.Status == 1)
                {
                    Msg = "订单已完成,操作失败";
                    return false;
                }
                try
                {
                    foreach (var IndexLine in OrderLineList)
                    {
                        if (AdminID != 0) //总部操作
                        {
                            if (IndexLine.SuppliersID == 0)
                            {
                                Msg = "供应商ID错误,订单行ID"+ IndexLine.OrderLineID;
                                return false;
                            }
                            SuppliersID = IndexLine.SuppliersID;
                        }

                        OrderLinesId = IndexLine.OrderLineID;
                        Status =(OrderLineStatus)IndexLine.Status;
                        if (Status == OrderLineStatus.DaiJieDan && AdminID == 0)
                        {                  
                            Msg = "非法操作,无权限.只有总部人员可派单";
                            return false;
                        }
                        if (!ctx.le_admin_re_users.Any(s => s.AdminID == AdminID && s.UserID == OrderHeadModel.UsersID)&& AdminID!=0)
                        {
                            Msg = "非法操作,无操作该门店订单权限";
                            return false;
                        }                
                        var LinesList = OrderHeadModel.le_orders_lines.ToList();                
                        //var CurrentDaiJieDanCount = LinesList.Count(s => s.Status == 1); //待接单
                        //var CurrentYiJieDanCount = LinesList.Count(s => s.Status == 2);//已结单
                        var CurrentYiQuXiaoCount= LinesList.Count(s => s.Status == 3);//已取消

                        var CurrentLine = LinesList.Where(s => s.OrdersLinesID == IndexLine.OrderLineID).FirstOrDefault();
                        var CurrentLineStatue = (OrderLineStatus)CurrentLine.Status;
                        if (CurrentLine.DeliverCount <= 0)
                        {
                            Msg = "操作失败,必须商品数必须大于0";
                            return false;
                        }
                        //if (CurrentLine.Status == 2 || CurrentLine.Status == 3|| Status==OrderLineStatus.DaiJieDan)
                        //{
                        //    Msg = "订单已经运行,无法修改派单";
                        //    return false;
                        //}
                        if (SuppliersID != 0 && CurrentLine.SuppliersID != SuppliersID && AdminID == 0)
                        {
                            Msg = "非法操作,无权限";
                            return false;
                        }

                        le_orders_lines_log OrderLineLog = new le_orders_lines_log();
                        OrderLineLog.BeforeCount = CurrentLine.GoodsCount;
                        OrderLineLog.BeforeMoney = CurrentLine.GoodsPrice;
                        OrderLineLog.BeforeStatus = CurrentLine.Status;
                        OrderLineLog.CreateTime = DateTime.Now;

                        le_orders_head_log OrderHeadLog = new le_orders_head_log();
                        OrderHeadLog.BeforeCount = OrderHeadModel.GoodsCount;
                        OrderHeadLog.BeforeMoney = OrderHeadModel.RealAmount;
                        OrderHeadLog.AfterMoney = OrderHeadModel.RealAmount; //比对
                        OrderHeadLog.BeforeStatus = OrderHeadModel.Status;
                        OrderHeadLog.CreateTime = DateTime.Now;
                        OrderHeadLog.OrderHeadID = OrderHeadModel.OrdersHeadID;
                        if (AdminID != 0)
                        {
                            OrderHeadLog.AdminID = AdminID;
                        }
                        if (SuppliersID != 0)
                        {
                            OrderHeadLog.SupplierID = SuppliersID ;
                        }
                        if (Status != OrderLineStatus.YiQuXiao) //现在的状态不是已取消
                        {
                            if (CurrentLineStatue == OrderLineStatus.YiQuXiao) //取消的订单重新操作。 修改销量和库存
                            {
                                CurrentLine.le_goods.SalesVolumes += CurrentLine.DeliverCount;
                                CurrentLine.le_goods.TotalSalesVolume += CurrentLine.DeliverCount;
                                CurrentLine.le_goods.Stock -= CurrentLine.DeliverCount;

                                OrderHeadModel.RealAmount += CurrentLine.DeliverCount * CurrentLine.GoodsPrice;
                                OrderHeadModel.RealSupplyAmount += CurrentLine.DeliverCount * CurrentLine.SupplyPrice;
                                OrderHeadModel.DeliverCount += CurrentLine.DeliverCount;
                            }
                        }
                        if (!string.IsNullOrEmpty(IndexLine.Notes))
                        {
                            CurrentLine.Notes = IndexLine.Notes;
                        }
                       
                        switch (Status)
                        {
                            case OrderLineStatus.DaiJieDan: //待接单            
                                CurrentLine.Status = 1;

                                OrderHeadModel.AdminID = AdminID;
                                CurrentLine.AdminID = AdminID;
                                CurrentLine.SuppliersID = SuppliersID;
                                CurrentLine.UpdateTime = DateTime.Now;

                                if (LinesList.Count(s => s.Status == 1) == LinesList.Count() - CurrentYiQuXiaoCount) //全部派单 更新订单头状态为待接单
                                {
                                    OrderHeadModel.Status = (int)OrderStatus.DaiJieDan;//修改订单头状态为待接单
                                    OrderHeadLog.AfterStatus = 3;
                                }
                                new OtherService().UpdatePushMsg(SuppliersID, OrderHeadModel.OutTradeNo, 2);

                                OrderLineLog.AdminID = AdminID;

                                break;

                            case OrderLineStatus.YiJieDan: //已结单
                                CurrentLine.Status = 2;

                                if (AdminID != 0)  //总部加急单也有接单权限
                                {
                                    OrderLineLog.AdminID = AdminID;
                                }
                                if (AdminID == 0 && SuppliersID != 0)
                                {
                                    OrderLineLog.SupplierID = SuppliersID;
                                }

                                //var IsComplete1 = LinesList.Any(s => s.Status == 2 && s.Status != 3 && s.OrdersLinesID != CurrentLine.OrdersLinesID);

                                if (LinesList.Count(s => s.Status == 2) == LinesList.Count() - CurrentYiQuXiaoCount) //全部已接单,更新订单头状态
                                {
                                    OrderHeadModel.Status = 4;//修改订单头状态为已结单
                                    OrderHeadLog.AfterStatus = 4;
                                }
                                //供货商完成接单 推送消息给总部
                                new OtherService().UpdatePushMsg(CurrentLine.AdminID.Value, OrderHeadModel.OutTradeNo, 3);

                                break;

                            case OrderLineStatus.YiQuXiao:
                                CurrentLine.Status = 3;
                                OrderLineLog.SupplierID = SuppliersID;
                                CurrentLine.le_goods.Stock += CurrentLine.DeliverCount;
                                CurrentLine.le_goods.SalesVolumes -= CurrentLine.DeliverCount;
                                CurrentLine.le_goods.TotalSalesVolume -= CurrentLine.DeliverCount;

                                OrderHeadModel.RealAmount -= CurrentLine.DeliverCount * CurrentLine.GoodsPrice;
                                OrderHeadModel.RealSupplyAmount -= CurrentLine.DeliverCount * CurrentLine.SupplyPrice;
                                OrderHeadModel.DeliverCount -= CurrentLine.DeliverCount;

                                if (CurrentYiQuXiaoCount == LinesList.Count()) //全部已取消,更新订单头状态
                                {
                                    OrderHeadModel.Status = 5;//修改订单头状态为已结单
                                    OrderHeadLog.AfterStatus = 5;
                                }

                                new OtherService().UpdatePushMsg(CurrentLine.AdminID.Value, OrderHeadModel.OutTradeNo, 3);
                                new OtherService().UpdatePushMsg(CurrentLine.SuppliersID, OrderHeadModel.OutTradeNo, 2);

                                CurrentLine.DeliverCount = 0;

                                break;
                        }
                        #region 即将废弃代码
                        //if (Status == 1 && AdminID != 0) //派发订单只有总部有权限
                        //{
                        //    if (CurrentLine.Status == 3) //取消的订单重新派单。 修改销量和库存
                        //    {
                        //        CurrentLine.le_goods.SalesVolumes += CurrentLine.GoodsCount;
                        //        CurrentLine.le_goods.TotalSalesVolume += CurrentLine.GoodsCount;
                        //        CurrentLine.le_goods.Stock -= CurrentLine.GoodsCount;

                        //        OrderHeadModel.RealAmount += CurrentLine.GoodsCount * CurrentLine.GoodsPrice;
                        //        OrderHeadModel.RealSupplyAmount+= CurrentLine.GoodsCount * CurrentLine.SupplyPrice;
                        //        OrderHeadModel.DeliverCount += CurrentLine.GoodsCount;
                        //        OrderHeadLog.AfterMoney = OrderHeadModel.RealAmount;
                        //        OrderHeadLog.AfterCount = OrderHeadModel.DeliverCount;
                        //    }
                        //    OrderLineLog.AdminID = AdminID;

                        //    if (Status == 2 || Status == 3)
                        //    {
                        //        Msg = "订单已经运行,无法修改派单";
                        //        return false;
                        //    }

                        //    CurrentLine.AdminID = AdminID;
                        //    CurrentLine.Status = 1;
                        //    CurrentLine.SuppliersID = SuppliersID;

                        //    if (!string.IsNullOrEmpty(Notes))
                        //    {
                        //        CurrentLine.Notes = Notes;
                        //    }

                        //    var IsComplete = LinesList.Any(s => s.Status != 3 && s.Status != 2 && s.Status != 0 && s.OrdersLinesID != CurrentLine.OrdersLinesID);

                        //    if (IsComplete || LinesList.Count == 1) //全部派单 更新订单头状态为待接单
                        //    {
                        //        // var OrderHead = ctx.le_orders_head.Where(s => s.OutTradeNo == CurrentLine.OutTradeNo).FirstOrDefault();
                        //        OrderHeadModel.Status = 3;//修改订单头状态为待接单

                        //    }
                        //    OrderHeadModel.AdminID = AdminID;
                        //   // ctx.Entry<le_orders_head>(OrderHeadModel).State = EntityState.Modified;
                        //    //总部派发订单 推送消息给供货商
                        //    new OtherService().UpdatePushMsg(SuppliersID, OrderHeadModel.OutTradeNo, 2);

                        //    CurrentLine.UpdateTime = DateTime.Now;
                        //    ctx.Entry<le_orders_lines>(CurrentLine).State = EntityState.Modified;

                        //}
                        //if (Status == 2)//已接单
                        //{
                        //    if (AdminID != 0)  //总部加急单也有接单权限
                        //    {
                        //        OrderLineLog.AdminID = AdminID;
                        //    }
                        //    if (AdminID == 0 && SuppliersID != 0)
                        //    {
                        //        OrderLineLog.SupplierID = SuppliersID;
                        //    }
                        //    if (CurrentLine.Status == 3) //之前是已取消 现在改为已结单修改销量库存
                        //    {
                        //        CurrentLine.le_goods.SalesVolumes += CurrentLine.GoodsCount;
                        //        CurrentLine.le_goods.TotalSalesVolume += CurrentLine.GoodsCount;
                        //        CurrentLine.le_goods.Stock -= CurrentLine.GoodsCount;

                        //        OrderHeadModel.RealAmount += CurrentLine.GoodsCount * CurrentLine.GoodsPrice;
                        //        OrderHeadModel.RealSupplyAmount += CurrentLine.GoodsCount * CurrentLine.SupplyPrice;
                        //        OrderHeadModel.DeliverCount += CurrentLine.GoodsCount;

                        //        OrderHeadLog.AfterMoney = OrderHeadModel.RealAmount;
                        //        OrderHeadLog.AfterCount = OrderHeadModel.DeliverCount;
                        //    }

                        //    //此处使用多次查询 待优化 使用原生SQL？
                        //    CurrentLine.Status = 2;
                        //    if (!string.IsNullOrEmpty(Notes))
                        //    {
                        //        CurrentLine.Notes = Notes;
                        //    }

                        //    CurrentLine.UpdateTime = DateTime.Now;

                        //   // ctx.Entry<le_orders_lines>(CurrentLine).State = EntityState.Modified;


                        //    var IsComplete = LinesList.Any(s => s.Status != 1 && s.Status != 3 && s.Status != 0 && s.OrdersLinesID != CurrentLine.OrdersLinesID);

                        //    if (IsComplete || LinesList.Count == 1) //全部已接单,更新订单头状态
                        //    {                           
                        //        OrderHeadModel.Status = 4;//修改订单头状态为已结单
                        //        OrderHeadLog.AfterStatus = 4;

                        //    }
                        //    //供货商完成接单 推送消息给总部
                        //    new OtherService().UpdatePushMsg(CurrentLine.AdminID.Value, OrderHeadModel.OutTradeNo, 3);
                        //}
                        //if (Status == 3)//取消订单
                        //{
                        //    OrderLineLog.SupplierID = SuppliersID;

                        //    CurrentLine.Status = 3;
                        //    CurrentLine.le_goods.Stock += CurrentLine.GoodsCount;
                        //    CurrentLine.le_goods.SalesVolumes -= CurrentLine.GoodsCount;
                        //    CurrentLine.le_goods.TotalSalesVolume -= CurrentLine.GoodsCount;
                        //    CurrentLine.DeliverCount = 0;

                        //    if (!string.IsNullOrEmpty(Notes))
                        //    {
                        //        CurrentLine.Notes = Notes;
                        //    }
                        //    CurrentLine.UpdateTime = DateTime.Now;

                        //    //ctx.Entry<le_goods>(CurrentLine.le_goods).State = EntityState.Modified;

                        //    OrderHeadModel.RealAmount -= CurrentLine.GoodsCount * CurrentLine.GoodsPrice;
                        //    OrderHeadModel.RealSupplyAmount -= CurrentLine.GoodsCount * CurrentLine.SupplyPrice;
                        //    OrderHeadModel.DeliverCount -= CurrentLine.GoodsCount;

                        //    OrderHeadLog.AfterMoney = OrderHeadModel.RealAmount;
                        //    OrderHeadLog.AfterCount = OrderHeadModel.DeliverCount;

                        //    new OtherService().UpdatePushMsg(CurrentLine.AdminID.Value, OrderHeadModel.OutTradeNo, 3);
                        //    new OtherService().UpdatePushMsg(CurrentLine.SuppliersID, OrderHeadModel.OutTradeNo, 2);

                        //}
                        #endregion


                        CurrentLine.UpdateTime = DateTime.Now;
                        OrderHeadLog.AfterMoney = OrderHeadModel.RealAmount;
                        OrderHeadLog.AfterCount = OrderHeadModel.DeliverCount;

                        if (OrderHeadLog.BeforeMoney != OrderHeadLog.AfterMoney)
                        {
                            ctx.le_orders_head_log.Add(OrderHeadLog);
                        }

                        OrderLineLog.AfterCount = CurrentLine.DeliverCount;
                        OrderLineLog.AfterStatus = (int)Status;
                        OrderLineLog.OrderLineID = CurrentLine.OrdersLinesID;

                        ctx.le_orders_lines_log.Add(OrderLineLog);

                        if (CurrentLine.le_goods.TotalSalesVolume < 0 || CurrentLine.le_goods.SalesVolumes < 0)
                        {
                            Msg = string.Format("计算错误,月销量不可为负数.订单行ID:{0}", CurrentLine.OrdersLinesID);
                            log.Error(Msg, null);
                            return false;
                        }
                        ctx.Entry<le_orders_lines>(CurrentLine).State = EntityState.Modified;
                        ctx.Entry<le_orders_head>(OrderHeadModel).State = EntityState.Modified;
                    }
                   
                    if (ctx.SaveChanges() > 0)
                    {
                        Msg = "修改状态为已接单成功";
                        return true;
                    }
                    else
                    {
                        Msg = "保存失败" + AdminID.ToString();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Msg = ex.Message;
                    log.Error(OrderHeadModel.OutTradeNo.ToString() + "|" + Status.ToString(), ex);
                    return false;

                }
                Msg = "FAIL";
                return false;
            }
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="OutTradeNo"></param>
        /// <param name="Status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UpdateOrderStatus(string OutTradeNo, int Status, LoginInfo loginInfo, out string msg)
        {
            le_orders_head_log OrderHeadLogModel = new le_orders_head_log();

            using (Entities ctx = new Entities())
            {
                msg = "";
                try
                {
                    var model = ctx.le_orders_head.Where(s => s.OutTradeNo == OutTradeNo).FirstOrDefault();
                    if (model == null)
                    {
                        msg = "该订单不存在，请确认后重试";
                        return false;
                    }

                    OrderHeadLogModel.BeforeCount = model.GoodsCount;
                    OrderHeadLogModel.BeforeMoney = model.RealAmount;
                    OrderHeadLogModel.BeforeStatus = model.Status;
                    if (loginInfo.UserType == 3)
                    {
                        OrderHeadLogModel.AdminID = loginInfo.UserID;
                        if (model.AdminID == null)
                        {
                            model.AdminID = loginInfo.UserID;
                        }
                    }
                    if (loginInfo.UserType == 1)
                    {
                        OrderHeadLogModel.UserID = loginInfo.UserID;
                    }

                    OrderHeadLogModel.OrderHeadID = model.OrdersHeadID;

                   
                    var OrderlineList = model.le_orders_lines.ToList(); //ctx.le_orders_lines.Where(s => s.OutTradeNo == OutTradeNo).ToList();
                    if (OrderlineList.Any(s => s.Status == 1 || s.Status == 2) && Status == 5)
                    {
                        msg = "取消订单失败,订单已在运行";
                        return false;
                    }
                    if (model.Status == 1 && Status == 1)
                    {
                        msg = "该订单已完成，请勿重复操作";
                        return false;
                    }
                    if (model.OrderType != 3 && Status == 4)
                    {
                        msg = "非加急单,无直接接单权限";
                        return false;
                    }
                    if (Status == 1)//已完成
                    {
                       if( OrderlineList.Any(s=>s.Status==1)||OrderlineList.Any(s=>s.Status==0))
                        {
                            msg = "该订单内还有未接单或未派单订单,无法确认完成";
                            return false;
                        }
                        model.CompleteTime = DateTime.Now;
                        new OtherService().UpdatePushMsg(model.UsersID, model.OutTradeNo, 1); //推送消息给用户
                    }
                    if (Status == 4)//加急单直接设置为已结单
                    {

                        foreach (var orderline in OrderlineList)
                        {
                            le_orders_lines_log OrderLineLogModel = new le_orders_lines_log();
                            OrderLineLogModel.BeforeCount = orderline.GoodsCount;
                            OrderLineLogModel.BeforeMoney = orderline.GoodsPrice;
                            OrderLineLogModel.BeforeStatus = orderline.Status;
                            OrderLineLogModel.CreateTime = DateTime.Now;
                            OrderLineLogModel.AfterStatus = 2;
                            OrderLineLogModel.AdminID = loginInfo.UserID;
                            OrderLineLogModel.OrderLineID = orderline.OrdersLinesID;
                            orderline.Status = 2;
                            orderline.UpdateTime = DateTime.Now;
                            ctx.Entry<le_orders_lines>(orderline).State = EntityState.Modified;
                            ctx.le_orders_lines_log.Add(OrderLineLogModel);
                        }
                    }
                    if (Status == 5)//取消订单
                    {
                        if (loginInfo.UserID != model.AdminID && model.AdminID != null)
                        {
                            msg = "无权限操作此订单";
                            return false;
                        }
                        foreach (var orderline in OrderlineList)
                        {
                            if (orderline.Status != 3)
                            {
                                orderline.le_goods.Stock += orderline.GoodsCount;
                                orderline.le_goods.SalesVolumes -= orderline.GoodsCount;
                                orderline.le_goods.TotalSalesVolume -= orderline.GoodsCount;
                                ctx.Entry<le_goods>(orderline.le_goods).State = EntityState.Modified;
                            }
                            le_orders_lines_log OrderLineLogModel = new le_orders_lines_log();
                            OrderLineLogModel.BeforeCount = orderline.GoodsCount;
                            OrderLineLogModel.BeforeMoney = orderline.GoodsPrice;
                            OrderLineLogModel.BeforeStatus = orderline.Status;
                            OrderLineLogModel.AfterStatus = 3;
                            OrderLineLogModel.CreateTime = DateTime.Now;
                            OrderLineLogModel.OrderLineID = orderline.OrdersLinesID;
                            if (loginInfo.UserType == 1)
                            {
                                OrderLineLogModel.UserID = loginInfo.UserID;
                            }
                            if (loginInfo.UserType == 3)
                            {
                                OrderLineLogModel.AdminID = loginInfo.UserID;
                            }

                            orderline.Status = 3;
                            orderline.UpdateTime = DateTime.Now;
                            ctx.Entry<le_orders_lines>(orderline).State = EntityState.Modified;

                            ctx.le_orders_lines_log.Add(OrderLineLogModel);

                            new OtherService().UpdatePushMsg(orderline.SuppliersID, model.OutTradeNo, 2);
                        }
                        if (model.AdminID != null)
                        {
                            new OtherService().UpdatePushMsg(model.AdminID.Value, model.OutTradeNo, 3);
                        }
                        new OtherService().UpdatePushMsg(model.UsersID, model.OutTradeNo, 1);
                    }
                    if (Status == 3)//待接单==派单
                    {
                        if (loginInfo.UserType != 3)
                        {
                            msg = "无操作权限，只有总部人员可操作";
                            return false;
                        }
                        foreach (var orderline in OrderlineList)
                        {

                            if(orderline.Status!=0)
                            {
                                msg = "该订单内还有其他状态，无法派单！";
                                return false;
                            }
                            le_orders_lines_log OrderLineLogModel = new le_orders_lines_log();
                            OrderLineLogModel.BeforeCount = orderline.GoodsCount;
                            OrderLineLogModel.BeforeMoney = orderline.GoodsPrice;
                            OrderLineLogModel.BeforeStatus = orderline.Status;
                            OrderLineLogModel.AfterCount = orderline.DeliverCount;
                            OrderLineLogModel.AfterMoney = orderline.GoodsPrice;
                            OrderLineLogModel.AfterStatus = 1;
                            OrderLineLogModel.CreateTime = DateTime.Now;                          
                            OrderLineLogModel.AdminID = loginInfo.UserID;
                            OrderLineLogModel.OrderLineID = orderline.OrdersLinesID;

                            orderline.AdminID = loginInfo.UserID;
                            orderline.Status = 1;
                            orderline.UpdateTime = DateTime.Now;
                            ctx.Entry<le_orders_lines>(orderline).State = EntityState.Modified;
                            ctx.le_orders_lines_log.Add(OrderLineLogModel);
                        }
                    }


                    model.UpdateTime = DateTime.Now;
                  
                    model.Status = Status;

                    OrderHeadLogModel.AfterStatus = model.Status;
                    OrderHeadLogModel.CreateTime = DateTime.Now;

                    ctx.le_orders_head_log.Add(OrderHeadLogModel);
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
                catch (DbEntityValidationException exception)
                {
                    var errorMessages =
                        exception.EntityValidationErrors
                            .SelectMany(validationResult => validationResult.ValidationErrors)
                            .Select(m => m.ErrorMessage);

                    var fullErrorMessage = string.Join(", ", errorMessages);
                   
                    var exceptionMessage = string.Concat(exception.Message, " 验证异常消息是：", fullErrorMessage);

                    log.Error(exceptionMessage, exception);

                    msg = exceptionMessage;
                    return false;

                }
                catch (Exception ex)
                {
                    msg = "修改异常，异常信息：" + ex.ToString();

                    log.Error(ExceptionHelper.GetInnerExceptionMsg(ex), ex);
                    return false;
                }
                
            }
        }
        /// <summary>
        /// 修改订单头信息
        /// </summary>
        /// <param name="orderEditParams"></param>
        /// <returns></returns>
        public bool UpdateOrderInfo(OrderEditInfo orderEditParams)
        {
            using (Entities ctx = new Entities())
            {
                var Model = ctx.le_orders_head.Where(s => s.OutTradeNo == orderEditParams.OrderNo).FirstOrDefault();
                if (Model == null)
                {
                    return false;
                }
                Model.OrderType = orderEditParams.OrderType;
                //if(orderEditParams.Status!=null)
                //{
                //    Model.Status = orderEditParams.Status.Value;
                //}
                if (!string.IsNullOrEmpty(orderEditParams.Notes))
                {
                    Model.Head_Notes = orderEditParams.Notes;
                }
                if (orderEditParams != null)
                {
                    Model.PickupTime = orderEditParams.PickupTime;
                }
                Model.PickUpMan = orderEditParams.PickUpMan;
                Model.PickUpPhone = orderEditParams.PickUpPhone;
                Model.CarNumber = orderEditParams.CarNumber;
                ctx.Entry<le_orders_head>(Model).State = EntityState.Modified;
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
        /// 设置订单打包数
        /// </summary>
        /// <param name="OrderHeadID"></param>
        /// <param name="SupplierID"></param>
        /// <param name="PackCount"></param>
        /// <returns></returns>
        public bool SetOrderPackCount(int OrderHeadID, int SupplierID, int PackCount)
        {
            using (Entities ctx = new Entities())
            {
                var Model = ctx.le_order_pack.Where(s => s.OrderHeadID == OrderHeadID && s.SupplierID == SupplierID).FirstOrDefault();
                if (Model == null)
                {
                    le_order_pack model = new le_order_pack();
                    model.OrderHeadID = OrderHeadID;
                    model.CreateTime = DateTime.Now;
                    model.UpdateTime = DateTime.Now;
                    model.SupplierID = SupplierID;
                    model.PackCount = PackCount;
                    ctx.le_order_pack.Add(model);

                }
                else
                {
                    Model.PackCount = PackCount;
                    Model.UpdateTime = DateTime.Now;
                    ctx.Entry<le_order_pack>(Model).State = EntityState.Modified;
                }
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    log.Error("SetOrderPackCount" + ex.Message, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取订单打包数
        /// </summary>
        /// <param name="OrderHeadID"></param>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public List<OrderPackCount> GetOrderPackCountsList(int OrderHeadID, int? SupplierID)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_order_pack.Where(s => s.OrderHeadID == OrderHeadID);
                if (SupplierID != null)
                {
                    tempIq = tempIq.Where(s => s.SupplierID == SupplierID);
                }
                var result = tempIq.Select(s => new OrderPackCount
                {
                    OrderHeadID = s.OrderHeadID,
                    SuppliersID = s.SupplierID,
                    Suppliers_Name = s.le_suppliers.SuppliersName,
                    PackCount = s.PackCount,
                    UpdateTime = s.UpdateTime,
                });
                return result.ToList();
            }
            return null;
        }

        /// <summary>
        /// 检查库存，增加销量
        /// </summary>
        /// <param name="goodsStocks"></param>
        /// <returns></returns>
        private bool CheckStock(List<GoodsStock> goodsStocks)
        {

            List<CommandInfo> sqllist = new List<CommandInfo>();
            foreach (var index in goodsStocks)
            {
                if (index.Stock - index.GoodsCount < 0)
                {
                    return false;
                }
                string sql = "update le_goods set Stock=Stock-@GoodsCount,SalesVolumes=SalesVolumes+@GoodsCount,TotalSalesVolume=TotalSalesVolume+@GoodsCount where GoodsID=@GoodsID and RowVersion=@RowVersion and Stock>=0";
                MySqlParameter[] parameters1 =
                {
                    new MySqlParameter("@GoodsID", MySqlDbType.Int32),
                    new MySqlParameter("@RowVersion", MySqlDbType.DateTime),
                    new MySqlParameter("@GoodsCount", MySqlDbType.Int32),
                };
                parameters1[0].Value = index.GoodsID;
                parameters1[1].Value = index.RowVersion;
                parameters1[2].Value = index.GoodsCount;
                CommandInfo commandInfo = new CommandInfo(sql, parameters1, EffentNextType.ExcuteEffectRows);
                sqllist.Add(commandInfo);

            }
            try
            {
                DbHelperMySQL.ExecuteSqlTranWithIndentity(sqllist);
                return true;
            }
            catch (Exception ex)
            {
                var json= JsonConvert.SerializeObject(goodsStocks);
                
                log.Error(json, ex);
                return false;
            }
        }

        private bool IsEqual(List<AddGoodsValues> a, List<AddGoodsValues> b)
        {
            a = a.OrderBy(p => p.CategoryType).ToList();
            b = b.OrderBy(p => p.GoodsValueID).ToList();

            if (a.Count != b.Count)
            {
                return false;
            }
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].CategoryType != b[i].CategoryType)
                {
                    return false;
                }
                if (a[i].GoodsValueID != b[i].GoodsValueID)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 对订单进行状态处理 
        /// </summary>
        /// <param name="linesModel"></param>
        /// <param name="HeadModel"></param>
        /// <param name="IsCancel">当前状态是不是取消</param>
        private void StateProcessing(le_orders_lines CurrentLine, le_orders_head OrderHeadModel, bool IsCancel)
        {
            if (IsCancel) //取消订单 增加库存减销量
            {
               
                CurrentLine.le_goods.Stock += CurrentLine.DeliverCount;
                CurrentLine.le_goods.SalesVolumes -= CurrentLine.DeliverCount;
                CurrentLine.le_goods.TotalSalesVolume -= CurrentLine.DeliverCount;

                OrderHeadModel.RealAmount -= CurrentLine.DeliverCount * CurrentLine.GoodsPrice;
                OrderHeadModel.RealSupplyAmount -= CurrentLine.DeliverCount * CurrentLine.SupplyPrice;
                OrderHeadModel.DeliverCount -= CurrentLine.DeliverCount;
            }
            else //之前是取消状态现在重新操作 减库存加销量
            {
                CurrentLine.le_goods.SalesVolumes += CurrentLine.DeliverCount;
                CurrentLine.le_goods.TotalSalesVolume += CurrentLine.DeliverCount;
                CurrentLine.le_goods.Stock -= CurrentLine.DeliverCount;

                OrderHeadModel.RealAmount += CurrentLine.DeliverCount * CurrentLine.GoodsPrice;
                OrderHeadModel.RealSupplyAmount += CurrentLine.DeliverCount * CurrentLine.SupplyPrice;
                OrderHeadModel.DeliverCount += CurrentLine.DeliverCount;
            }
       

          

        }
    }

}
