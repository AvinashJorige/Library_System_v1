using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_System_v1.Models
{
    public class LoginModel
    {
        public string Email{ get; set; }
        public string Password { get; set; }
        public string PasswordHashKey { get; set; }
        public bool RememberMe { get; set; }
    }
}