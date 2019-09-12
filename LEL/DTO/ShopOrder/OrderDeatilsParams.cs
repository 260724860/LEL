using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
   public class OrderDeatilsParams
    {
        public int Offset { get; set; }
        public int Rows { get; set; }
        public string KeyWords { get; set; }

        /// <summary>
        /// 多单号，如果为个单号数据自动合并
        /// </summary>
        public List<string> OrderNos { get; set; }
        
        /// <summary>
        /// 总部ID
        /// </summary>
        public int? AdminID { get; set; }
        /// <summary>
        /// 多供应供应商ID
        /// </summary>
        public List<int?> SupplierIDs { get; set; }
        /// <summary>
        /// 多门店ID
        /// </summary>
        public List<int?> UserIDs { get; set; }
    }
}
