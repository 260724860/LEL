using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO.Common.Enum;

namespace DTO.ShopOrder
{
     public  class BackOrderListParas
    {
        public int UserID { get; set; }
        /// <summary>
        /// 是否导出Excel 
        /// </summary>
        public bool IsExport { get; set; }
        /// <summary>
        ///1 更新时间倒序 2更新时间正序  3条码正序4 条码倒叙 5 供应商正序 6供应商倒叙
        /// </summary>
        public BackOrderOrderByType OrderByType { get; set; }
        public string Flag { get; set; } = "";
        public DateTime? BeginTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public string MerchantCode { get; set; } = "";
        public string InStock { get; set; } = "";
    }
}
