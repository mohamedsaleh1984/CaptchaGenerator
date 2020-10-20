using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaLibrary
{
    public class Helper
    {
        /// <summary>
        /// Generate random text.
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomText(int length)
        {
            string randomText = "";
            string alphabets = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random r = new Random();

            for (int j = 0; j < length; j++)
                randomText = randomText + alphabets[r.Next(alphabets.Length)];

            return randomText;
        }
    }
}
