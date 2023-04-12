using System;

namespace University.Mooc.Security
{
    public class LoginInfo
    {
        public int UserID { get; set; }
        public Guid SessionID { get; set; }
    }
}
