using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Common
{
    public class SeachOptions
    {
        public int Offset { get; set; }
        public int Rows { get; set; }
        public string KeyWords { get; set; }
        public int OutCount { get; set; }
    }
}
