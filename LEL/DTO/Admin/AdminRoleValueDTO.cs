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
    public class AdminRoleMappingDTO
    {
        public int RoleMappingID { get; set; }

        public int AdminRoleID { get; set; }

        public int NavigationID { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime CreateTime { get; set; }

        #region 扩展字段

        public List<RoleValueDTO> RoleValueDTO { get; set; }

        public NavigationDTO NavigationDTO { get; set; }
        #endregion

    }

    public class RoleValueDTO {

        public int ID { get; set; }

        public int AdminRoleID { get; set; }

        public int NavigationID { get; set; }

        public int ActionType { get; set; }
    }

    public class NavigationDTO {
        public int NavigationID { get; set; }

        public string Name { get; set; }

        public string LinkUrl { get; set; }

        public string Remark { get; set; }

        public int ParentID { get; set; }

        public int Sort { get; set; }

        public int Flag { get; set; }


        public List<RoleValueDTO> RoleValueList { get; set; }
    }
}
