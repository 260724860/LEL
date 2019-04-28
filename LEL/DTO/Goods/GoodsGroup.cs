using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Goods
{
    public class GoodsGroupDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Sort { get; set; }
        public int ParentID { get; set; }
    }
}
