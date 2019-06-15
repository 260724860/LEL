using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Report
{
     public  class GoodsSaleDTO
     {

        public string GoodsName { get; set; }
        public string GoodsGroupName { get; set; }
        public string GoodsGroupsID { get; set; }
        public int GoodsID { get; set; }
        /// <summary>
        /// 销售额
        /// </summary>
        public double SalesAmount { get; set; }
        /// <summary>
        /// 销量 件
        /// </summary>
        public int SalesCount { get; set; }

        /// <summary>
        /// 交易次数
        /// </summary>
        public int Transactiontimes { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        public double? ProfitSum { get; set; }
    }
}
