using Common;
using DTO.Admin;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service
{
    /// <summary>
    /// 后台用户管理
    /// </summary>
    public class AdminService
    {
        private static ILog log = LogManager.GetLogger(typeof(AdminService));

        #region 后台用户管理

        /// <summary>
        /// 设置管理员与加盟店用户关系
        /// </summary>
        /// <param name="UserListID"></param>
        /// <param name="AdminID"></param>
        /// <returns></returns>
        public bool SetAdminReUsers(List<int> UserListID, int AdminID)
        {
            using (Entities ctx = new Entities())
            {
                var Oldrelation = ctx.le_admin_re_users.Where(s => s.AdminID == AdminID);
                ctx.Set<le_admin_re_users>().RemoveRange(Oldrelation);

                foreach (var index in UserListID)
                {
                    le_admin_re_users model = new le_admin_re_users();
                    model.AdminID = AdminID;
                    model.IsDelete = 0;
                    model.CreateTime = DateTime.Now;
                    model.UpdateTime = DateTime.Now;
                    model.UserID = index;
                    ctx.le_admin_re_users.Add(model);
                }
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
                    log.Error("SetAdminReUsers", ex);
                    return false;
                }

            }
        }
        /// <summary>
        /// 设置管理员与供应商用户关系
        /// </summary>
        /// <param name="UserListID"></param>
        /// <param name="AdminID"></param>
        /// <returns></returns>
        public bool SetAdminReSupplier(List<int> UserListID, int AdminID)
        {
            using (Entities ctx = new Entities())
            {
                var Oldrelation = ctx.lel_admin_suppliers.Where(s => s.AdminID == AdminID);
                ctx.Set<lel_admin_suppliers>().RemoveRange(Oldrelation);

                foreach (var index in UserListID)
                {
                    lel_admin_suppliers model = new lel_admin_suppliers();
                    model.AdminID = AdminID;
                    model.IsDelete = 0;
                    model.CreateTime = DateTime.Now;
                    model.UpdateTime = DateTime.Now;
                    model.SupplierID = index;
                    ctx.lel_admin_suppliers.Add(model);
                }
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
                    log.Error("SetAdminReUsers", ex);
                    return false;
                }

            }
        }

        /// <summary>
        /// 获取管理员与供应商权限关系
        /// </summary>
        /// <param name="AdminID"></param>
        /// <returns></returns>
        public List<AdminReUsers> GetAdminReSupplier(int AdminID)
        {
            using (Entities ctx = new Entities())
            {
                var Result = ctx.lel_admin_suppliers.Where(s => s.AdminID == AdminID).Select(
                    k => new AdminReUsers
                    {
                        UserID = k.SupplierID,
                        UserName = k.le_suppliers.SuppliersName
                    }
                    ).ToList();
                return Result;
            }
        }

        /// <summary>
        /// 获取管理员与门店权限关系
        /// </summary>
        /// <param name="AdminID"></param>
        /// <returns></returns>
        public List<AdminReUsers> GetAdminReUsers(int AdminID)
        {
            using (Entities ctx = new Entities())
            {
                var Result = ctx.le_admin_re_users.Where(s => s.AdminID == AdminID).Select(
                    k => new AdminReUsers
                    {
                        UserID = k.UserID,
                        UserName = k.le_users.UsersNickname
                    }
                    ).ToList();
                return Result;
            }
        }


        /// <summary>
        /// 后台用户登录
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="PWD"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public AdminDTO Login(string LoginName, string PWD)
        {
            using (Entities ctx = new Entities())
            {
                AdminDTO dto = new AdminDTO();
                var Exit = ctx.le_admin.Join(ctx.le_pushmsg, a => a.AdminID, b => b.UserID, (a, b) => new
                {
                    a.AdminID,
                    a.LoginName,
                    a.Salt,
                    a.Password,
                    a.Status,
                    a.Roleid,
                    a.AdminRoleStr,
                    b.MsgCount,
                    a.TelePhone,
                    b.UserType,
                    a.IsUrgent
                }).
                Where(s => s.LoginName == LoginName || s.TelePhone == LoginName).Where(s => s.UserType == 3).FirstOrDefault();


                if (Exit != null)
                {
                    var psd = DESEncrypt.Decrypt(Exit.Password, Exit.Salt);
                    var result = DESEncrypt.MD5Encrypt32(psd + "SystemLEL");

                    if (PWD != result)
                    {
                        log.Debug(string.Format("用户名：{0},登陆失败)", LoginName));
                        dto.code = 1;
                        dto.msg = "账号或密码错误";
                        return dto;
                    }
                    if (Exit.Status != 1)
                    {
                        dto.code = 1;
                        dto.msg = "账号被禁用";
                        return dto;
                    }
                    dto.status = Exit.Status;
                    dto.AdminID = Exit.AdminID;
                    dto.loginname = Exit.LoginName;
                    dto.Roleid = Exit.Roleid;
                    dto.AdminRoleStr = Exit.AdminRoleStr;
                    dto.MsgCount = Exit.MsgCount;
                    #region 查询用户角色权限
                    //var AdminRole = GetAdminRole(Exit.Roleid);
                    //if (AdminRole.Count > 0)
                    //{
                    //    dto.Role = AdminRole[0];
                    //    dto.RoleValueList = GetAdminRoleValueList(dto.Role.ID);
                    //}
                    #endregion
                    // dto.AdminID = 2;
                    dto.code = 0;
                    dto.IsUrgent = Exit.IsUrgent;
                    dto.msg = "SUCCESS";
                    return dto;
                }
                else
                {
                    log.Debug(string.Format("用户名：{0},登陆失败)", LoginName));
                    dto.code = 0;
                    dto.msg = "未查询到有效账号";
                    //return 0;
                    return dto;
                }
            }
        }

        /// <summary>
        /// 添加后台用户
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool AddAdmin(AdminDTO dto, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    if (dto == null)
                    {
                        msg = "参数为空，请确认后重试";
                        return false;
                    }

                    var exit = ctx.le_admin.Where(s => s.AdminID == dto.AdminID).Select(s => s.AdminID).FirstOrDefault();
                    if (exit != 0)
                    {
                        msg = "该操作记录已存在，请确认后重试";
                        return false;
                    }

                    le_admin model = new le_admin();

                    model.LoginName = dto.loginname;
                    model.Nickname = dto.nickname;
                    model.TelePhone = dto.telephone;
                    model.Salt = DESEncrypt.GetCheckCode(6);
                    model.Password = DESEncrypt.Encrypt(dto.password, model.Salt);
                    model.Email = dto.email;
                    model.Status = 1;
                    model.Roleid = dto.Roleid;
                    model.Address = dto.Address;
                    model.AdminRoleStr = dto.AdminRoleStr;
                    model.IsUrgent = dto.IsUrgent;
                    ctx.le_admin.Add(model);
                    if (ctx.SaveChanges() > 0)
                    {
                        new OtherService().AddPulshMsg(model.AdminID, 3);
                        msg = "SUCCESS";
                        return true;
                    }

                    msg = "添加失败";
                    return false;
                }
                catch (Exception ex)
                {
                    msg = "添加异常，错误信息：" + ex.ToString();
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除后台用户
        /// </summary>
        /// <param name="AdminID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteAdmin(int AdminID, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {

                    var dto = ctx.le_admin.Where(s => s.AdminID == AdminID).FirstOrDefault();
                    if (dto == null)
                    {
                        msg = "该记录不存在，请确认后重试";
                        return false;
                    }
                    dto.Status = 0;
                    ctx.Entry<le_admin>(dto).State = EntityState.Modified;
                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }
                    else
                    {
                        msg = "删除失败，请稍后重试";
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    msg = "删除异常，错误信息：" + ex.ToString();
                    return false;
                }
            }
        }

        /// <summary>
        /// 编辑后台用户
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UpdateAdmin(AdminDTO dto, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    var model = ctx.le_admin.Where(s => s.AdminID == dto.AdminID).FirstOrDefault();
                    if (model == null)
                    {
                        msg = "该记录不存在，请确认后重试";
                        return false;
                    }

                    model.LoginName = dto.loginname;
                    model.Nickname = dto.nickname;
                    model.TelePhone = dto.telephone;
                    model.Email = dto.email;
                    model.Roleid = dto.Roleid;
                    model.Address = dto.Address;
                    model.AdminRoleStr = dto.AdminRoleStr;

                    ctx.Entry<le_admin>(model).State = EntityState.Modified;

                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "修改成功";
                        return true;
                    }
                    else
                    {
                        msg = "修改失败，请稍后重试";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    msg = "更新异常，错误信息：" + ex.ToString();
                    return false;
                }
            }
        }

        /// <summary>
        /// 更新后台资料
        /// </summary>
        /// <param name="editAdmin"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool EditAdminUserInfo(EditAdminUserDto editAdmin, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                var model = ctx.le_admin.Where(s => s.AdminID == editAdmin.AdminID).FirstOrDefault();
                if (model == null)
                {
                    msg = "参数错误请检查，AdminID：" + editAdmin.AdminID.ToString();
                    return false;
                }
                if (!string.IsNullOrEmpty(editAdmin.AdminRoleStr))
                {
                    model.AdminRoleStr = editAdmin.AdminRoleStr;
                }
                if (!string.IsNullOrEmpty(editAdmin.LoginName))
                {
                    model.LoginName = editAdmin.LoginName;
                }
                if (!string.IsNullOrEmpty(editAdmin.Nickname))
                {
                    model.Nickname = editAdmin.Nickname;
                }
                if (!string.IsNullOrEmpty(editAdmin.Password))
                {
                    model.Salt = DESEncrypt.GetCheckCode(6);
                    model.Password = DESEncrypt.Encrypt(editAdmin.Password, model.Salt);
                }
                if (!string.IsNullOrEmpty(editAdmin.TelePhone))
                {
                    model.TelePhone = editAdmin.TelePhone;
                }
                model.Status = editAdmin.Status;
                //  model.UpdateTime = DateTime.Now;
                ctx.Entry<le_admin>(model).State = EntityState.Modified;
                try
                {
                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }
                    else
                    {
                        msg = "更新错误";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    log.Error(editAdmin, ex);
                    return false;
                }
                msg = "未知错误";
                return false;
            }
        }

        /// <summary>
        /// 查询后台用户
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        public List<AdminDTO> GetAdminList(out string msg, string KeyWords = "")
        {
            using (Entities ctx = new Entities())
            {
                try
                {
                    List<AdminDTO> List = new List<AdminDTO>();
                    msg = "";
                    var tempIq = from a in ctx.le_admin
                                 join b in ctx.le_admin_role
                                on a.Roleid equals b.ID
                                 select new AdminDTO
                                 {
                                     AdminID = a.AdminID,
                                     loginname = a.LoginName,
                                     nickname = a.Nickname,
                                     telephone = a.TelePhone,
                                     password = a.Password,
                                     email = a.Email,
                                     status = a.Status,
                                     Roleid = a.Roleid,
                                     Address = a.Address,
                                     Createtime = a.Createtime,
                                     RoleName = b.name,
                                     AdminRoleStr = a.AdminRoleStr,
                                     IsUrgent = a.IsUrgent
                                 };

                    if (string.IsNullOrEmpty(KeyWords))
                    {
                        List = tempIq.OrderBy(s => s.Roleid).Select(s => new AdminDTO
                        {
                            AdminID = s.AdminID,
                            loginname = s.loginname,
                            nickname = s.nickname,
                            telephone = s.telephone,
                            password = s.password,
                            email = s.email,
                            status = s.status,
                            Roleid = s.Roleid,
                            Address = s.Address,
                            Createtime = s.Createtime,
                            RoleName = s.RoleName,
                            AdminRoleStr = s.AdminRoleStr,
                            IsUrgent = s.IsUrgent
                        }).ToList();

                    }
                    else
                    {
                        List = tempIq.Where(s => s.loginname.Contains(KeyWords)
                        || s.nickname.Contains(KeyWords)
                        || s.telephone.Contains(KeyWords)

                        ).Select(s => new AdminDTO
                        {
                            AdminID = s.AdminID,
                            loginname = s.loginname,
                            nickname = s.nickname,
                            telephone = s.telephone,
                            password = s.password,
                            email = s.email,
                            status = s.status,
                            Roleid = s.Roleid,
                            Address = s.Address,
                            Createtime = s.Createtime,
                            RoleName = s.RoleName,
                            AdminRoleStr = s.AdminRoleStr,
                            IsUrgent = s.IsUrgent
                        }).AsNoTracking().ToList();

                    }

                    msg = "SUCCESS";
                    return List;
                }
                catch (Exception ex)
                {
                    msg = "查询异常，错误信息：" + ex.ToString();
                    return null;
                }
            }
        }

        /// <summary>
        /// 修改后台用户密码
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UpdateAdminPassWord(UpdatePassWordDTO dto, out string msg)
        {
            using (Entities ctx = new Entities())
            {
                try
                {

                    var model = ctx.le_admin.Where(s => s.TelePhone == dto.telephone).FirstOrDefault();
                    if (model == null)
                    {
                        msg = "该账号不存在，请确认后重试";
                        return false;
                    }

                    var psd = DESEncrypt.Encrypt(dto.Beforepassword, model.Salt);

                    /*** 判断输入的密码，加密后，是否和数据库加密密码一致 ***/
                    if (psd != model.Password)
                    {
                        msg = "密码有误，修改失败";
                        return false;
                    }
                    else
                    {
                        model.Password = DESEncrypt.Encrypt(dto.Updatepassword, model.Salt);
                        ctx.Entry<le_admin>(model).State = EntityState.Modified;

                        if (ctx.SaveChanges() > 0)
                        {
                            msg = "修改成功";
                            return true;
                        }
                        else
                        {
                            msg = "修改失败，请稍后重试";
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg = "更新密码异常，错误信息：" + ex.ToString();
                    return false;
                }
            }
        }



        #endregion

        #region 角色权限操作
        /// <summary>
        /// 查询角色权限组
        /// </summary>
        /// <param name = "ID" ></ param >
        /// < returns ></ returns >
        public List<AdminRoleDTO> GetAdminRole(int ID = 0)
        {
            using (Entities ctx = new Entities())
            {
                IQueryable<le_admin_role> tempIq = null;

                if (ID > 0)
                {
                    tempIq = ctx.le_admin_role.Where(s => s.ID == ID);
                }
                else
                {
                    tempIq = ctx.le_admin_role.Where(s => true).AsNoTracking();
                }
                var result = tempIq.Select(s => new AdminRoleDTO
                {
                    ID = s.ID,
                    name = s.name
                }).ToList();
                return result;
            }
        }

        /// <summary>
        /// 查询角色权限Mapping Value
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<AdminRoleMappingDTO> GetAdminRoleValueList(int ID)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_admin_role_mapping.Where(s => s.AdminRoleID == ID);
                List<AdminRoleMappingDTO> List = new List<AdminRoleMappingDTO>();
                var result = tempIq.Select(s => new AdminRoleMappingDTO
                {
                    RoleMappingID = s.RoleMappingID,
                    AdminRoleID = s.AdminRoleID,
                    NavigationID = s.NavigationID,
                    UpdateTime = s.UpdateTime,
                    CreateTime = s.CreateTime
                }).ToList();

                if (result.Count > 0)
                {
                    foreach (var mod in result)
                    {
                        AdminRoleMappingDTO gourp = new AdminRoleMappingDTO();

                        gourp.RoleMappingID = mod.RoleMappingID;
                        gourp.AdminRoleID = mod.AdminRoleID;
                        gourp.NavigationID = mod.NavigationID;
                        gourp.UpdateTime = mod.UpdateTime;
                        gourp.CreateTime = mod.CreateTime;
                        gourp.NavigationDTO = GetNavigationDTO(gourp.NavigationID, gourp.AdminRoleID);

                        List.Add(gourp);
                    }
                }
                return List;
            }
        }

        /// <summary>
        /// 查询导航栏信息
        /// </summary>
        /// <param name="NavigationID"></param>
        /// <returns></returns>
        public NavigationDTO GetNavigationDTO(int NavigationID, int AdminRoleID)
        {
            using (Entities ctx = new Entities())
            {

                var model = ctx.le_navigation.Where(s => s.ID == NavigationID && s.Flag == 1).Select(s => new NavigationDTO
                {
                    NavigationID = s.ID,
                    Name = s.Name,
                    LinkUrl = s.LinkUrl,
                    ParentID = s.ParentID,
                    Sort = s.Sort,
                    Remark = s.Remark,
                    Flag = s.Flag
                }).FirstOrDefault();


                if (model != null)
                {
                    model.RoleValueList = GetRoleValueList(NavigationID, AdminRoleID);
                }

                return model;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NavigationID"></param>
        /// <param name="AdminRoleID"></param>
        /// <returns></returns>
        public List<RoleValueDTO> GetRoleValueList(int NavigationID, int AdminRoleID)
        {
            using (Entities ctx = new Entities())
            {
                var result = ctx.le_admin_role_value.Where(s => s.AdminRoleID == AdminRoleID && s.NavigationID == NavigationID).Select(s => new RoleValueDTO
                {
                    NavigationID = s.NavigationID,
                    AdminRoleID = s.AdminRoleID,
                    ActionType = s.ActionType,
                    ID = s.ID
                }).ToList();

                return result;
            }
        }




        #endregion




    }
}
