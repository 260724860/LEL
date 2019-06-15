using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
    public class UserBaseInfoDto
    {
        public int UsersID { get; set; }
        public string UsersNickname { get; set; }
        public string UsersMobilePhone { get; set; }
        public int UsersStatus { get; set; }
    }
}
