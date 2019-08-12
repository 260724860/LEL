﻿using Common;
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
        private SortedList<string, le_sysconfig> SysConfigs = SysConfig.Get().values;
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
                    Suppliers_IDImgA = a.IDImgA,
                    Suppliers_IDImgB = a.IDImgB,
                    Suppliers_BusinessImg = a.BusinessImg,
                    Suppliers_AttachImg1 = a.AttachImg1,
                    Suppliers_AttachImg2 = a.AttachImg2,
                    Suppliers_Addr = a.Addr,
                    Suppliers_Status = a.Status,
                    // Suppliers_LoginTime = a.SuppliersLoginTime,
                    UpdateTime = a.UpdateTime,
                    CreateTime = a.CreateTime,
                    MsgCount = b.MsgCount,
                    UserType = b.UserType,
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
                    Deliverer = a.Deliverer,
                    DelivererPhone = a.DelivererPhone,
                    Category = a.Category,
                    ManagingBrands = a.ManagingBrands,

                    CustomerService = a.CustomerService,
                    CustomerServicePhone = a.CustomerServicePhone,
                    Docker = a.Docker,
                    DockerPhone = a.DockerPhone,
                    Zoning = a.Zoning,
                    CartModel = a.CartModel,
                    Classify = a.Classify,
                    AnotherName = a.AnotherName,

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
        public bool Update(SupplierUserDto dto, bool oneself,out string msg)
        {
            using (Entities ctx = new Entities())
            {
                var model = ctx.le_suppliers.Where(s => s.SuppliersID == dto.SuppliersID).FirstOrDefault();
                if (dto == null)
                {
                    msg = "该记录不存在";
                    return false;
                }

                model.SuppliersName = dto.Suppliers_Name;
                model.ResponPeople = dto.Suppliers_ResponPeople;
                model.Addr = dto.Suppliers_Addr;
                model.Status = dto.Suppliers_Status;
                model.MobilePhone = dto.Suppliers_MobilePhone;
                model.IDImgA = dto.Suppliers_IDImgA;
                model.IDImgB = dto.Suppliers_IDImgB;
                model.BusinessImg = dto.Suppliers_BusinessImg;
                model.AttachImg1 = dto.Suppliers_AttachImg1;
                model.AttachImg2 = dto.Suppliers_AttachImg2;
                model.HeadImage = dto.Suppliers_HeadImage;
                model.HeadImage = dto.Suppliers_HeadImage;
                model.IDCardNo = dto.IDCardNo;
                model.BusinessNo = dto.BusinessNo;
                model.Province = dto.Province;
                model.City = dto.City;
                model.Area = dto.Area;
                model.Longitude = dto.Longitude;
                model.Latitude = dto.Latitude;
                model.IMEI = dto.IMEI;
                model.Initial = dto.Initial;
                model.Landline = dto.Landline;
                model.FinanceName = dto.FinanceName;
                model.FinancePhone = dto.FinancePhone;
                model.AuthCode = dto.AuthCode;
                model.Remarks = dto.Remarks;
                model.Deliverer = dto.Deliverer;
                model.DelivererPhone = dto.DelivererPhone;
                model.Category = dto.Category;
                model.ManagingBrands = dto.ManagingBrands;
                model.CustomerService = dto.CustomerService;
                model.CustomerServicePhone = dto.CustomerServicePhone;
                model.Docker = dto.Docker;
                model.DockerPhone = dto.DockerPhone;
                model.Zoning = dto.Zoning;
                model.CartModel = dto.CartModel;
                model.Classify = dto.Classify;
                model.AnotherName = dto.AnotherName;

                if (dto.Suppliers_Status == 2)
                {

                    var GoodsSupplierPriceList = model.le_goods_suppliers.ToList();
                    foreach (var index in GoodsSupplierPriceList)
                    {
                        if (index.IsDefalut == 1) //搜索其他供应商替换为默认供应商
                        {
                            var NoDefaultList = ctx.le_goods_suppliers.Where(s => s.GoodsID == index.GoodsID && s.IsDeleted == 0).ToList();
                            if (NoDefaultList == null || NoDefaultList.Count == 0 || NoDefaultList.Count == 1)
                            {
                                msg = string.Format("关闭失败,该供应商为商品ID:{0}的唯一供应商,无法关闭,请确认检查", index.GoodsID);
                                return false;
                            }
                            var SetDefault = NoDefaultList.Where(s => s.GoodsMappingID != index.GoodsMappingID).OrderBy(s => s.Supplyprice).OrderByDescending(s => s.CreatTime).FirstOrDefault();
                            SetDefault.IsDefalut = 1;
                            ctx.Entry<le_goods_suppliers>(SetDefault).State = EntityState.Modified;
                            ctx.le_goods_suppliers.Remove(index);
                        }
                        else
                        {
                            //ctx.Entry<le_goods_suppliers>(index).State = EntityState.Deleted;
                            ctx.le_goods_suppliers.Remove(index);
                        }
                    }
                }
                if (!oneself)
                {
                    model.Status = dto.Suppliers_Status;
                }
                //model.Status = dto.Suppliers_Status;
                ctx.Entry<le_suppliers>(model).State = EntityState.Modified;
                var result = ctx.SaveChanges();
                if (result > 0)
                {
                    msg = "SUCCESS";
                    return true;
                }

                msg = "修改失败";
                return false;
            }


            //using (Entities ctx = new Entities())
            //{
            //    var UserModel = ctx.le_suppliers.Where(s => s.MobilePhone == dTO.Suppliers_MobilePhone).FirstOrDefault();
            //    if (UserModel == null)
            //    {
            //        return false;
            //    }
            //    UserModel.SuppliersID = dTO.SuppliersID;
            //    UserModel.SuppliersName = dTO.Suppliers_Name;
            //    UserModel.ResponPeople = dTO.Suppliers_ResponPeople;

               
            //    UserModel.Email = dTO.Suppliers_Email;
            //    UserModel.HeadImage = dTO.Suppliers_HeadImage;
            //    UserModel.MobilePhone = dTO.Suppliers_MobilePhone;
            //    UserModel.IDImgA = dTO.Suppliers_IDImgA;
            //    UserModel.IDImgB = dTO.Suppliers_IDImgB;
            //    UserModel.BusinessImg = dTO.Suppliers_BusinessImg;
            //    UserModel.AttachImg1 = dTO.Suppliers_AttachImg1;
            //    UserModel.AttachImg2 = dTO.Suppliers_AttachImg2;
            //    UserModel.Addr = dTO.Suppliers_Addr;
              
            //    UserModel.UpdateTime = dTO.UpdateTime;
               
            //    UserModel.IDCardNo = dTO.IDCardNo;
            //    UserModel.BusinessNo = dTO.BusinessNo;

            //    UserModel.Province = dTO.Province;
            //    UserModel.City = dTO.City;
            //    UserModel.Area = dTO.Area;
            //    UserModel.Longitude = dTO.Longitude;
            //    UserModel.Latitude = dTO.Latitude;
            //    UserModel.IMEI = dTO.IMEI;
            //    UserModel.Initial = dTO.Initial;
            //    UserModel.Landline = dTO.Landline;
            //    UserModel.FinanceName = dTO.FinanceName;
            //    UserModel.FinancePhone = dTO.FinancePhone;
            //    UserModel.AuthCode = dTO.AuthCode;
            //    UserModel.Remarks = dTO.Remarks;
            //    UserModel.Deliverer = dTO.Deliverer;
            //    UserModel.DelivererPhone= dTO.DelivererPhone;
            //    UserModel.Category = dTO.Category;
            //    UserModel.ManagingBrands = dTO.ManagingBrands;

            //    UserModel.CustomerService = dTO.CustomerService;
            //    UserModel.CustomerServicePhone = dTO.CustomerServicePhone;
            //    UserModel.Docker = dTO.Docker;
            //    UserModel.DockerPhone = dTO.DockerPhone;
            //    UserModel.Zoning = dTO.Zoning;
            //    UserModel.CartModel = dTO.CartModel;
            //    UserModel.Classify = dTO.Classify;
            //    UserModel.AnotherName = dTO.AnotherName;

               
            //    UserModel.UpdateTime = DateTime.Now;
            //    try
            //    {
            //        ctx.Entry<le_suppliers>(UserModel).State = EntityState.Modified;
            //        if (ctx.SaveChanges() > 0)
            //        {
            //            return true;
            //        }
            //        return false;
            //    }
            //    catch (Exception ex)
            //    {
            //        log.Error(ex);
            //        throw ex;
            //        return false;

            //    }
            //}
           
        }

        /// <summary>
        /// 用户供应商注册
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

                lel_admin_suppliers Admin_ReModel = new lel_admin_suppliers();
                Admin_ReModel.CreateTime = DateTime.Now;
                Admin_ReModel.UpdateTime = DateTime.Now;
                if (SysConfigs.Where(s => s.Value.Name == "CurrentContext").FirstOrDefault().Value.Value == "TEST")
                {
                    Admin_ReModel.AdminID = 9;
                }
                Admin_ReModel.le_suppliers = model;

                ctx.lel_admin_suppliers.Add(Admin_ReModel);

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
                    Suppliers_IDImgA = s.IDImgA,
                    Suppliers_IDImgB = s.IDImgB,
                    Suppliers_BusinessImg = s.BusinessImg,
                    Suppliers_AttachImg1 = s.AttachImg1,
                    Suppliers_AttachImg2 = s.AttachImg2,
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
