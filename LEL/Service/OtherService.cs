using DTO.Common;
using DTO.Others;
using log4net;
using MPApiService;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service
{
    /// <summary>
    /// 其他功能服务层
    /// </summary>
    public class OtherService
    {
        private static ILog log = LogManager.GetLogger(typeof(OtherService));

        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="Img"></param>
        /// <param name="Link"></param>
        /// <param name="Sort"></param>
        /// <param name="AdName"></param>
        /// <returns></returns>
        public int AddAd(string Img, string Link, int Sort, string AdName)
        {
            using (Entities ctx = new Entities())
            {
                le_ad model = new le_ad();
                model.AdName = AdName;
                model.Flag = 1;
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.Link = Link;
                model.ImgSrc = Img;
                model.Sort = Sort;
                try
                {
                    ctx.le_ad.Add(model);
                    if (ctx.SaveChanges() > 0)
                    {
                        return model.ID;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    return 0;
                }
            }
            return 0;
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="AdID"></param>
        /// <returns></returns>
        public bool DeleteAd(int AdID)
        {
            using (Entities ctx = new Entities())
            {
                le_ad entity = new le_ad { ID = AdID };
                ctx.le_ad.Attach(entity);
                ctx.le_ad.Remove(entity);

                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString(), ex);
                    return false;
                }
                return false;
            }
        }
        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="AdId"></param>
        /// <param name="Img"></param>
        /// <param name="Link"></param>
        /// <param name="Sort"></param>
        /// <param name="AdName"></param>
        /// <returns></returns>
        public bool UpdateAd(int AdId, string Img, string Link, int Sort, string AdName, int flag = 1)
        {
            using (Entities ctx = new Entities())
            {
                var temp = ctx.le_ad.Where(s => s.ID == AdId).FirstOrDefault();
                if (temp == null)
                {
                    return false;
                }

                temp.AdName = AdName;
                temp.Flag = flag;
                temp.CreateTime = DateTime.Now;
                temp.UpdateTime = DateTime.Now;
                temp.Link = Link;
                temp.ImgSrc = Img;
                temp.Sort = Sort;
                try
                {
                    ctx.Entry<le_ad>(temp).State = EntityState.Modified;
                    if (ctx.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="rows"></param>
        /// <param name="Keywords"></param>
        /// <param name="Flag"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<le_ad> GetAdList(string Keywords, int Flag, out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var tmepIq = ctx.le_ad.Where(s => s.Flag == Flag);
                if (!string.IsNullOrEmpty(Keywords))
                {
                    tmepIq = tmepIq.Where(s => s.AdName.Contains(Keywords));
                }
                Count = tmepIq.Count();
                var result = tmepIq.OrderByDescending(s => s.Sort).ToList();
                return result;
            }
        }

        /// <summary>
        /// 添加收获地址
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ReceiveName"></param>
        /// <param name="ReceivePhone"></param>
        /// <param name="ReceiveArea"></param>
        /// <param name="ReceiveAddress"></param>
        /// <param name="DefaultAddr"></param>
        /// <returns></returns>
        public int AddAddress(int UserID, string ReceiveName, string ReceivePhone, string ReceiveArea, string ReceiveAddress, int DefaultAddr)
        {
            using (Entities ctx = new Entities())
            {
                le_user_address model = new le_user_address();
                model.CreateTime = DateTime.Now;
                model.DefaultAddr = DefaultAddr;
                model.ReceiveAddress = ReceiveAddress;
                model.ReceiveArea = ReceiveArea;
                model.ReceivePhone = ReceivePhone;
                model.ReceiveName = ReceiveName;
                model.UserID = UserID;
                model.Status = 1;
                ctx.le_user_address.Add(model);
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        return model.AddressID;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    return 0;
                }
            }
        }

        /// <summary>
        /// 修改收获地址
        /// </summary>
        /// <param name="AddressID"></param>
        /// <param name="ReceiveName"></param>
        /// <param name="ReceivePhone"></param>
        /// <param name="ReceiveArea"></param>
        /// <param name="ReceiveAddress"></param>
        /// <param name="DefaultAddr"></param>
        /// <returns></returns>
        public bool UpdateAddress(int AddressID, string ReceiveName, string ReceivePhone, string ReceiveArea, string ReceiveAddress, int DefaultAddr, int Status)
        {
            using (Entities ctx = new Entities())
            {
                le_user_address model = ctx.le_user_address.Where(s => s.AddressID == AddressID).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                if (Status != 0 && Status != 1)
                {
                    return false;
                }
                model.DefaultAddr = DefaultAddr;
                model.ReceiveAddress = ReceiveAddress;
                model.ReceiveArea = ReceiveArea;
                model.ReceivePhone = ReceivePhone;
                model.ReceiveName = ReceiveName;
                model.Status = Status;
                ctx.Entry<le_user_address>(model).State = EntityState.Modified;
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
                    log.Error(ex.Message, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取地址列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<UserAddressDto> GetAddressList(SeachOptions options, int UserID, out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_user_address.Where(s => s.UserID == UserID && s.Status == 1)
                      .Select(s => new UserAddressDto
                      {
                          AddressID = s.AddressID,
                          DefaultAddr = s.DefaultAddr,
                          Status = s.Status,
                          ReceiveAddress = s.ReceiveAddress,
                          ReceiveArea = s.ReceiveArea,
                          ReceiveName = s.ReceiveName,
                          ReceivePhone = s.ReceivePhone,
                          UserID = s.UserID,
                          CartNumber=s.le_users.CarNumber
                      });
                if (!string.IsNullOrEmpty(options.KeyWords))
                {
                    tempIq = tempIq.Where(s => s.ReceiveAddress.Contains(options.KeyWords)
                      || s.ReceiveArea.Contains(options.KeyWords) || s.ReceivePhone.Contains(options.KeyWords));
                }
                tempIq = tempIq.OrderByDescending(s => s.AddressID);
                tempIq = tempIq.Skip(options.Offset).Take(options.Rows);
                Count = tempIq.Count();
                return tempIq.ToList();
            }
        }


        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public le_sms GetSmsRecord(string Phone)
        {
            using (Entities ctx = new Entities())
            {
                var result = ctx.le_sms.Where(s => s.Phone == Phone).OrderByDescending(s => s.CreatTime).FirstOrDefault();
                return result;
            }
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public le_sms GetSmsRecordforNoce(string NoceStr)
        {
            using (Entities ctx = new Entities())
            {
                var result = ctx.le_sms.Where(s => s.NonceStr == NoceStr).OrderByDescending(s => s.CreatTime).FirstOrDefault();
                return result;
            }
        }
        /// <summary>
        /// 添加短信记录
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int AddSmsRecord(string Code, string Phone, string NoceStr)
        {
            using (Entities ctx = new Entities())
            {
                le_sms model = new le_sms();
                model.Code = Code;
                model.CreatTime = DateTime.Now;
                model.Status = 0;
                model.Phone = Phone;
                if (string.IsNullOrEmpty(NoceStr))
                {
                    NoceStr = "NoceStr";
                }
                model.NonceStr = NoceStr;
                ctx.le_sms.Add(model);
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        return model.ID;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(Code, ex);
                    return 0;

                }
            }
        }
       
        /// <summary>
        /// 修改短信记录
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public bool UpdateSmsRecord(string Code, string Phone)
        {
            using (Entities ctx = new Entities())
            {
                var Model = ctx.le_sms.Where(s => s.Phone == Phone && s.Code == Code).OrderByDescending(s => s.CreatTime).FirstOrDefault();
                Model.Status = 1;
                Model.VerifyTime = DateTime.Now;
                ctx.Entry<le_sms>(Model).State = EntityState.Modified;
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
                    log.Error(Phone, ex);
                    return false;
                }
            }
        }


        /// <summary>
        /// 添加系统收货地址
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ReceiveName"></param>
        /// <param name="ReceivePhone"></param>
        /// <param name="ReceiveArea"></param>
        /// <param name="ReceiveAddress"></param>
        /// <param name="DefaultAddr"></param>
        /// <returns></returns>
        public int AddSysAddress(string ReceiveName, string ReceivePhone, string ReceiveArea, string ReceiveAddress, int Sort, double? Longitude, double? Latitude)
        {
            using (Entities ctx = new Entities())
            {
                le_sys_address model = new le_sys_address();
                model.CreateTime = DateTime.Now;
                model.ReceiveAddress = ReceiveAddress;
                model.ReceiveArea = ReceiveArea;
                model.ReceivePhone = ReceivePhone;
                model.ReceiveName = ReceiveName;
                model.Status = 1;
                model.Sort = Sort;
                model.Longitude = Longitude;
                model.Latitude = Latitude;
                ctx.le_sys_address.Add(model);
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        return model.AddressID;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    return 0;
                }
            }
        }

        /// <summary>
        /// 修改系统收获地址
        /// </summary>
        /// <param name="AddressID"></param>
        /// <param name="ReceiveName"></param>
        /// <param name="ReceivePhone"></param>
        /// <param name="ReceiveArea"></param>
        /// <param name="ReceiveAddress"></param>
        /// <param name="DefaultAddr"></param>
        /// <returns></returns>
        public bool UpdateSysAddress(int AddressID, string ReceiveName, string ReceivePhone, string ReceiveArea, string ReceiveAddress, int Sort, int Status, double? Longitude, double? Latitude)
        {
            using (Entities ctx = new Entities())
            {
                le_sys_address model = ctx.le_sys_address.Where(s => s.AddressID == AddressID).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                if (Status != 0 && Status != 1)
                {
                    return false;
                }
                model.Sort = Sort;
                model.ReceiveAddress = ReceiveAddress;
                model.ReceiveArea = ReceiveArea;
                model.ReceivePhone = ReceivePhone;
                model.ReceiveName = ReceiveName;
                model.Status = Status;
                model.Longitude = Longitude;
                model.Latitude = Latitude;
                ctx.Entry<le_sys_address>(model).State = EntityState.Modified;
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
                    log.Error(ex.Message, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除收货系统地址
        /// </summary>
        /// <param name="AddressID"></param>
        /// <returns></returns>
        public bool DeleteSysAddress(int AddressID)
        {
            using (Entities ctx = new Entities())
            {
                le_sys_address entity = new le_sys_address { AddressID = AddressID };
                ctx.le_sys_address.Attach(entity);
                ctx.le_sys_address.Remove(entity);

                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString(), ex);
                    return false;
                }
                return false;
            }
        }

        /// <summary>
        /// 获取系统地址列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<le_sys_address> GetSysAddressList(SeachOptions options, out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_sys_address.Where(s => true);
                if (options.Status != null)
                {
                    tempIq = tempIq.Where(s => s.Status == options.Status);
                }
                if (!string.IsNullOrEmpty(options.KeyWords))
                {
                    tempIq = tempIq.Where(s => s.ReceiveAddress.Contains(options.KeyWords)
                      || s.ReceiveArea.Contains(options.KeyWords) || s.ReceivePhone.Contains(options.KeyWords));
                }
                tempIq = tempIq.OrderBy(s => s.Sort);
                tempIq = tempIq.Skip(options.Offset).Take(options.Rows);
                Count = tempIq.Count();
                return tempIq.ToList();
            }
        }

        /// <summary>
        /// 添加消息推送
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="UserType">用户类型 1 商户 2 供应商 3 总部</param>
        /// <returns></returns>
        public int AddPulshMsg(int UserID, int UserType)
        {
            using (Entities ctx = new Entities())
            {
                le_pushmsg le_Pushmsg = new le_pushmsg();
                le_Pushmsg.UserType = UserType;
                le_Pushmsg.MsgCount = 1;
                le_Pushmsg.IsDeleted = 0;
                le_Pushmsg.UpdateTime = DateTime.Now;
                le_Pushmsg.UserID = UserID;
                le_Pushmsg.MsgType = 1;
                ctx.le_pushmsg.Add(le_Pushmsg);
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        return le_Pushmsg.ID;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(UserID, ex);
                    return 0;
                }
            }
        }

       /// <summary>
       /// 推送消息
       /// </summary>
       /// <param name="UserID"></param>
       /// <param name="OrderNO"></param>
       /// <param name="UserType"></param>
       /// <param name="MsgType">1普通订单消息2发货提醒</param>
       /// <param name="IsDeleted"></param>
       /// <param name="PickupTime">取货时间可以为空</param>
       /// <returns></returns>
        public bool UpdatePushMsg(int? UserID, string OrderNO, int UserType, int MsgType = 1,int IsDeleted=0,string PickupTime="")
        {
            if(!UserID.HasValue)
            {
                return false;
            }
            using (Entities ctx = new Entities())
            {
                var WeixinUserList = new WeixinUserService().GetWeixinUserList(UserType, UserID);

                foreach (var item in WeixinUserList)
                {
                    MPApiServiceClient serviceClient = new MPApiServiceClient(new Uri("https://xcy.kdk94.top/"), new AnonymousCredential());
                    var result =  serviceClient.SendSuppliersTemplateMsgWithHttpMessagesAsync(item.openid, OrderNO,item.unionid, PickupTime);
                }

                var model = ctx.le_pushmsg.Where(s => s.UserID == UserID && s.UserType == UserType).FirstOrDefault();
                if (model == null)
                {
                    log.Error(string.Format("推送订单消息失败,UserID:{0}，单号:{1}", UserID, OrderNO));
                    return false;
                }
            
                model.OrderNo = OrderNO;
                model.MsgCount += 1;
                model.UpdateTime = DateTime.Now;
                model.MsgType = MsgType;
                model.IsDeleted = IsDeleted;
                ctx.Entry<le_pushmsg>(model).State = EntityState.Modified;
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
        /// 获取推送消息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserType"></param>
        /// <returns></returns>
        public le_pushmsg GetPushMsg(int UserID, int UserType)
        {
            using (Entities ctx = new Entities())
            {
                var result = ctx.le_pushmsg.Where(s => s.UserID == UserID && s.UserType == UserType).FirstOrDefault();
                return result;
            }
        }
    }
}
