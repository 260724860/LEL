using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShopOrder
{
    public class OrderDto
    {
        public int Orders_Head_ID { get; set; }
        public string Out_Trade_No { get; set; }
        public string RcAddr { get; set; }
        public string RcName { get; set; }
        public string RcPhone { get; set; }
        public decimal Money { get; set; }
        public int Status { get; set; }
        public string Head_Notes { get; set; }
        public DateTime? UpdateTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int? AdminID { get; set; }
        public int UsersID { get; set; }
        public string UserName { get; set; }
        public DateTime? CompleteTime { get; set; }
        public int LinesCount { get; set; }
    }
}
