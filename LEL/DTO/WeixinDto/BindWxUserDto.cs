using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WeixinDto
{
    public class BindWxUserDto
    {
        public OAuthUserInfo WeixinInfo { get; set; }

        public int UserID { get; set; }
        public int UserType { get; set; }
    }
}
