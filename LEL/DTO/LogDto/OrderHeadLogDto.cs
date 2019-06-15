using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.LogDto
{
    public class OrderHeadLogDto
    {
        public int BeforeCount { get; set; }
        public int? AfterCount { get; set; }
        public decimal BeforeMoney { get; set; }
        public decimal? AfterMoney { get; set; }
        public int BeforeStatus { get; set; }
        public int? AfterStatus { get; set; }

        public int? AdminID { get; set; }

        public int? UserID { get; set; }

        public int? SupplierID { get; set; }

        public string AdminName { get; set; }
        public string SupplierName { get; set; }
        public string UserName { get; set; }
        public int OrderHeadID { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
