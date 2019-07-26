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

        /// <summary>
        /// 营业执照编码
        /// </summary>
        public string BusinessNo { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDCardNo { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市区
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string Area { get; set; }
      
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IMEI { get; set; }
        /// <summary>
        /// 首字母
        /// </summary>
        public string Initial { get; set; }
     
        /// <summary>
        /// 座机
        /// </summary>
        public string Landline { get; set; }
        /// <summary>
        /// 财务人员名
        /// </summary>
        public string FinanceName { get; set; }
        /// <summary>
        /// 财务人员联系电话
        /// </summary>
        public string FinancePhone { get; set; }
        /// <summary>
        /// 授权码
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 划分区域
        /// </summary>
        public string Zoning { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string CartModel { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNumber { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Classify { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string AnotherName { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiveName { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceivePhone { get; set; }

        /// <summary>
        /// 客服
        /// </summary>
        public string CustomerService { get; set; }
        /// <summary>
        /// 客服电话
        /// </summary>
        public string CustomerServicePhone { get; set; }
        public string Token { get; set; }
    }
}
