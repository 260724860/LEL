using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Admin
{
    /// <summary>
    /// 系统菜单 DTO
    /// </summary>
    public class NavigationDTO
    {
        public int ID { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string ActionType { get; set; }
    }
}
