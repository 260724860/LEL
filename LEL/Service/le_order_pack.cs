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
    
    public partial class le_order_pack
    {
        public int ID { get; set; }
        public int OrderHeadID { get; set; }
        public int SupplierID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public int PackCount { get; set; }
    
        public virtual le_orders_head le_orders_head { get; set; }
        public virtual le_suppliers le_suppliers { get; set; }
    }
}
