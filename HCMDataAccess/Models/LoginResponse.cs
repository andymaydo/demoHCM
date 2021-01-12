using System;
using System.Collections.Generic;
using System.Text;

namespace HCMDataAccess.Models
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string NikName { get; set; }
        public int ContactID { get; set; }
        public int UserID { get; set; }

        public bool Success { get; set; }
        public int ErrCode { get; set; }

        public string ErrDescr { get; set; }
    }
}
