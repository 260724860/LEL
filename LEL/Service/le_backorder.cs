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
    
    public partial class le_backorder
    {
        public int ID { get; set; }
        public string BarCode { get; set; }
        public string GoodsName { get; set; }
        public string PurchasePrice { get; set; }
        public string SellingPrice { get; set; }
        public string Specifications { get; set; }
        public Nullable<int> GoodsCount { get; set; }
        public string Merchant { get; set; }
        public string MerchantCode { get; set; }
        public string Classify { get; set; }
        public string ClassifyCode { get; set; }
        public Nullable<int> UsersID { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public string Flag { get; set; }
        public string Remark { get; set; }
        public string InStock { get; set; }
    }
}