using System;

namespace DTO.LogDto
{
    public class OrderLineLogDto
    {
        public int BeforeCount { get; set; }
        public int? AfterCount { get; set; }
        public decimal BeforeMoney { get; set; }
        public decimal? AfterMoney { get; set; }
        public int? BeforeStatus { get; set; }
        public int? AfterStatus { get; set; }

        public string AdminName { get; set; }
        public string SupplierName { get; set; }
        public string UserName { get; set; }

        public int? UserID { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrderLineID { get; set; }
    }
}
