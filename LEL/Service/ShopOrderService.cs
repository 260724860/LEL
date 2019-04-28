using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using log4net;
using DTO.ShopOrder;
using DTO.Common;

namespace Service
{
    public class ShopOrderService
    {
        private static ILog log = LogManager.GetLogger(typeof(ShopOrderService));

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <param name="GoodValueID"></param>
        /// <param name="GoodsCount"></param>
        /// <param name="Mes"></param>
        /// <returns></returns>
        public int AddCart(int GoodsID, int GoodValueID, int GoodsCount,int UserID,out string Mes)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    var GoodsValuePrice = ctx.le_goods_value_mapping                   
                    .Where(s => s.le_goods.GoodsID == GoodsID && s.GoodsValueID == GoodValueID)
                    .Select(s => s.le_goods.SpecialOffer).FirstOrDefault();
                    if(GoodsValuePrice<=0)
                    {
                        Mes = "对象关联关系不存在";
                        return 0;
                    }
                    var ShopCarModel = ctx.le_shop_cart.Where(s => s.UserID == UserID && s.GoodsID == GoodsID && s.GoodsValueID == GoodValueID).FirstOrDefault();
                    if(ShopCarModel!=null)
                    {
                        ShopCarModel.GoodsCount = GoodsCount;
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
                    le_shop_cart model = new le_shop_cart();
                    model.Createtime = DateTime.Now;
                    model.GoodsCount = GoodsCount;
                    model.GoodsID = GoodsID;
                    model.GoodsValueID=GoodValueID;
                    model.Price = GoodsValuePrice;
                    model.UserID = UserID;
                    ctx.le_shop_cart.Add(model);

                    if (ctx.SaveChanges() > 0)
                    {
                        Mes = "SUCCESS";
                        return model.CartID;

                    }
                }
                catch (Exception ex )
                {
                    log.Error(GoodsID, ex);
                }
            }
            Mes = "操作失败";
           return 0;
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
                var result= ctx.le_shop_cart
                    .Include(H => H.le_goods)
                    .Include(H => H.le_goods_value)
                    .Where(s => s.UserID == UserID)
                    .Select(s=>new ShopCartDto
                    {
                        CartID =s.CartID,
                        GoodsCount=s.GoodsCount,
                        GoodsID=s.GoodsID,
                        GoodsImg=s.le_goods.Image,
                        GoodsName=s.le_goods.GoodsName,
                        GoodsValueName=s.le_goods_value.GoodsValue,
                        Price=s.le_goods.SpecialOffer
                    }).ToList();
                return result;
            }
        }

        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="orderGoodsList"></param>
        /// <param name="UserID"></param>
        /// <param name="AddressID"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public int OrderSave(List<OrderGoodsList> orderGoodsList,int UserID,int AddressID,out string Msg)
        {
            using (Entities ctx = new Entities())
            {

                //判断商品和商品属性是否是映射关系
                //var GoodsIDArry = orderGoodsList.Select(k => k.GoodsID).ToArray();
                //var GoodsValueIDArry = orderGoodsList.Select(k => k.GoodsValueID).ToArray();
                //var GoodsList = ctx.le_goods_value_mapping.Where(s => GoodsIDArry.Contains(s.GoodsID) && GoodsValueIDArry.Contains(s.GoodsValueID)).
                //    Select(s => new { s.GoodsID, s.GoodsValueID, s.le_goods.SpecialOffer })
                //    .ToList();
                //if (orderGoodsList.Count!=GoodsList.Count)
                //{
                //    Msg = "商品输入错误";
                //    return 0;
                //}

                var UserAddress = ctx.le_user_address.Where(s => s.AddressID == AddressID && s.UserID == UserID).FirstOrDefault();
                if (UserAddress == null)
                {
                    Msg = "地址输入错误";
                    return 0;
                }
                List<le_orders_lines> OrderLinesList = new List<le_orders_lines>();
                string Trade_no = Common.RandomUtils.GenerateOutTradeNo("LEL");
                foreach (var goodsModel in orderGoodsList)
                {
                    var Price = ctx.le_goods_value_mapping.Where(s => s.GoodsID == goodsModel.GoodsID).Select(s => s.le_goods.SpecialOffer).FirstOrDefault();
                    le_orders_lines linesModel = new le_orders_lines();
                    linesModel.CreateTime = DateTime.Now;
                    linesModel.GoodsCount = goodsModel.GoodsCount;
                    linesModel.Money = Price;
                    linesModel.Out_Trade_No = Trade_no;
                    linesModel.Status = 0;
                    linesModel.UpdateTime = DateTime.Now;
                    linesModel.UsersID = UserID;
                    linesModel.Goods_ID = goodsModel.GoodsID;
                    foreach (var model in goodsModel.ValueList)
                    {
                       switch(model.CategoryType)
                        {
                            case 1:linesModel.Category1 = model.GoodsValueID; break;
                            case 2: linesModel.Category2 = model.GoodsValueID; break;
                            case 3: linesModel.Category3 = model.GoodsValueID; break;
                            case 4: linesModel.Category4 = model.GoodsValueID; break;
                            case 5: linesModel.Category5 = model.GoodsValueID; break;
                            default:
                                new Exception("属性类型错误，超出范围");
                                break;
                        }
                    }
                    OrderLinesList.Add(linesModel);
                    ctx.le_orders_lines.Add(linesModel);
                }
                le_orders_head le_Orders_Head = new le_orders_head();
                le_Orders_Head.CreateTime = DateTime.Now;
                le_Orders_Head.Money = OrderLinesList.Sum(s => s.Money * s.GoodsCount);
                le_Orders_Head.Out_Trade_No = Trade_no;
                le_Orders_Head.RcAddr = UserAddress.ReceiveArea + UserAddress.ReceiveAddress;
                le_Orders_Head.RcName = UserAddress.ReceiveName;
                le_Orders_Head.RcPhone = UserAddress.ReceivePhone;
                le_Orders_Head.Status = 0;
                le_Orders_Head.UsersID = UserID;
                le_Orders_Head.UpdateTime = DateTime.Now;
                le_Orders_Head.LinesCount = orderGoodsList.Count;
                ctx.le_orders_head.Add(le_Orders_Head);

                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        Msg = "订单提交成功";
                        return le_Orders_Head.Orders_Head_ID;
                    }
                    else
                    {
                        Msg = "订单提交失败";
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    Msg = ex.Message;
                    log.Error(orderGoodsList, ex);
                    return 0;

                }
                Msg = "FAIL";
                return 0;
            }
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="seachParams"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<OrderDto>GetListOrder(OrderSeachParams seachParams,out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_orders_head.Where(s =>s.CreateTime>=seachParams.BeginTime&&s.CreateTime<=seachParams.EndTime);
                if(seachParams.Status!=null)
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
                if(!string.IsNullOrEmpty(seachParams.Out_Trade_No))
                {
                    tempIq = tempIq.Where(s => s.Out_Trade_No == seachParams.Out_Trade_No);
                }
                if(seachParams.Status!=null)
                {
                    tempIq = tempIq.Where(s => s.Status == seachParams.Status);
                }
                if(seachParams.UserID!=null)
                {
                    tempIq = tempIq.Where(s => s.UsersID == seachParams.UserID);
                }               
                var result = tempIq.Select(s => new OrderDto
                {
                    AdminID = s.AdminID,
                    CompleteTime = s.CompleteTime,
                    CreateTime = s.CreateTime,
                    Head_Notes = s.Head_Notes,
                    LinesCount = s.LinesCount,
                    Money = s.Money,
                    Orders_Head_ID = s.Orders_Head_ID,
                    Out_Trade_No = s.Out_Trade_No,
                    RcAddr = s.RcAddr,
                    Status = s.Status,
                    RcName = s.RcName,
                    RcPhone = s.RcPhone,
                    UpdateTime = s.UpdateTime,
                    UserName = s.le_users.UsersNickname,
                    UsersID = s.UsersID
                }
                );
                result = result.OrderByDescending(s => s.CreateTime);
                result = result.Skip(seachParams.Offset).Take(seachParams.Rows);
                Count = result.Count();
                return result.ToList();
            }
        }

        /// <summary>
        /// 获取订单详细
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public List<OrderDetail> GetOrderDetails(string OrderNo)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_orders_lines
                    .Include(s => s.le_goods)
                    .Include(s => s.le_goods_value)
                    .Include(s => s.le_suppliers)
                    .Where(s => s.Out_Trade_No == OrderNo)
                    .Select(s => new OrderDetail
                    {
                        DefultSupplier = s.le_goods.le_suppliers.Suppliers_Name,
                        GoodsCount = s.GoodsCount,
                        GoodsID = s.Goods_ID,
                        GoodsImg = s.le_goods.Image,
                        GoodsName = s.le_goods.GoodsName,
                        Notes = s.Notes,
                        SpecialOffer = s.le_goods.SpecialOffer,
                        Status = s.Status,
                        SuppliersName = s.le_suppliers.Suppliers_Name,
                        CategoryName1 = s.le_goods_value.GoodsValue,
                        CategoryName2 = s.le_goods_value1.GoodsValue,
                        CategoryName3 = s.le_goods_value2.GoodsValue,
                        CategoryName4 = s.le_goods_value3.GoodsValue,
                        CategoryName5 = s.le_goods_value3.GoodsValue,

                    });
                 var result=tempIq.ToList();
                return result;
            }
        }
    }
}
