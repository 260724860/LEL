using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Suppliers_ImgA { get; set; }

        public string Suppliers_ImgB { get; set; }

        public string Suppliers_ImgC { get; set; }

        public string Suppliers_ImgD { get; set; }

        public string Suppliers_ImgE { get; set; }

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
