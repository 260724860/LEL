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
    }
}
