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
    
    public partial class le_goods_suppliers
    {
        public int GoodsMappingID { get; set; }
        public int SuppliersID { get; set; }
        public decimal Supplyprice { get; set; }
        public int GoodsID { get; set; }
        public int IsDeleted { get; set; }
        public int IsDefalut { get; set; }
        public System.DateTime CreatTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    
        public virtual le_goods le_goods { get; set; }
        public virtual le_suppliers le_suppliers { get; set; }
    }
}
