using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Goods
{
    public class QualityTestingCreateOrEditDto
    {
        public int? ID { get; set; }
        public string Image { get; set; }
        public int IsDelete { get; set; }
        public int GoodsID { get; set; }
        public int SupplierID { get; set; }
    }
}
