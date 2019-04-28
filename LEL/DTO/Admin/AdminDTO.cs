using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Admin
{
    /// <summary>
    /// 后台用户 DTO
    /// </summary>
    public class AdminDTO
    {
        #region
        /// <summary>
        /// 用户ID
        /// </summary>
        public int AdminID { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string loginname { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        public string salt { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public int Roleid { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createtime { get; set; }

        #endregion

        #region 扩展字段
        public int code { get; set; }

        public string msg { get; set; }

        public string RoleName { get; set; }

        public AdminRoleDTO Role { get; set; }

        public List<AdminRoleValueDTO> RoleValueList { get; set; }
        #endregion
    }

    public class UpdatePassWordDTO
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// 修改前密码
        /// </summary>
        public string Beforepassword { get; set; }

        /// <summary>
        /// 修改后密码
        /// </summary>
        public string Updatepassword { get; set; }
    }
}
