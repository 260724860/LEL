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
    
    public partial class le_users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public le_users()
        {
            this.le_admin_re_users = new HashSet<le_admin_re_users>();
            this.le_orders_head = new HashSet<le_orders_head>();
            this.le_orders_head_log = new HashSet<le_orders_head_log>();
            this.le_orders_lines = new HashSet<le_orders_lines>();
            this.le_orders_lines_log = new HashSet<le_orders_lines_log>();
            this.le_shop_cart = new HashSet<le_shop_cart>();
            this.le_user_address = new HashSet<le_user_address>();
        }
    
        public int UsersID { get; set; }
        public string UsersName { get; set; }
        public string UsersNickname { get; set; }
        public string UsersPassWord { get; set; }
        public string UsersMobilePhone { get; set; }
        public string Salt { get; set; }
        public string UsersEmail { get; set; }
        public string UsersImage { get; set; }
        public string UsersIDImgA { get; set; }
        public string UsersIDImgB { get; set; }
        public string UsersBusinessImg { get; set; }
        public string UsersLandline { get; set; }
        public Nullable<System.DateTime> UsersLoginTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int UsersStatus { get; set; }
        public string UsersAddress { get; set; }
        public string CarNumber { get; set; }
        public string BusinessNo { get; set; }
        public string IDCardNo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_admin_re_users> le_admin_re_users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_orders_head> le_orders_head { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_orders_head_log> le_orders_head_log { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_orders_lines> le_orders_lines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_orders_lines_log> le_orders_lines_log { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_shop_cart> le_shop_cart { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<le_user_address> le_user_address { get; set; }
    }
}
