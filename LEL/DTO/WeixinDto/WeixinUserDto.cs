using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WeixinDto
{
    public class WeixinUserDto
    {
        public  OAuthUserInfo WeixinInfo { get;set;}
     
        public string SuppliersName { get; set; }
        public string AdminName { get; set; }
        public string UserName { get; set; }

        public int UserID { get; set; }
        public int UserType { get; set; }
    }
}
