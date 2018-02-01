using Domain.Entities;
using RepositoryDB;
using RepositoryDB.Repositories;
using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace TestProgram
{
    class Program
    {
        //Usefull Link : https://github.com/aliakseimaniuk/MongoRepository
        //Usefulll Link for sms = https://control.textlocal.in/settings/apikeys/
        static void Main(string[] args)
        {
            sendSMS("918555820830", "text Avinash");

            sendSMS();
            Task t = MainAsync();
            t.Wait();

            Console.ReadKey();
        }

        public static string sendSMS()
        {
            String message = HttpUtility.UrlEncode("This is your message");
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , "wyA2r/MwIHU-pDL8m0pZAlxViWbWe2qrC1BcyaX8s3	"},
                {"numbers" , "918555820830"},
                {"message" , message},
                {"sender" , "TXTLCL"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }

        public static void sendSMS(string Recepient, string Message)
        {
            //Recepient is the cellno in string format
            StringBuilder sb = new StringBuilder();
            sb.Append("http://www.mymobileapi.com/api5/http5.aspx?");
            sb.Append("Type=sendparam");
            sb.Append("&username=Aviansh_Jorige");//add your username here
            sb.Append("&password=Avinash@123");//add your password here
            sb.AppendFormat("&numto={0}", Recepient);
            string message = HttpUtility.UrlEncode(Message, ASCIIEncoding.ASCII);
            sb.AppendFormat("&data1={0}", message);

            try
            {
                ////Create the request and send data to the SMS Gateway Server by HTTP connection
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(sb.ToString());
                //Get response from the SMS Gateway Server and read the answer
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch (Exception ex)
            {

            }
        }

        static async Task MainAsync()
        {
            var p = new AdminMaster()
            {
                adCode = "Level 1",
                adEmail = "jo.avi.1990@gmail.com",
                adName = "Avinash",
                adPassword = "123456",
                CreatedDate = DateTime.UtcNow.ToString(),
                ModifiedDate = DateTime.UtcNow.ToString(),
                IsActive = true,
                Id = ObjectId.GenerateNewId()
            };

            var context = new MongoDataContext();
            var personRepository = new GenericRepository<AdminMaster>(context);

            await personRepository.SaveAsync(p);

            var personFromDatabase = await personRepository.GetByIdAsync(p.Id);            
        }
    }
}
