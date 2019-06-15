
﻿using DTO.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    /// <summary>
    /// 订单行Dto
    /// </summary>
    public class OrderLineDto
    {
        public string OrderNo { get; set; }
        public int OrderLineID { get; set; }
        public int OrderHeadID { get;set;}
        public int Goods_ID { get; set; }
        public decimal Money { get; set; }
        public int GoodsCount { get; set; }
        public int DeliverCount { get; set; }

        public string Notes { get; set; }
        public int? Status1 { get; set; }
        public int? Status2{ get; set; }
        public int? Status3 { get; set; }
        public int? AdminID { get; set; }
        public int UsersID { get; set; }
        public int? SuppliersID { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public System.DateTime CreateTime { get; set; }

        public decimal SupplyMoney { get; set; }

        public string GoodsName { get; set; }
        public string GoodsImage { get; set; }
        

        public int OrderType { get; set; }
        #region 扩展属性
        //public string GoodsName { get; set; }
        //public string GoodsImage { get; set; }
        public string AdminName { get; set; }

        public string SuppliersName { get; set; }
        public string UsersName { get; set; }
        public string StatusName { get; set; }
        public List<GoodsValues> GoodsValuesList { get; set; }

        public string RcName { get; set; }
        public string RcPhone { get; set; }
        public string Out_Trade_No { get; set; }

        public DateTime? PickupTime { get; set; }

        public string AdminTelPhone { get; set; }
        #endregion
    }
}
