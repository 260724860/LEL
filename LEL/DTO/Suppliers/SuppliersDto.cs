using System;

namespace DTO.Suppliers
{
    /// <summary>
    /// 供货商dto
    /// </summary>
    public class SuppliersDto
    {
        public int SuppliersID { get; set; }

        public string Suppliers_Name { get; set; }

        public string Suppliers_ResponPeople { get; set; }

        public string Suppliers_PassWord { get; set; }

        public string Suppliers_Salt { get; set; }

        public string Suppliers_Email { get; set; }

        public string Suppliers_Image { get; set; }

        public string Suppliers_MobilePhone { get; set; }

        public string Suppliers_ImgA { get; set; }

        public string Suppliers_ImgB { get; set; }

        public string Suppliers_ImgC { get; set; }

        public string Suppliers_ImgD { get; set; }

        public string Suppliers_ImgE { get; set; }

        public string Suppliers_Addr { get; set; }

        public int Suppliers_Status { get; set; }


        public DateTime Suppliers_LoginTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class GoodsSuppliersInfoDto
    {
        public int GoodsID { get; set; }

        public string GoodsName { get; set; }

        public string Suppliers_Name { get; set; }

        public decimal Price { get; set; }

        public int SuppliersID { get; set; }

        public string Suppliers_MobilePhone { get; set; }
    }
}
