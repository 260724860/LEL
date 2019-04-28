using Common;
using DTO.Admin;
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
    /// 后台用户管理
    /// </summary>
    public class AdminService
    {
        private static ILog log = LogManager.GetLogger(typeof(AdminService));

        #region 后台用户管理
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
                var Exit = ctx.le_admin.Where(s => s.loginname == LoginName || s.telephone == LoginName).Select(s => new {
                    s.AdminID,
                    s.loginname,
                    s.salt,
                    s.password,
                    s.status,
                    s.Roleid
                }).FirstOrDefault();
                if (Exit != null)
                {
                    var psd = DESEncrypt.Decrypt(Exit.password, Exit.salt);
                    var result = DESEncrypt.MD5Encrypt32(psd + "SystemLEL");

                    if (PWD != result)
                    {
                        log.Debug(string.Format("用户名：{0},登陆失败)", LoginName));
                        dto.code = 1;
                        dto.msg = "账号或密码错误";
                        return dto;
                    }
                    if (Exit.status != 1)
                    {
                        dto.code = 1;
                        dto.msg = "账号被禁用";
                        return dto;
                    }

                    dto.AdminID = Exit.AdminID;
                    dto.loginname = Exit.loginname;
                    dto.Roleid = Exit.Roleid;

                    #region 查询用户角色权限
                    var AdminRole = GetAdminRole(Exit.Roleid);
                    if (AdminRole.Count > 0)
                    {
                        dto.Role = AdminRole[0];
                        dto.RoleValueList = GetAdminRoleValueList(dto.Role.ID);
                    }
                    #endregion

                    dto.code = 0;
                    dto.msg = "SUCCESS";
                    return dto;
                }
                else
                {
                    log.Debug(string.Format("用户名：{0},登陆失败)", LoginName));
                    dto.code = 0;
                    dto.msg = "账号或密码错误";
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
                try {
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

                    model.loginname = dto.loginname;
                    model.nickname = dto.nickname;
                    model.telephone = dto.telephone;
                    model.salt = DESEncrypt.GetCheckCode(6);
                    model.password = DESEncrypt.Encrypt(dto.password, model.salt);
                    model.email = dto.email;
                    model.status = 1;
                    model.Roleid = dto.Roleid;
                    model.Address = dto.Address;

                    ctx.le_admin.Add(model);
                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "SUCCESS";
                        return true;
                    }

                    msg = "添加失败";
                    return false;
                } catch (Exception ex)
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
                try {

                    var dto = ctx.le_admin.Where(s => s.AdminID == AdminID).FirstOrDefault();
                    if (dto == null)
                    {
                        msg = "该记录不存在，请确认后重试";
                        return false;
                    }
                    dto.status = 0;
                    ctx.Entry<le_admin>(dto).State = EntityState.Modified;
                    if (ctx.SaveChanges() > 0)
                    {
                        msg = "删除成功";
                        return true;
                    }
                    else
                    {
                        msg = "删除失败，请稍后重试";
                        return false;
                    }

                } catch (Exception ex)
                {
                    msg = "删除异常，错误信息："+ ex.ToString();
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
                try {
                    var model = ctx.le_admin.Where(s => s.AdminID == dto.AdminID).FirstOrDefault();
                    if (model == null)
                    {
                        msg = "该记录不存在，请确认后重试";
                        return false;
                    }

                    model.loginname = dto.loginname;
                    model.nickname = dto.nickname;
                    model.telephone = dto.telephone;
                    model.email = dto.email;
                    model.Roleid = dto.Roleid;
                    model.Address = dto.Address;
                    
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
                } catch (Exception ex)
                {
                    msg = "更新异常，错误信息：" + ex.ToString();
                    return false;
                }
            }
        }

        /// <summary>
        /// 查询后台用户
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        public List<AdminDTO> GetAdminList(out string msg,string KeyWords = "")
        {
            using (Entities ctx = new Entities())
            {
                try {
                    List<AdminDTO> List = new List<AdminDTO>();
                    msg = "";
                    var tempIq = from a in ctx.le_admin
                                  join b in ctx.le_admin_role
                                 on a.Roleid equals b.ID
                                 select new AdminDTO
                                 {
                                     AdminID = a.AdminID,
                                     loginname = a.loginname,
                                     nickname = a.nickname,
                                     telephone = a.telephone,
                                     password = a.password,
                                     email = a.email,
                                     status = a.status,
                                     Roleid = a.Roleid,
                                     Address = a.Address,
                                     Createtime = a.Createtime,
                                     RoleName = b.name
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
                            RoleName = s.RoleName
                        }).ToList();

                    }
                    else {
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
                            RoleName = s.RoleName
                        }).AsNoTracking().ToList();

                    }

                    msg = "查询成功";
                    return List;
                }
                catch (Exception ex) {
                    msg = "查询异常，错误信息："+ ex.ToString();
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
                try {

                    var model = ctx.le_admin.Where(s => s.telephone == dto.telephone).FirstOrDefault();
                    if (model == null)
                    {
                        msg = "该账号不存在，请确认后重试";
                        return false;
                    }

                    var psd = DESEncrypt.Encrypt(dto.Beforepassword, model.salt);

                    /*** 判断输入的密码，加密后，是否和数据库加密密码一致 ***/
                    if (psd != model.password)
                    {
                        msg = "密码有误，修改失败";
                        return false;
                    }
                    else {
                        model.password = DESEncrypt.Encrypt(dto.Updatepassword, model.salt);
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
                } catch (Exception ex )
                {
                    msg = "更新密码异常，错误信息：" + ex.ToString();
                    return false;
                }
            }
        }

        /// <summary>
        /// 后台用户 通过手机短信找回密码
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>

        #endregion

        #region 角色权限操作
        /// <summary>
        /// 查询角色权限
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<AdminRoleDTO> GetAdminRole(int ID)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_admin_role.Where(s => s.ID == ID);
                var result = tempIq.Select(s => new AdminRoleDTO
                {
                    ID = s.ID,
                    name = s.name
                }).ToList();
                return result;
            }
        }

        /// <summary>
        /// 查询角色权限Value
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<AdminRoleValueDTO> GetAdminRoleValueList(int ID)
        {
            using (Entities ctx = new Entities())
            {
                var tempIq = ctx.le_admin_role_value.Where(s => s.RoleID == ID);
                var result = tempIq.Select(s => new AdminRoleValueDTO
                {
                    ID = s.ID,
                    RoleID = s.RoleID,
                    NavigationID = s.NavigationID,
                    ActionType = s.ActionType
                }).ToList();
                return result;
            }
        }


        #endregion
    }
}
