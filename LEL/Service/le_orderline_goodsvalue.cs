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
    
    public partial class le_orderline_goodsvalue
    {
        public int ID { get; set; }
        public int GoodsValueID { get; set; }
        public int OrderlineID { get; set; }
        public int CategoryType { get; set; }
    
        public virtual le_goods_value le_goods_value { get; set; }
        public virtual le_orders_lines le_orders_lines { get; set; }
    }
}
