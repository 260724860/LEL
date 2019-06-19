using System;

namespace DTO.User
{
    public class UserSeachOptions
    {
        public int Offset { get; set; }
        public int Rows { get; set; }
        public string KeyWords { get; set; }
        public int? Status { get; set; }
        /// <summary>
        /// 注册开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 注册结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
