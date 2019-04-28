using Common;
using DTO.User;
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
    /// 商户用户管理
    /// </summary>
    public  class StoreUserService
    {
        private static ILog log = LogManager.GetLogger(typeof(StoreUserService));
        /// <summary>
        /// 登陆  返回用户ID
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public long Login(string LoginName,string PWD,out string Msg )
        {
            using (Entities ctx = new Entities())
            {
                var Exit = ctx.le_users.Where(s => s.UsersMobilePhone == LoginName ).Select(s => new { s.Users_ID,s
                .Salt,s.UsersPassWord,s.UsersStatus}).FirstOrDefault();
                if(Exit!=null)
                {
                    var DbPwd = DESEncrypt.Decrypt(Exit.UsersPassWord, Exit.Salt);
                    var result =DESEncrypt.MD5Encrypt32(DbPwd + "SystemLEL");
                    if (result != PWD)
                    {
                        Msg = "账号或密码错误";
                        return 0;
                    }
                    //if (Exit.UsersPassWord == DESEncrypt.Decrypt(Exit.UsersPassWord, Exit.Salt))
                    //{
                    //    log.Debug(string.Format("用户名：{0},登陆失败)", LoginName));
                    //    Msg = "账号或密码错误";
                    //    return 0;
                    //}

                    if (Exit.UsersStatus != 1)
                    {
                        Msg = "账号被禁用";
                    }
                    Msg = "SUCCESS";
                    return Exit.Users_ID;
                }               
                else
                {
                    log.Debug(string.Format("用户名：{0},登陆失败)",LoginName));
                    Msg = "账号或密码错误";
                    return 0;
                }
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        public long Add(UserModifyDTO dTO,out string  Msg)
        {
            using (Entities ctx = new Entities())
            {
                if(ctx.le_users.Where(s=>s.UsersMobilePhone==dTO.Mobile).Any())
                {
                    Msg = "该手机号码已被注册";
                    return 0;
                }
                le_users model = new le_users();
                model.UsersAddress = dTO.Address;
                model.UsersImage = dTO.Image;
                model.UsersEmail = dTO.Email;
                model.UsersMobilePhone = dTO.Mobile;
                model.Salt = DESEncrypt.GetCheckCode(6);
                model.UsersPassWord = DESEncrypt.Encrypt(dTO.PWD, model.Salt);
                model.UsersName = dTO.TrueName;
                model.UsersNickname = dTO.NickName;
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.UsersStatus = 0;
                ctx.le_users.Add(model);
                try
                {
                    if(ctx.SaveChanges()>0)
                    {
                        Msg = "SUCCESS";
                        return model.Users_ID;
                    }
                   else
                    {
                        Msg = "FAIL";
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    log.Debug(string.Format("用户名：{0},Exceprtion" + ex.Message, dTO.Mobile), ex);
                    Msg = ex.Message;
                    return 0;
                }
            }
            return 0;
         }

        /// <summary>
        /// 修改用户资料
        /// </summary>
        /// <param name="dTO"></param>
        /// <param name="oneself"></param>
        /// <returns></returns>
        public bool Update(UserModifyDTO dTO, bool oneself)
        {
            using (Entities ctx = new Entities())
            {
                var UserModel = ctx.le_users.Where(s => s.UsersMobilePhone == dTO.Mobile).FirstOrDefault();
                if (UserModel == null)
                {
                    return false;
                }
                UserModel.UsersAddress = dTO.Address;
                UserModel.UsersImage = dTO.Image;
                UserModel.UsersImage = dTO.Email;
                UserModel.UsersMobilePhone = dTO.Mobile;
                UserModel.UsersNickname = dTO.NickName;
                UserModel.UsersPassWord = dTO.PWD;
                UserModel.UsersName = dTO.TrueName;
                if (!oneself)
                {
                    UserModel.UsersStatus = dTO.status;
                }
                UserModel.UpdateTime = DateTime.Now;
                ctx.Entry<le_users>(UserModel).State = EntityState.Modified;
                if (ctx.SaveChanges() > 0)
                {
                    return true;
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


        public int Regist(string LoginName,string PWD,out string Msg)
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
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        Msg = "注册成功";
                        return model.Users_ID;
                    }
                    else
                    {
                        Msg = "注册失败";
                        return 0;
                    }
                }
                catch( Exception ex)
                {
                    log.Error(LoginName, ex);
                    Msg = ex.Message;
                    return 0;
                }
            }

        }

    }
}
