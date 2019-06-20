using DTO.Common;
using System.Collections.Generic;

namespace LELAdmin.Models
{
    public class OrderLineLogParams
    {
        public SeachDateTimeOptions SeachOptions { get; set; }
        public int? AdminID { get; set; }

        public List<int> LinesRecordID { get; set; }
    }
}