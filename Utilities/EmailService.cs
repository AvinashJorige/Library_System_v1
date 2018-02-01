using System.Linq;
using System.Net.Mail;
using System.IO;
using System.Threading.Tasks;

namespace Utilities
{
    public class EmailService
    {
        public async Task<string> ServiceEmail(string template, string[] _fieldKeys, string subject, string toAddress)
        {
            string Body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/templates/" + template + ".html")))
            {
                Body = reader.ReadToEnd();
            }

            Body += _fieldKeys.Select(x =>
            {
                Body += Body.Replace("{" + x + "}", x);
                return Body;
            });

            return await SendHtmlFormattedEmail(subject, Body, toAddress);
        }

        private Task<string> SendHtmlFormattedEmail(string subject, string body, string toAddress)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("jo.avi.1990@gmail.com");
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(toAddress));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "jo.avi.1990@gmail.com"; //reading from web.config  
                NetworkCred.Password = "Avinash@123"; //reading from web.config  
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587; //reading from web.config  
                smtp.Send(mailMessage);
            }
            return default(Task<string>);
        }
    }
}
