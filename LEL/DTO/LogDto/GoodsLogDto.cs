using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.LogDto
{
    public class GoodsLogDto
    {
        public string AdminName { get; set; }
        public int OperationRecordID { get; set; }
        public int AdminID { get; set; }
        public int GoodsID { get; set; }
        public string BeforeGoodsName { get; set; }
        public string AfterGoodsName { get; set; }
        public decimal BeforeSpecialOffer { get; set; }
        public Nullable<decimal> AfterSpecialOffer { get; set; }
        public int BeforeSheLvesStatus { get; set; }
        public Nullable<int> AfterSheLvesStatus { get; set; }
    
        public int BeforeStock { get; set; }
        public Nullable<int> AfterStock { get; set; }
        public int BeforeQuota { get; set; }
        public Nullable<int> AfterQuota { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
