using System;

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

    public class OrderHeadChangeRecordDTO
    {
        public int HeadRecordID { get; set; }
        public int OrderHeadID { get; set; }
        public int BeforeCount { get; set; }
        public int AfterCount { get; set; }
        public decimal BeforeMoney { get; set; }
        public decimal AfterMoney { get; set; }
        public int AdminID { get; set; }
        public DateTime CreateTime { get; set; }

        public string AdminName { get; set; }
    }

    public class OrderLineChangeRecordDTO
    {
        public int LinesRecordID { get; set; }
        public int OrderLineID { get; set; }
        public int BeforeCount { get; set; }
        public int AfterCount { get; set; }
        public decimal BeforeMoney { get; set; }
        public decimal AfterMoney { get; set; }
        public int UserID { get; set; }
        public int UserType { get; set; }
        public DateTime CreateTime { get; set; }

        public string UserName { get; set; }
    }
}
