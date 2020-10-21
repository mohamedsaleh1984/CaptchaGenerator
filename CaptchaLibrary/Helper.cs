using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

        /// <summary>
        /// Generate random points to place the line on the Bitmap
        /// </summary>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <returns></returns>
        public static Point[] GetRandomPoints(int width,int height)
        {
            Random _Rand = new Random();
            Point[] points = { new Point(0, 0),
                               new Point(_Rand.Next(0, width),_Rand.Next(0, height)) };

            return points;
        }
        public static Point[] GetRandomPoints(int width, int height,Random r)
        {
            Point[] points = { new Point(0, 0),
                               new Point(r.Next(0, width),r.Next(0, height)) };

            return points;
        }
        public static List<Point[]> GenerateLinesStartEndPoints(int numOfLines, int width, int height)
        {
            Random _Rand = new Random();
            List<Point[]> list = new List<Point[]>();

            for (int i = 0; i < numOfLines; i++)
            {
                Point[] points = { new Point(0, 0),
                               new Point(_Rand.Next(0, width),_Rand.Next(0, height)) };
                list.Add(points);
            }

            return list;
           
        }

        /// <summary>
        /// Generate Image File Name with BMP Extension.
        /// </summary>
        /// <returns>new file name</returns>
        public  static string GenerateFileName()
        {
            string strFileName = DateTime.Now.Year.ToString() +
                                DateTime.Now.Month.ToString() +
                                DateTime.Now.Day.ToString() +
                                DateTime.Now.Hour.ToString() +
                                DateTime.Now.Minute.ToString() +
                                DateTime.Now.Second.ToString();

            strFileName += ".bmp";
            return strFileName;
        }

        /// <summary>
        /// Calculate generated text code width and height based on font size
        /// </summary>
        /// <param name="text">Text Input</param>
        /// <param name="font">Font</param>
        /// <returns></returns>
        public static Rectangle CalcTextRectangle(string text, Font font)
        {
            Bitmap bitmap = new Bitmap(1024, 1024, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            SizeF textSizeF = g.MeasureString(text, font);

            double width = Math.Ceiling(textSizeF.Width);
            double height = Math.Ceiling(textSizeF.Height);

            return new Rectangle(0, 0, (int)width, (int)height);
        }
    }
}
