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
    
    public partial class le_goods_operationrecord
    {
        public int OperationRecordID { get; set; }
        public int AdminID { get; set; }
        public int GoodsID { get; set; }
        public string PreGoodsName { get; set; }
        public string AfterGoodsName { get; set; }
        public decimal PreOriginalPrice { get; set; }
        public decimal AfterOriginalPrice { get; set; }
        public decimal PreSpecialOffer { get; set; }
        public decimal AfterSpecialOffer { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
