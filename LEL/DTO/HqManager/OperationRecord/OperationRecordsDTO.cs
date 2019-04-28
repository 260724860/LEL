using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.HqManager.OperationRecord
{
    /// <summary>
    /// 操作记录 DTO
    /// </summary>
    public class GoodsOperationRecordDTO
    {
        public int OperationRecordID { get; set; }

        public int AdminID { get; set; }

        public string AdminName { get; set; }

        public int GoodsID { get; set; }

        public string GoodsName { get; set; }

        public string PreGoodsName { get; set; }

        public string AfterGoodsName { get; set; }

        public decimal PreOriginalPrice { get; set; }

        public decimal AfterOriginalPrice { get; set; }

        public decimal PreSpecialOffer { get; set; }

        public decimal AfterSpecialOffer { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class OrdersOperationRecordDTO
    {
        public int OperationRecordID { get; set; }

        public int AdminID { get; set; }

        public int Orders_Head_ID { get; set; }

        public string OperationAction { get; set; }

        public DateTime CreateTime { get; set; }

        public string AdminName { get; set; }

        public string Out_Trade_No { get; set; }

    }
}
