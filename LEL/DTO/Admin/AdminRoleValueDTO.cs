using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Admin
{
    /// <summary>
    /// 后台用户角色对应Value DTO
    /// </summary>
    public class AdminRoleValueDTO
    {
        public int ID { get; set; }

        public int RoleID { get; set; }

        public int NavigationID { get; set; }

        public string ActionType { get; set; }

    }
}
