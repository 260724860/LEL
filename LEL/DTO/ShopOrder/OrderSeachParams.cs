using DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    public class OrderSeachParams
    {
        public int Offset { get; set; }
        public int Rows { get; set; }
        public string KeyWords { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? Status { get; set; }
        public string Out_Trade_No { get; set; }
        public int? UserID { get; set; }
    }
}
