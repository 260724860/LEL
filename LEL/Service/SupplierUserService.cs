using Common;
using DTO.SupplierUser;
using DTO.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service
{
    public class SupplierUserService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(SupplierUserService));
        /// <summary>
        /// 登陆  
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public SupplierUserDto Login(string LoginName, string PWD)
        {
            using (Entities ctx = new Entities())
            {
                var User = ctx.le_suppliers.Join(ctx.le_pushmsg, a => a.SuppliersID, b => b.UserID, (a, b) => new SupplierUserDto
                {
                    SuppliersID = a.SuppliersID,
                    Suppliers_Name = a.SuppliersName,
                    Suppliers_ResponPeople = a.ResponPeople,
                    Suppliers_PassWord = a.PassWord,
                    Suppliers_Salt = a.Salt,
                    Suppliers_Email = a.Email,
                    Suppliers_HeadImage = a.HeadImage,
                    Suppliers_MobilePhone = a.MobilePhone,
                    Suppliers_ImgA = a.ImgA,
                    Suppliers_ImgB = a.ImgB,
                    Suppliers_ImgC = a.ImgC,
                    Suppliers_ImgD = a.ImgD,
                    Suppliers_ImgE = a.ImgE,
                    Suppliers_Addr = a.Addr,
                    Suppliers_Status = a.Status,
                    // Suppliers_LoginTime = a.SuppliersLoginTime,
                    UpdateTime = a.UpdateTime,
                    CreateTime = a.CreateTime,
                    MsgCount = b.MsgCount,
                    UserType = b.UserType,
                    BusinessNo = a.BusinessNo,
                    IDCardNo = a.IDCardNo
                }).Where(s => s.Suppliers_MobilePhone == LoginName).Where(s => s.UserType == 2).FirstOrDefault();

                if (User != null)
                {

                    var DbPwd = DESEncrypt.Decrypt(User.Suppliers_PassWord, User.Suppliers_Salt);
                    var result = DESEncrypt.MD5Encrypt32(DbPwd + "SystemLEL");
                    if (result != PWD)
                    {
                        User.Code = 1;
                        User.Msg = "账号或密码错误";
                        return User;
                    }
                    if (User.Suppliers_Status == 2)
                    {
                        User.Code = 1;
                        User.Msg = "账号被禁用";
                        return User;
                    }
                    //User.Suppliers_LoginTime = DateTime.Now;
                    //ctx.Entry<le_suppliers>(User).State = EntityState.Modified;
                    //ctx.SaveChanges();
                    User.Code = 0;
                    User.Msg = "SUCCESS";
                    return User;
                }
                else
                {
                    //log.Debug(string.Format("用户名：{0},登陆失败)", LoginName));
                    SupplierUserDto UserDto = new SupplierUserDto();
                    UserDto.Code = 1;
                    UserDto.Msg = "该手机号码尚未注册！";
                    return UserDto;
                }
            }
        }

        /// <summary>
        /// 修改用户资料
        /// </summary>
        /// <param name="dTO"></param>
        /// <param name="oneself"></param>
        /// <returns></returns>
        public bool Update(SupplierUserDto dTO, bool oneself)
        {
            using (Entities ctx = new Entities())
            {
                var UserModel = ctx.le_suppliers.Where(s => s.MobilePhone == dTO.Suppliers_MobilePhone).FirstOrDefault();
                if (UserModel == null)
                {
                    return false;
                }
                UserModel.SuppliersID = dTO.SuppliersID;
                UserModel.SuppliersName = dTO.Suppliers_Name;
                UserModel.ResponPeople = dTO.Suppliers_ResponPeople;

                //UserModel.Suppliers_PassWord = dTO.Suppliers_PassWord;
                //UserModel.Suppliers_Salt = dTO.Suppliers_Salt;
                UserModel.Email = dTO.Suppliers_Email;
                UserModel.HeadImage = dTO.Suppliers_HeadImage;
                UserModel.MobilePhone = dTO.Suppliers_MobilePhone;
                UserModel.ImgA = dTO.Suppliers_ImgA;
                UserModel.ImgB = dTO.Suppliers_ImgB;
                UserModel.ImgC = dTO.Suppliers_ImgC;
                UserModel.ImgD = dTO.Suppliers_ImgD;
                UserModel.ImgE = dTO.Suppliers_ImgE;
                UserModel.Addr = dTO.Suppliers_Addr;
                //UserModel.Status = dTO.Suppliers_Status;
                // UserModel.LoginTime = dTO.Suppliers_LoginTime;
                UserModel.UpdateTime = dTO.UpdateTime;
                //  UserModel.CreateTime = dTO.CreateTime.Value;
                UserModel.IDCardNo = dTO.IDCardNo;
                UserModel.BusinessNo = dTO.BusinessNo;
                if (!oneself)
                {
                    UserModel.Status = dTO.Suppliers_Status;
                }
                UserModel.UpdateTime = DateTime.Now;
                try
                {
                    ctx.Entry<le_suppliers>(UserModel).State = EntityState.Modified;
                    if (ctx.SaveChanges() > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    return false;
                }
            }
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
                if (ctx.le_suppliers.Where(s => s.MobilePhone == LoginName).Any())
                {
                    Msg = "该手机号码已被注册";
                    return 0;
                }
                le_suppliers model = new le_suppliers();
                model.MobilePhone = LoginName;
                model.Salt = DESEncrypt.GetCheckCode(6);
                model.PassWord = DESEncrypt.Encrypt(PWD, model.Salt);
                model.CreateTime = DateTime.Now;
                model.Status = 0;
                ctx.le_suppliers.Add(model);
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        new OtherService().AddPulshMsg(model.SuppliersID, 2);
                        Msg = "注册成功";
                        return model.SuppliersID;
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
        /// 获取用户列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<SupplierUserDto> GetUserList(UserSeachOptions options, out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var temp = ctx.le_suppliers.Where(s => true);

                if (!string.IsNullOrEmpty(options.KeyWords))
                {
                    temp = temp.Where(s => s.SuppliersName.Contains(options.KeyWords)
                      || s.SuppliersName.Contains(options.KeyWords)
                      || s.MobilePhone.Contains(options.KeyWords));
                }
                if (options.BeginTime != null)
                {
                    temp = temp.Where(s => s.CreateTime > options.BeginTime.Value);
                }
                if (options.EndTime != null)
                {
                    temp = temp.Where(s => s.CreateTime < options.EndTime.Value);
                }

                temp = temp.OrderByDescending(s => s.CreateTime);
                Count = temp.Count();
                temp = temp.Skip(options.Offset).Take(options.Rows);
                var result = temp.Select(s => new SupplierUserDto
                {

                    SuppliersID = s.SuppliersID,
                    Suppliers_Name = s.SuppliersName,
                    Suppliers_ResponPeople = s.ResponPeople,
                    Suppliers_PassWord = s.PassWord,
                    Suppliers_Salt = s.Salt,
                    Suppliers_Email = s.Email,
                    Suppliers_HeadImage = s.HeadImage,
                    Suppliers_MobilePhone = s.MobilePhone,
                    Suppliers_ImgA = s.ImgA,
                    Suppliers_ImgB = s.ImgB,
                    Suppliers_ImgC = s.ImgC,
                    Suppliers_ImgD = s.ImgD,
                    Suppliers_ImgE = s.ImgE,
                    Suppliers_Addr = s.Addr,
                    Suppliers_Status = s.Status,
                    // Suppliers_LoginTime = s.SuppliersLoginTime,
                    UpdateTime = s.UpdateTime.Value,
                    CreateTime = s.CreateTime

                }).ToList();

                return result;
            }
            return null;
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
                var result = ctx.le_suppliers.Where(s => s.MobilePhone == Loginname).FirstOrDefault();
                if (result != null)
                {

                    result.Salt = DESEncrypt.GetCheckCode(6);
                    // string TruePwd = DESEncrypt.DecryptString16(Pwd, "SystemLE");
                    result.PassWord = DESEncrypt.Encrypt(Pwd, result.Salt);
                }
                ctx.Entry<le_suppliers>(result).State = EntityState.Modified;
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
    }
}
