using System;

namespace DTO.SupplierUser
{
    public class SupplierUserDto
    {
        public int SuppliersID { get; set; }

        public string Suppliers_Name { get; set; }

        public string Suppliers_ResponPeople { get; set; }

        public string Suppliers_PassWord { get; set; }

        public string Suppliers_Salt { get; set; }

        public string Suppliers_Email { get; set; }

        public string Suppliers_HeadImage { get; set; }

        public string Suppliers_MobilePhone { get; set; }

        public string Suppliers_IDImgA { get; set; }

        public string Suppliers_IDImgB { get; set; }

        public string Suppliers_BusinessImg { get; set; }

        public string Suppliers_AttachImg1 { get; set; }

        public string Suppliers_AttachImg2 { get; set; }

        public string Suppliers_Addr { get; set; }

        public int Suppliers_Status { get; set; }


        public DateTime? Suppliers_LoginTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public DateTime? CreateTime { get; set; }

        public int Code { get; set; }

        public string Msg { get; set; }

        /// <summary>
        /// 消息值
        /// </summary>
        public int MsgCount { get; set; }

        public int UserType { get; set; }

        public string IDCardNo { get; set; }
        public string BusinessNo { get; set; }

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
        /// 送货人
        /// </summary>
        public string Deliverer { get; set; }
        /// <summary>
        /// 送货人电话
        /// </summary>
        public string DelivererPhone { get; set; }
        /// <summary>
        /// 经营品类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 经营品牌
        /// </summary>
        public string ManagingBrands { get; set; }
        /// <summary>
        /// 客服
        /// </summary>
        public string CustomerService { get; set; }
        /// <summary>
        /// 客服电话
        /// </summary>
        public string CustomerServicePhone { get; set; }
        /// <summary>
        /// 对接人
        /// </summary>
        public string Docker { get; set; }
        /// <summary>
        /// 对接电话
        /// </summary>
        public string DockerPhone { get; set; }
        /// <summary>
        /// 划分区域
        /// </summary>
        public string Zoning { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string CartModel { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Classify { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string AnotherName { get; set; }
        /// <summary>
        /// 登陆token
        /// </summary>
        public string Token { get; set; }

        public string ClientVersion { get; set; }
        public int? IsLogingClient { get; set;  }
        public DateTime? ClientLoginTime { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string OpeningBank { get; set; }
        /// <summary>
        /// 开户人
        /// </summary>
        public string AccountHolder { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankNumber { get; set; }
    }
}
