using System;

namespace DTO.User
{
    public class LoginInfo
    {
        public int UserID { get; set; }
        public int UserType { get; set; }
        public string LoginName { get; set; }
        public int Status { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
