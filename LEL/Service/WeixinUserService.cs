﻿using DTO.WeixinDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public class WeixinUserService
    {
        /// <summary>
        /// 绑定微信
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="UserType"></param>
        /// <param name="UserID"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool BindWeixinUser(OAuthUserInfo userInfo,int UserType,int UserID,out string Msg)
        {
            using (Entities ctx=new Entities())
            {
                var Exit = ctx.le_weixinuser.Any(s => s.Openid == userInfo.openid && s.UserID == UserID && s.UserType == UserType);
                if(Exit)
                {
                    Msg = "请勿重复绑定";
                }
                le_weixinuser model = new le_weixinuser();
                model.UserType = UserType;
                model.UpdateTime = DateTime.Now;
                model.Unionid = userInfo.unionid;
                model.UserID = UserID;
                model.AppID = userInfo.AppID;
                model.City = userInfo.city;
                model.Country = userInfo.country;
                model.HeadImgUrl = userInfo.headimgurl;
                model.Nickname = userInfo.nickname;
                model.Openid = userInfo.openid;
                model.Privilege = string.Join(",",userInfo.privilege);
                model.Province = userInfo.province;
                model.Sex = userInfo.sex;
                ctx.le_weixinuser.Add(model);
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        Msg = "绑定成功";
                        return true;
                    }
                    else
                    {
                        Msg = "绑定失败";
                        return false;
                    }
                }
                catch( Exception ex)
                {
                    throw ex;
                }
                return false;
            }
        }

        /// <summary>
        /// 解绑微信
        /// </summary>
        /// <param name="UserType"></param>
        /// <param name="UserID"></param>
        /// <param name="Openid"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>

        public bool UnBindWeixinUser(int UserType,int UserID ,string Openid,out string Msg)
        {
            using (Entities ctx = new Entities())
            {
               var model= ctx.le_weixinuser.Where(s => s.UserType == UserType && s.UserID == UserID && s.Openid == Openid).FirstOrDefault();
                if(model==null)
                {
                    Msg = "openid不存在";
                    return false;
                }
                ctx.le_weixinuser.Remove(model);
                if(ctx.SaveChanges()>0)
                {
                    Msg = "解绑成功";
                    return true;
                }
                else
                {
                    Msg = "解绑失败";
                    return false;
                }
                
            }
        }

        //public List<WeixinUserDto> GetWeixinUserList(int UserType, int UserID)
        //{
        //    using (Entities ctx=new Entities())
        //    {
        //        switch (UserType)
        //        {

        //            case 1:
        //                ctx.le
        //                break;
        //        }
        //    }
        //}
    }
}