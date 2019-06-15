using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Common
{
   public class Enum
   {
        /// <summary>
        /// 商品销量排序类型
        /// </summary>
        public enum GoodsSalesReportOrderByType
        {
            SalesAmountAsc=1,
            SalesAmountDesc=2,
            SalesCountAsc=3,
            SalesCountDesc=4,
            TransactiontimesAsc=5,
            TransactiontimesDesc=6,
            ProfitSumAsc=7,
            ProfitSumDesc = 8,
        }

    }
}
