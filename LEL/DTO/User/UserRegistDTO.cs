using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
    public class UserModifyDTO
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PWD { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// 店铺图
        /// </summary>
        public string Image { get; set; }    
        /// <summary>
        /// 店铺名
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; } = 0;
    }
}
