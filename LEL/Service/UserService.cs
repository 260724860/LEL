using Common;
using DTO.User;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Service
{
    /// <summary>
    /// 商户用户管理
    /// </summary>
    public class StoreUserService
    {
        private static ILog log = LogManager.GetLogger(typeof(StoreUserService));
        private SortedList<string, le_sysconfig> SysConfigs = SysConfig.Get().values;
        /// <summary>
        /// 登陆  
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public UserDTO Login(string LoginName, string PWD,string Token="")
        {
            using (Entities ctx = new Entities())
            {
                UserDTO UserDto = new UserDTO();
                var User = new UserDTO();
                var temp = ctx.le_users.Join(ctx.le_pushmsg, a => a.UsersID, b => b.UserID, (a, b) => new UserDTO
                {
                    Address = a.UsersAddress,
                    status = a.UsersStatus,
                    Salt = a.Salt,
                    BusinessImg = a.UsersBusinessImg,
                    Email = a.UsersEmail,
                    HeadImage = a.UsersImage,
                    IDImgA = a.UsersIDImgA,
                    IDImgB = a.UsersIDImgB,
                    Mobile = a.UsersMobilePhone,
                    NickName = a.UsersNickname,
                    PWD = a.UsersPassWord,
                    TrueName = a.UsersName,
                    UserID = a.UsersID,
                    MsgCount = b.MsgCount,
                    UserType = b.UserType,
                    CarNumber = a.CarNumber,
                    BusinessNo = a.BusinessNo,
                    IDCardNo = a.IDCardNo,
                    Province = a.Province,
                    City = a.City,
                    Area = a.Area,

                    Longitude = a.Longitude,
                    Latitude = a.Latitude,
                    IMEI = a.IMEI,
                    Initial = a.Initial,

                    Landline = a.Landline,
                    FinanceName = a.FinanceName,
                    FinancePhone = a.FinancePhone,
                    AuthCode = a.AuthCode,
                    Remarks = a.Remarks,

                    Zoning = a.Zoning,
                    ContractNumber = a.ContractNumber,
                    Classify = a.Classify,
                    CartModel = a.CartModel,
                    AnotherName = a.AnotherName,
                    ReceiveName = a.ReceiveName,
                    ReceivePhone = a.ReceivePhone,
                    CustomerService = a.CustomerService,
                    CustomerServicePhone = a.CustomerServicePhone,
                    Token = a.Token,


                });//.Where(s => s.Mobile == LoginName).Where(s => s.UserType == 1).FirstOrDefault();
                if(!string.IsNullOrEmpty(Token))
                {
                    User = temp.Where(s => s.Token == Token).FirstOrDefault();
                }
                else
                {
                    User=temp.Where(s => s.Mobile == LoginName).Where(s => s.UserType == 1).FirstOrDefault();
                }
                if (User != null)
                {
                    var DbPwd = DESEncrypt.Decrypt(User.PWD, User.Salt);
                    var result = DESEncrypt.MD5Encrypt32(DbPwd + "SystemLEL");
                    if (result != PWD&&string.IsNullOrEmpty(Token))
                    {
                        User.Code = 1;
                        User.Msg = "账号或密码错误";
                        return User;
                    }
                    if (User.status == 2)
                    {
                        User.Code = 1;
                        User.Msg = "账号被禁用";
                        return User;
                    }
                    var update= ctx.le_users.Where(s => s.UsersID == User.UserID).FirstOrDefault();
                    update.Token = Guid.NewGuid().ToString("N");
                    ctx.Entry<le_users>(update).State=EntityState.Modified;
                    ctx.SaveChanges();
                    User.Code = 0;
                    User.Salt = "******";
                    User.PWD = "*******";
                    User.Msg = "SUCCESS";
                    return User;
                }
                else if(!string.IsNullOrEmpty(Token))
                {
                    User.Code = 1;
                    User.Msg = "Token失效";
                    return User;
                }
                else
                {
                    //log.Debug(string.Format("用户名：{0},登陆失败)",LoginName));
                    UserDto.Code = 1;
                    UserDto.Msg = "该手机号码尚未注册!";
                    return UserDto;
                }
            }
        }


        public string GetUserToken(int UserID)
        {
            using (Entities ctx=new Entities())
            {
                var UserModel = ctx.le_users.Where(s => s.UsersID == UserID).FirstOrDefault();
                if(UserModel==null)
                {
                    return "";
                }
                else
                {
                    string Token = Guid.NewGuid().ToString("N");
                    UserModel.Token = Token;
                    ctx.Entry<le_users>(UserModel).State = EntityState.Modified;
                    ctx.SaveChanges();
                    return Token;
                }
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        //public long Add(UserDTO dTO,out string  Msg)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        if(ctx.le_users.Where(s=>s.UsersMobilePhone==dTO.Mobile).Any())
        //        {
        //            Msg = "该手机号码已被注册";
        //            return 0;
        //        }
        //        le_users model = new le_users();
        //        model.UsersAddress = dTO.Address;
        //        model.UsersImage = dTO.HeadImage;
        //        model.UsersEmail = dTO.Email;
        //        model.UsersMobilePhone = dTO.Mobile;
        //        model.Salt = DESEncrypt.GetCheckCode(6);
        //        model.UsersPassWord = DESEncrypt.Encrypt(dTO.PWD, model.Salt);
        //        model.UsersName = dTO.TrueName;
        //        model.UsersNickname = dTO.NickName;
        //        model.CreateTime = DateTime.Now;
        //        model.UpdateTime = DateTime.Now;
        //        model.UsersStatus = 0;
        //        ctx.le_users.Add(model);
        //        try
        //        {
        //            if(ctx.SaveChanges()>0)
        //            {
        //                Msg = "SUCCESS";
        //                return model.UsersID;
        //            }
        //           else
        //            {
        //                Msg = "FAIL";
        //                return 0;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            log.Debug(string.Format("用户名：{0},Exceprtion" + ex.Message, dTO.Mobile), ex);
        //            Msg = ex.Message;
        //            return 0;
        //        }
        //    }
        //    return 0;
        // }

        /// <summary>
        /// 修改用户资料
        /// </summary>
        /// <param name="dTO"></param>
        /// <param name="oneself"></param>
        /// <returns></returns>
        public bool Update(UserDTO dTO, bool oneself)
        {

            using (Entities ctx = new Entities())
            {
                var UserModel = ctx.le_users.Where(s => s.UsersID == dTO.UserID).FirstOrDefault();
                if (UserModel == null)
                {
                    log.Debug(dTO.UserID);
                    return false;
                }
                UserModel.UsersAddress = dTO.Address;
                UserModel.UsersImage = dTO.HeadImage;
                UserModel.UsersEmail = dTO.Email;
                //UserModel.UsersMobilePhone = dTO.Mobile;
                UserModel.UsersNickname = dTO.NickName;
                UserModel.UsersName = dTO.TrueName;
                UserModel.UsersIDImgA = dTO.IDImgA;
                UserModel.UsersIDImgB = dTO.IDImgB;
                UserModel.UsersBusinessImg = dTO.BusinessImg;
                UserModel.CarNumber = dTO.CarNumber;
                UserModel.BusinessNo = dTO.BusinessNo;
                UserModel.IDCardNo = dTO.IDCardNo;

                UserModel.Province = dTO.Province;
                UserModel.City = dTO.City;
                UserModel.Area = dTO.Area;
                UserModel.Address = dTO.Address;
                UserModel.Longitude = dTO.Longitude;
                UserModel.Latitude = dTO.Latitude;
                UserModel.IMEI = dTO.IMEI;
                UserModel.Initial = dTO.Initial;
                UserModel.Email = dTO.Email;
                UserModel.Landline = dTO.Landline;
                UserModel.FinanceName = dTO.FinanceName;
                UserModel.FinancePhone = dTO.FinancePhone;
                UserModel.AuthCode = dTO.AuthCode;
                UserModel.Remarks = dTO.Remarks;

                UserModel.Zoning = dTO.Zoning;
                UserModel.ContractNumber = dTO.ContractNumber;
                UserModel.Classify = dTO.Classify;
                UserModel.CartModel = dTO.CartModel;
                UserModel.AnotherName = dTO.AnotherName;
                UserModel.ReceiveName = dTO.ReceiveName; 
                UserModel.ReceivePhone=dTO.ReceivePhone;
                UserModel.CustomerService = dTO.CustomerService;
                UserModel.CustomerServicePhone = dTO.CustomerServicePhone;

                if (!oneself)
                {
                    UserModel.UsersStatus = dTO.status;
                }
                UserModel.UpdateTime = DateTime.Now;
                ctx.Entry<le_users>(UserModel).State = EntityState.Modified;
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    throw ex;
                    //string kk = "";
                    //foreach (var index in ex.EntityValidationErrors)
                    //{
                    //    kk += index.ValidationErrors;
                    //}
                    //string msg  = "数据类型错误:" + ExceptionHelper.GetInnerExceptionMsg(ex);
                    //log.Error(msg, ex);
                    //return false;
                }
                catch (Exception ex)
                {
                    log.Error(dTO, ex);
                    throw ex;
                }
                return false;
            }
        }

        /// <summary>
        /// 插入用户TOken记录
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Token"></param>
        public void AddUserToken(long UserID, string Token)
        {
            //using (Entities ctx = new Entities())
            //{
            //    le_user model = new fa_user_token();
            //    model.token = Token;
            //    model.user_id = UserID;
            //    model.createtime = DateTime.Now;
            //    ctx.fa_user_token.Add(model);
            //    ctx.SaveChanges();

            //}
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="PWD"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public int Regist(string LoginName, string PWD, out string Msg)
        {
            using (Entities ctx = new Entities())
            {
                if (ctx.le_users.Where(s => s.UsersMobilePhone == LoginName).Any())
                {
                    Msg = "该手机号码已被注册";
                    return 0;
                }
                le_users model = new le_users();
                model.UsersMobilePhone = LoginName;
                model.Salt = DESEncrypt.GetCheckCode(6);
                model.UsersPassWord = DESEncrypt.Encrypt(PWD, model.Salt);
                model.CreateTime = DateTime.Now;
                model.UsersStatus = 0;

                le_admin_re_users Admin_ReModel = new le_admin_re_users();
                Admin_ReModel.CreateTime = DateTime.Now;
                Admin_ReModel.UpdateTime = DateTime.Now;
                if (SysConfigs.Where(s => s.Value.Name == "CurrentContext").FirstOrDefault().Value.Value == "TEST")
                {
                    Admin_ReModel.AdminID = 9;
                }
                Admin_ReModel.le_users = model;

                ctx.le_admin_re_users.Add(Admin_ReModel);
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        new OtherService().AddPulshMsg(model.UsersID, 1);
                        Msg = "注册成功";
                        return model.UsersID;
                    }
                    else
                    {
                        Msg = "注册失败";
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(LoginName, ex);
                    Msg = ex.Message;
                    return 0;
                }
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public bool UpdatePwd(string Loginname, string Pwd)
        {
            using (Entities ctx = new Entities())
            {
                var result = ctx.le_users.Where(s => s.UsersMobilePhone == Loginname).FirstOrDefault();
                if (result != null)
                {

                    result.Salt = DESEncrypt.GetCheckCode(6);
                    // string TruePwd = DESEncrypt.DecryptString16(Pwd, "SystemLE");
                    result.UsersPassWord = DESEncrypt.Encrypt(Pwd, result.Salt);
                }

                ctx.Entry<le_users>(result).State = EntityState.Modified;
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
        /// 获取门店用户列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<UserDTO> GetUserList(UserSeachOptions options,out int Count,int AdminId= 0 )
        {
            using (Entities ctx = new Entities())
            {
                var temp = ctx.le_users.Where(s => true);

                if (!string.IsNullOrEmpty(options.KeyWords))
                {
                    temp = temp.Where(s => s.UsersNickname.Contains(options.KeyWords)
                      || s.UsersName.Contains(options.KeyWords)
                      || s.UsersMobilePhone.Contains(options.KeyWords)
                      || s.UsersAddress.Contains(options.KeyWords));
                }
                if (options.BeginTime != null)
                {
                    temp = temp.Where(s => s.CreateTime > options.BeginTime.Value);
                }
                if (options.EndTime != null)
                {
                    temp = temp.Where(s => s.CreateTime < options.EndTime.Value);
                }
                if (options.Status != null)
                {
                    temp = temp.Where(s => s.UsersStatus == options.Status);
                }
                if (AdminId != 0)
                {
                    var AdminReUser = ctx.le_admin_re_users.Where(s => s.AdminID == AdminId).Select(s=>s.UserID).ToList();
                    temp = temp.Where(s => AdminReUser.Contains(s.UsersID));
                }
                temp = temp.OrderByDescending(s => s.UsersLoginTime);
                Count = temp.Count();
                temp = temp.Skip(options.Offset).Take(options.Rows);
                var result = temp.Select(s => new UserDTO
                {
                    Address = s.UsersAddress,
                    status = s.UsersStatus,
                    Salt = s.Salt,
                    BusinessImg = s.UsersBusinessImg,
                    Email = s.UsersEmail,
                    HeadImage = s.UsersImage,
                    IDImgA = s.UsersIDImgA,
                    IDImgB = s.UsersIDImgB,
                    Mobile = s.UsersMobilePhone,
                    NickName = s.UsersNickname,
                    PWD = s.UsersPassWord,
                    TrueName = s.UsersName,
                    UserID = s.UsersID,
                    BusinessNo = s.BusinessNo,
                    CarNumber = s.CarNumber,
                    IDCardNo = s.IDCardNo,
                    Province = s.Province,
                    City = s.City,
                    Area = s.Area,
                    Longitude = s.Longitude,
                    Latitude = s.Latitude,
                    IMEI = s.IMEI,
                    Initial = s.Initial,
                    Landline = s.Landline,
                    FinanceName = s.FinanceName,
                    FinancePhone = s.FinancePhone,
                    AuthCode = s.AuthCode,
                    Remarks = s.Remarks,

                    Zoning = s.Zoning,
                    ContractNumber = s.ContractNumber,
                    Classify = s.Classify,
                    CartModel = s.CartModel,
                    AnotherName = s.AnotherName,
                    Token=s.Token

                }).ToList();
                foreach (var index in result)
                {
                    var DbPwd = DESEncrypt.Decrypt(index.PWD, index.Salt);
                    var pwd = DESEncrypt.MD5Encrypt32(DbPwd + "SystemLEL");
                    index.Salt = "******";
                    index.PWD = "********";
                }


                return result;
            }
            return null;
        }


        /// <summary>
        /// 获取商户列表
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        public List<UserBaseInfoDto> GetBaseStoreUserList(string KeyWords, int AdminID)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_admin_re_users.Where(s => s.AdminID == AdminID);
                if (!string.IsNullOrEmpty(KeyWords))
                {
                    tempIq = tempIq.Where(s => s.le_users.UsersNickname.Contains(KeyWords) || s.le_users.UsersMobilePhone.Contains(KeyWords));
                }
                var result = tempIq.Select(s => new UserBaseInfoDto
                {
                    UsersStatus = s.le_users.UsersStatus,
                    UsersID = s.le_users.UsersID,
                    UsersMobilePhone = s.le_users.UsersMobilePhone,
                    UsersNickname = s.le_users.UsersNickname,
                });
                return result.ToList();
            }
        }


    }
}
