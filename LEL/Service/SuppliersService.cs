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
                    Suppliers_ImgA = s.ImgA,
                    Suppliers_ImgB = s.ImgB,
                    Suppliers_ImgC = s.ImgC,
                    Suppliers_ImgD = s.ImgD,
                    Suppliers_ImgE = s.ImgE,
                    Suppliers_Addr = s.Addr,
                    Suppliers_Status = s.Status,

                    IDCardNo = s.IDCardNo,
                    BusinessNo = s.BusinessNo,

                    // Suppliers_LoginTime = s.LoginTime,
                    UpdateTime = s.UpdateTime.Value,
                    CreateTime = s.CreateTime,

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
        public bool EditSupplierUser(SupplierUserDto dto, out string msg)
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
                model.ImgA = dto.Suppliers_ImgA;
                model.ImgB = dto.Suppliers_ImgB;
                model.ImgC = dto.Suppliers_ImgC;
                model.ImgD = dto.Suppliers_ImgD;
                model.ImgE = dto.Suppliers_ImgE;
                model.HeadImage = dto.Suppliers_HeadImage;
                model.HeadImage = dto.Suppliers_HeadImage;
                model.IDCardNo = dto.IDCardNo;
                model.BusinessNo = dto.BusinessNo;
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
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<UserDTO> GetUserList(UserSeachOptions options, out int Count)
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


                }).ToList();

                return result;
            }
            return null;
        }

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

                //UserModel.

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
