﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(Entities));
        public Entities()
            : base("name=Entities")
        {
            this.Database.Log = (sql) => {
                log.DebugFormat("EF执行SQL：{0}", sql);
            };
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<le_ad> le_ad { get; set; }
        public virtual DbSet<le_admin> le_admin { get; set; }
        public virtual DbSet<le_admin_re_users> le_admin_re_users { get; set; }
        public virtual DbSet<le_admin_role> le_admin_role { get; set; }
        public virtual DbSet<le_admin_role_mapping> le_admin_role_mapping { get; set; }
        public virtual DbSet<le_admin_role_value> le_admin_role_value { get; set; }
        public virtual DbSet<le_cart_goodsvalue> le_cart_goodsvalue { get; set; }
        public virtual DbSet<le_goods> le_goods { get; set; }
        public virtual DbSet<le_goods_img> le_goods_img { get; set; }
        public virtual DbSet<le_goods_log> le_goods_log { get; set; }
        public virtual DbSet<le_goods_suppliers> le_goods_suppliers { get; set; }
        public virtual DbSet<le_goods_value> le_goods_value { get; set; }
        public virtual DbSet<le_goodsgroups> le_goodsgroups { get; set; }
        public virtual DbSet<le_navigation> le_navigation { get; set; }
        public virtual DbSet<le_order_pack> le_order_pack { get; set; }
        public virtual DbSet<le_orderline_goodsvalue> le_orderline_goodsvalue { get; set; }
        public virtual DbSet<le_orders_head> le_orders_head { get; set; }
        public virtual DbSet<le_orders_head_log> le_orders_head_log { get; set; }
        public virtual DbSet<le_orders_lines> le_orders_lines { get; set; }
        public virtual DbSet<le_orders_lines_log> le_orders_lines_log { get; set; }
        public virtual DbSet<le_orders_modify_record> le_orders_modify_record { get; set; }
        public virtual DbSet<le_orders_operationrecord> le_orders_operationrecord { get; set; }
        public virtual DbSet<le_pushmsg> le_pushmsg { get; set; }
        public virtual DbSet<le_shop_cart> le_shop_cart { get; set; }
        public virtual DbSet<le_sms> le_sms { get; set; }
        public virtual DbSet<le_suppliers> le_suppliers { get; set; }
        public virtual DbSet<le_sys_address> le_sys_address { get; set; }
        public virtual DbSet<le_sysconfig> le_sysconfig { get; set; }
        public virtual DbSet<le_user_address> le_user_address { get; set; }
        public virtual DbSet<le_users> le_users { get; set; }
        public virtual DbSet<lel_admin_suppliers> lel_admin_suppliers { get; set; }
        public virtual DbSet<lel_imei> lel_imei { get; set; }
    }
}
