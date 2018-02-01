using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilities
{
    public class SMSsend
    {
        public string sendSMS(string numbers, string strMessage)
        {
            String message = HttpUtility.UrlEncode(strMessage);
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , "wyA2r/MwIHU-pDL8m0pZAlxViWbWe2qrC1BcyaX8s3	"},
                {"numbers" , numbers},
                {"message" , message},
                {"sender" , "TXTLCL"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }
    }
}
