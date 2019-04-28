using DTO.Common;
using DTO.Others;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int AddAd(string Img,string Link,int Sort,string AdName)
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
                try
                {
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
        /// 修改广告
        /// </summary>
        /// <param name="AdId"></param>
        /// <param name="Img"></param>
        /// <param name="Link"></param>
        /// <param name="Sort"></param>
        /// <param name="AdName"></param>
        /// <returns></returns>
        public bool UpdateAd(int AdId,string Img, string Link, int Sort, string AdName,int flag=1)
        {
            using (Entities ctx = new Entities())
            {
                var temp = ctx.le_ad.Where(s => s.ID == AdId).FirstOrDefault();
                if(temp==null)
                {
                    return false;
                }

                temp.AdName = AdName;
                temp.Flag = flag;
                temp.CreateTime = DateTime.Now;
                temp.UpdateTime = DateTime.Now;
                temp.Link = Link;
                temp.ImgSrc = Img;
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
        public  List<le_ad> GetAdList(int offset,int rows,string Keywords,int Flag,out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var tmepIq = ctx.le_ad.Where(s => s.Flag==Flag);
                if(!string.IsNullOrEmpty(Keywords))
                {
                    tmepIq = tmepIq.Where(s => s.AdName.Contains(Keywords));
                }
                Count = tmepIq.Count();
                var result = tmepIq.OrderByDescending(s => s.Sort).Skip(offset).Take(rows).ToList();
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
        public int AddAddress(int UserID,string ReceiveName,string ReceivePhone,string ReceiveArea,string  ReceiveAddress,int DefaultAddr)
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
                try {
                    if(ctx.SaveChanges()>0)
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
        public bool UpdateAddress(int AddressID, string ReceiveName, string ReceivePhone, string ReceiveArea, string ReceiveAddress, int DefaultAddr,int Status)
        {
            using (Entities ctx = new Entities())
            {
                le_user_address model = ctx.le_user_address.Where(s => s.AddressID == AddressID).FirstOrDefault();
                if(model==null)
                {
                    return false;
                }  
                if(Status!=0&&Status!=1)
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
        public List<UserAddressDto> GetAddressList(SeachOptions options,int UserID,out int Count)
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
                          UserID = s.UserID
                      });
                if(!string.IsNullOrEmpty(options.KeyWords))
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
        public int AddSmsRecord(string Code,string Phone,string NoceStr)
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
                catch(Exception ex)
                {
                    return 0;
                    log.Error(Code, ex);
                }
            }
        }
        /// <summary>
        /// 修改短信记录
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public bool UpdateSmsRecord(string Code,string Phone)
        {
            using (Entities ctx = new Entities())
            {
                var Model = ctx.le_sms.Where(s => s.Phone == Phone && s.Code == Code).OrderByDescending(s => s.CreatTime).FirstOrDefault();
                Model.Status = 1;
                Model.VerifyTime = DateTime.Now;
                ctx.Entry<le_sms>(Model).State = EntityState.Modified;
                try
                {
                    if(ctx.SaveChanges()>0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(Exception ex)
                {
                    log.Error(Phone, ex);
                    return false;
                }
            }
        }
    }
}
