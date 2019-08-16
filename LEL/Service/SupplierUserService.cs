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
             //   model.Status = dto.Suppliers_Status;
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

                string updateGoodsSupplierSql = "";
                string UpdateGoodsSql = "";

               List<string> UpdateDefaultSql =new List<string>();


                if (!oneself)
                {
                    model.Status = dto.Suppliers_Status;
                }

                if (model.Status == 2)
                {                   
                    //1.如果该供应商对的商品只有一个，则直接删除供应商价格
                    //2.如果该供应商对的商品有多个，删除该供应商价格，设置其他供应商为默认供应商
                    var temp1 = ctx.le_goods_suppliers.Where(s => s.SuppliersID == model.SuppliersID&&s.IsDeleted==0).Select(s=>s.GoodsID);
                    var temp = ctx.le_goods_suppliers.Where(s => temp1.Contains(s.GoodsID)).GroupBy(s=>s.GoodsID)
                        .Select(s => new { GoodsId = s.Key, Count = s.Count(k => k.GoodsID > 0) }).ToList();
                    if (temp.Count > 0)
                    {
                        string FileterStr = string.Join(",", temp.Select(s => s.GoodsId).ToArray());
                        updateGoodsSupplierSql = string.Format("update le_goods_suppliers set IsDeleted=1 ,IsDefalut=0  where suppliersid={0} and goodsid in ({1})", model.SuppliersID, FileterStr);

                        //一商多供
                        var MultipleList = temp.Where(s => s.Count > 1).Select(s => s.GoodsId).ToArray();
                        //单个商品
                        var SignerList = temp.Where(s => s.Count == 1).Select(s => s.GoodsId).ToArray();

                        if (MultipleList.Length > 0)
                        {
                            //查询更新价格最低得供应商
                            foreach (var index in MultipleList)
                            {
                                UpdateDefaultSql.Add( string.Format(" update le_goods_suppliers a inner join(select GoodsMappingID from le_goods_suppliers where goodsid in ({0}) and SuppliersID != {1} and IsDeleted = 0 order by Supplyprice ASC limit 1) b on a.GoodsMappingID = b.GoodsMappingID set IsDefalut = 1  ",
                               index, model.SuppliersID));
                            }
                            

                        }

                        if (SignerList.Length > 0)
                        {
                            UpdateGoodsSql = string.Format("update le_goods set IsShelves=0 where Goodsid in ({0})", string.Join(",", SignerList));
                        }
                    }
                    
                   
                }
                if (model.Status == 1)
                {
                    var temp1 = ctx.le_goods_suppliers.Where(s => s.SuppliersID == model.SuppliersID && s.IsDeleted == 1).Select(s => s.GoodsID).ToList();
                    if (temp1.Count > 0)
                    {
                        updateGoodsSupplierSql = string.Format("update le_goods_suppliers set IsDeleted=0  where suppliersid={0} and goodsid in ({1})", model.SuppliersID, string.Join(",", temp1));
                        UpdateGoodsSql = string.Format("update le_goods set IsShelves=1 where Goodsid in ({0})", string.Join(",", string.Join(",", temp1)));
                    }
                }
                using (var transe = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(updateGoodsSupplierSql))
                        {
                            ctx.Database.ExecuteSqlCommand(updateGoodsSupplierSql);
                        }
                        if(UpdateDefaultSql.Count>0)
                        {
                            foreach(var item in UpdateDefaultSql)
                            {
                                ctx.Database.ExecuteSqlCommand(item);
                            }
                        }
                        if (!string.IsNullOrEmpty(UpdateGoodsSql))
                        {
                                ctx.Database.ExecuteSqlCommand(UpdateGoodsSql);
                        }
                 
                        var result = ctx.SaveChanges();
                        transe.Commit();
                        if (result > 0)
                        {
                            msg = "SUCCESS";
                            return true;
                        }

                        msg = "修改失败";
                        return false;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        transe.Rollback();
                        return false;
                    }
               }
            }
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
