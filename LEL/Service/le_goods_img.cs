//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Service
{
    using System;
    using System.Collections.Generic;
    
    public partial class le_goods_img
    {
        public int ID { get; set; }
        public string Src { get; set; }
        public System.DateTime CreatTime { get; set; }
        public int GoodsID { get; set; }
    
        public virtual le_goods le_goods { get; set; }
    }
}
