using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Suppliers
{
    public class UpdateOrderLineParamas
    {
       public int Status { get; set; }
       public int OrderLineID { get; set; }
       public string OrderNo { get; set; }
       public string Notes { get; set; }
    }
}
