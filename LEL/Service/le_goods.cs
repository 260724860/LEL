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
    
    public partial class le_goods
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public le_goods()
        {
            this.le_goods_img = new HashSet<le_goods_img>();
            this.le_goods_log = new HashSet<le_goods_log>();
            this.le_goods_suppliers = new HashSet<le_goods_suppliers>();
            this.le_goods_value = new HashSet<le_goods_value>();
            this.le_orders_lines = new HashSet<le_orders_lines>();
            this.le_shop_cart = new HashSet<le_shop_cart>();
        }
    
        public int GoodsID { get; set; }
        public int GoodsGroupsID { get; set; }
        public string GoodsName { get; set; }
        public string Image { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal SpecialOffer { get; set; }
        public decimal MSRP { get; set; }
        public string Specifications { get; set; }
        public int IsShelves { get; set; }
        public int IsRecommend { get; set; }
        public int IsNewGoods { get; set; }
        public int IsHot { get; set; }
        public int IsSeckill { get; set; }
        public int IsBulkCargo { get; set; }
        public int IsDeliverHome { get; set; }
        public int IsCrossdomain { get; set; }
        public int IsReturn { get; set; }
        public int IsParcel { get; set; }
        public int IsRandomDistribution { get; set; }
        public string Describe { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int Sort { get; set; }
        public string ShelfLife { get; set; }
        public Nullable<int> PackingNumber { get; set; }
        public int SalesVolumes { get; set; }
        public int TotalSalesVolume { get; set; }
        public int Stock { get; set; }
        public System.DateTime RowVersion { get; set; }
        public int Quota { get; set; }
        public int MinimumPurchase { get; set; }
        public int Integral { get; set; }
        public string UrgentOrder { get; set; }
        public Nullable<System.DateTime> SeckillBeginTime { get; set; }
        public Nullable<System.DateTime> SeckillEndTime { get; set; }
        public string Initial { get; set; }
        public string PlaceofOrigin { get; set; }
        public Nullable<System.DateTime> ProductionDate { get; set; }
        public int VirtualNumber { get; set; }
        public int SupplierCount { get; set; }
        public string Remarks { get; set; }
        public int GoodsType { get; set; }
        public Nullable<int> GoodsBarand { get; set; }
        public decimal PriceFull { get; set; }
        public decimal CountFull { get; set; }
        public decimal CountReduction { get; set; }
        public decimal PriceReduction { get; set; }
        public string TermOfValidity { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public Nullable<decimal> PiecePrice { get; set; }
        public Nullable<decimal> MinimumPrice { get; set; }
        public Nullable<int> BusinessValue { get; set; }
        public Nullable<int> NewPeriod { get; set; }
        public string Unit { get; set; }
        public decimal PriceScheme1 { get; set; }
        public decimal PriceScheme2 { get; set; }
        public decimal PriceScheme3 { get; set; }
        public Nullable<System.DateTime> QuotaBeginTime { get; set; }
        public Nullable<System.DateTime> QuotaEndTime { get; set; }
        public decimal Discount { get; set; }
        public int IsVendorClosure { get; set; }
        public string Environment { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_goods_img> le_goods_img { get; set; }
        public virtual le_goods_brand le_goods_brand { get; set; }
        public virtual le_goodsgroups le_goodsgroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_goods_log> le_goods_log { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_goods_suppliers> le_goods_suppliers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_goods_value> le_goods_value { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_orders_lines> le_orders_lines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_shop_cart> le_shop_cart { get; set; }
    }
}
