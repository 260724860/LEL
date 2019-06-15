using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
    public class UserDTO
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
        public string Salt { get; set; }
        /// <summary>
        /// 店铺图
        /// </summary>
        public string HeadImage { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IDImgA { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IDImgB { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string BusinessImg { get; set; }
        /// <summary>
        /// 店铺名
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; } = 0;
        public int UserID { get; set; }

        public int Code { get; set; }
        public string Msg { get; set; }

        /// <summary>
        /// 消息值
        /// </summary>
        public int MsgCount { get; set; }

        /// <summary>
        /// 用户类型 1商户 2供货商 3总部
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }

        public string BusinessNo { get; set; }
        public string IDCardNo { get; set; }
    }
}
