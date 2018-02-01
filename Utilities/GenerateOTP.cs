using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
   public class GenerateOTP
    {
        public string OTPGenerate(bool alpha, int otpLength)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "1234567890";

            string characters = numbers;
            if (alpha)
            {
                characters += alphabets + numbers;
            }
            string otp = string.Empty;
            for (int i = 0; i < otpLength; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }

            return otp;
        }
    }
}
