using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_System_v1.Models
{
    public class ForgetPasswordModel
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}