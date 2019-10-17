using DTO.SupplierUser;
using DTO.User;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service
{
    /// <summary>
    /// 供货商操作Service
    /// </summary>
    public class SuppliersService
    {
        /// <summary>
        /// 获取供货商列表
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        public List<SupplierUserDto> GetSupplierList(string KeyWords)
        {
            using (Entities ctx = new Entities())
            {
                var temp = ctx.le_suppliers.Where(s => true);
                temp = temp.Where(s => s.Status == 1);
                if (!string.IsNullOrEmpty(KeyWords))
                {
                    temp = temp.Where(s => s.SuppliersName.Contains(KeyWords)
                      || s.SuppliersName.Contains(KeyWords)
                      || s.MobilePhone.Contains(KeyWords));
                }

                temp = temp.OrderByDescending(s => s.CreateTime);
                var result = temp.Select(s => new SupplierUserDto
                {
                    Suppliers_Status = s.Status,
                    SuppliersID = s.SuppliersID,
                    Suppliers_Name = s.SuppliersName

                }).ToList();

                return result;
            }
            return null;
        }

        /// <summary>
        /// 获取供货商列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<SupplierUserDto> GetSupplierUserList(UserSeachOptions options, out int Count)
        {
            using (Entities ctx = new Entities())
            {
                var temp = ctx.le_suppliers.Where(s => true);

                if (!string.IsNullOrEmpty(options.KeyWords))
                {
                    temp = temp.Where(s => s.SuppliersName.Contains(options.KeyWords)
                      || s.ResponPeople.Contains(options.KeyWords)
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
                if (options.Status != null)
                {
                    temp = temp.Where(s => s.Status == options.Status);
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

                    IDCardNo = s.IDCardNo,
                    BusinessNo = s.BusinessNo,
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
                    Deliverer = s.Deliverer,
                    DelivererPhone = s.DelivererPhone,
                    Category = s.Category,
                    ManagingBrands = s.ManagingBrands,
                    // Suppliers_LoginTime = s.LoginTime,
                    UpdateTime = s.UpdateTime.Value,
                    CreateTime = s.CreateTime,

                    CustomerService = s.CustomerService,
                    CustomerServicePhone = s.CustomerServicePhone,
                    Docker = s.Docker,
                    DockerPhone = s.DockerPhone,
                    Zoning = s.Zoning,
                    CartModel = s.CartModel,
                    Classify = s.Classify,
                    AnotherName = s.AnotherName,
                    Token=s.Token,
                    AccountHolder = s.AccountHolder,
                    OpeningBank = s.OpeningBank,
                    BankNumber = s.BankNumber,

            }).ToList();

                return result;
            }
        }

        /// <summary>
        /// 修改供货商信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        //public bool EditSupplierUser(SupplierUserDto dto, out string msg)
        //{
        //    using (Entities ctx = new Entities())
        //    {
        //        var model = ctx.le_suppliers.Where(s => s.SuppliersID == dto.SuppliersID).FirstOrDefault();
        //        if (dto == null)
        //        {
        //            msg = "该记录不存在";
        //            return false;
        //        }

        //        model.SuppliersName = dto.Suppliers_Name;
        //        model.ResponPeople = dto.Suppliers_ResponPeople;
        //        model.Addr = dto.Suppliers_Addr;
        //        model.Status = dto.Suppliers_Status;
        //        model.MobilePhone = dto.Suppliers_MobilePhone;
        //        model.IDImgA = dto.Suppliers_IDImgA;
        //        model.IDImgB = dto.Suppliers_IDImgB;
        //        model.BusinessImg = dto.Suppliers_BusinessImg;
        //        model.AttachImg1 = dto.Suppliers_AttachImg1;
        //        model.AttachImg2 = dto.Suppliers_AttachImg2;
        //        model.HeadImage = dto.Suppliers_HeadImage;
        //        model.HeadImage = dto.Suppliers_HeadImage;
        //        model.IDCardNo = dto.IDCardNo;
        //        model.BusinessNo = dto.BusinessNo;
        //        model.Province = dto.Province;
        //        model.City = dto.City;
        //        model.Area = dto.Area;
        //        model.Longitude = dto.Longitude;
        //        model.Latitude = dto.Latitude;
        //        model.IMEI = dto.IMEI;
        //        model.Initial = dto.Initial;
        //        model.Landline = dto.Landline;
        //        model.FinanceName = dto.FinanceName;
        //        model.FinancePhone = dto.FinancePhone;
        //        model.AuthCode = dto.AuthCode;
        //        model.Remarks = dto.Remarks;
        //        model.Deliverer = dto.Deliverer;
        //        model.DelivererPhone = dto.DelivererPhone;
        //        model.Category = dto.Category;
        //        model.ManagingBrands = dto.ManagingBrands;


        //        if (dto.Suppliers_Status == 2)
        //        {
                   
        //            var GoodsSupplierPriceList = model.le_goods_suppliers.ToList();
        //            foreach (var index in GoodsSupplierPriceList)
        //            {
        //                if (index.IsDefalut == 1) //搜索其他供应商替换为默认供应商
        //                {
        //                    var NoDefaultList = ctx.le_goods_suppliers.Where(s => s.GoodsID == index.GoodsID && s.IsDeleted == 0).ToList();
        //                    if (NoDefaultList == null || NoDefaultList.Count == 0 || NoDefaultList.Count == 1)
        //                    {
        //                        var Current= NoDefaultList.FirstOrDefault();
        //                        Current.le_goods.IsShelves = 0;
        //                        ctx.Entry<le_goods_suppliers>(Current).State = EntityState.Modified;
        //                        //msg = string.Format("关闭失败,该供应商为商品ID:{0}的唯一供应商,无法关闭,请确认检查", index.GoodsID);
        //                        //return false;
        //                    }
        //                    var SetDefault = NoDefaultList.Where(s => s.GoodsMappingID != index.GoodsMappingID).OrderBy(s => s.Supplyprice).OrderByDescending(s => s.CreatTime).FirstOrDefault();
        //                    SetDefault.IsDefalut = 1;
        //                    ctx.Entry<le_goods_suppliers>(SetDefault).State = EntityState.Modified;
        //                    ctx.le_goods_suppliers.Remove(index);
        //                }
        //                else
        //                {
        //                    //ctx.Entry<le_goods_suppliers>(index).State = EntityState.Deleted;
        //                    ctx.le_goods_suppliers.Remove(index);
        //                }
        //            }
        //        }

        //        model.Status = dto.Suppliers_Status;
        //        ctx.Entry<le_suppliers>(model).State = EntityState.Modified;
        //        var result = ctx.SaveChanges();
        //        if (result > 0)
        //        {
        //            msg = "SUCCESS";
        //            return true;
        //        }

        //        msg = "修改失败";
        //        return false;
        //    }
        //}

      
        /// <summary>
        /// 修改用户资料
        /// </summary>
        /// <param name="dTO"></param>
        /// <param name="oneself"></param>
        /// <returns></returns>
        public bool UpdateUserInfo(UserDTO dTO, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                var UserModel = ctx.le_users.Where(s => s.UsersMobilePhone == dTO.Mobile).FirstOrDefault();
                if (UserModel == null)
                {
                    msg = "该记录不存在";
                    return false;
                }
                UserModel.UsersAddress = dTO.Address;
                //UserModel.UsersMobilePhone = dTO.Mobile;
                UserModel.UsersNickname = dTO.NickName;
                UserModel.UsersName = dTO.TrueName;
                UserModel.UsersStatus = dTO.status;
                UserModel.UsersBusinessImg = dTO.BusinessImg;
                UserModel.UsersIDImgA = dTO.IDImgA;
                UserModel.UsersIDImgB = dTO.IDImgB;
                UserModel.UsersImage = dTO.HeadImage;
                UserModel.IDCardNo = dTO.IDCardNo;
                UserModel.BusinessNo = dTO.BusinessNo;
                UserModel.CarNumber = dTO.CarNumber;
                UserModel.UsersEmail = dTO.Email;
                UserModel.Email = dTO.Email;

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
                ctx.Entry<le_users>(UserModel).State = EntityState.Modified;
                if (ctx.SaveChanges() > 0)
                {
                    msg = "SUCCESS";
                    return true;
                }
                msg = "修改失败";
                return false;
            }
        }



    }
}
