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
    
    public partial class le_suppliers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public le_suppliers()
        {
            this.le_goods_suppliers = new HashSet<le_goods_suppliers>();
            this.le_goods = new HashSet<le_goods>();
            this.le_orders_lines = new HashSet<le_orders_lines>();
        }
    
        public int SuppliersID { get; set; }
        public string Suppliers_Name { get; set; }
        public string Suppliers_Nickname { get; set; }
        public string Suppliers_PassWord { get; set; }
        public string Suppliers_Salt { get; set; }
        public string Suppliers_Email { get; set; }
        public string Suppliers_Image { get; set; }
        public string Suppliers_MobilePhone { get; set; }
        public string Suppliers_Landline { get; set; }
        public int Suppliers_Gender { get; set; }
        public Nullable<System.DateTime> Suppliers_LoginTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public System.DateTime CreateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_goods_suppliers> le_goods_suppliers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_goods> le_goods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_orders_lines> le_orders_lines { get; set; }
    }
}
