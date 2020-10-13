using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace CaptchaLibrary
{
    public class CaptchaLibraryClass
    {
        private Random _Rand;
        private Bitmap _bitmap;
        private int _imageWidth;
        private int _imageHeight;

        public SolidBrush LinesColor
        { get; set; }
        public String ImageFilePath { get; private set; }

        public SolidBrush BackgroundColor { get; set; }
        public SolidBrush FontColor { get; set; }
        public String DirectoryPath { get; set; }

        public int CodeLength { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CodeLength">Length of Generated Code</param>
        /// <param name="DirectoryPath">Location where BMP files Get generated.</param>
        public CaptchaLibraryClass(int _CodeLength = 5,
                                string DirectoryPath = "C:\\Images",
                                int ImageWidth = 200,
                                int ImageHeight = 50)
        {
            _Rand = new Random();
            CodeLength = _CodeLength;
            _bitmap = null;
            this.DirectoryPath = DirectoryPath;
            ImageFilePath = "";
            GeneratedCode = "";
            _imageWidth = ImageWidth;
            _imageHeight = ImageHeight;
            ImageFont = new Font("Tahoma", 16);
            BackgroundColor = new SolidBrush(Color.Black);
            FontColor = new SolidBrush(Color.White);
            LinesColor = new SolidBrush(Color.Gray);
            NumOfLines = 10;
        }

        public int NumOfLines { get; set; }


        /// <summary>
        /// Get/Set Image Font
        /// </summary>
        public Font ImageFont { get; set; }

        /// <summary>
        /// Get Generate Text
        /// </summary>
        public String GeneratedCode { get; private set; }


        /// <summary>
        /// Reset all values for the class.
        /// </summary>
        public void Reset()
        {
            _Rand = new Random();
            CodeLength = 5;
            DirectoryPath = "";
        }

        /// <summary>
        /// Generate random points to place tge characters in the Bitmap.
        /// </summary>
        /// <returns></returns>
        private Point[] GetRandomPoints()
        {
            Point[] points = { new Point(_Rand.Next(10, 150), _Rand.Next(10, 150)),
                               new Point(_Rand.Next(10, 100), _Rand.Next(10, 100)) };
            return points;
        }

        /// <summary>
        /// Generate random text.
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomText()
        {
            string randomText = "";
            string alphabets = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random r = new Random();

            for (int j = 0; j < CodeLength; j++)
                randomText = randomText + alphabets[r.Next(alphabets.Length)];

            GeneratedCode = randomText.ToString();
            return GeneratedCode;
        }

        /// <summary>
        /// Check if the given string matched with generated code.
        /// </summary>
        /// <param name="strInputFromUser"></param>
        /// <returns></returns>
        public bool IsValidCode(object strInputFromUser)
        {
            if (String.IsNullOrEmpty(GeneratedCode))
            {
                throw new Exception("");
            }

            return GeneratedCode.Equals(Convert.ToString(strInputFromUser)) ? true : false;
        }

        /// <summary>
        /// Generate an Image.
        /// </summary>
        /// <param name="strPath"></param>
        private void CreateImage()
        {
            string code = GenerateRandomText();

            Bitmap bitmap = new Bitmap(_imageWidth, _imageHeight, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);

            Rectangle rect = new Rectangle(0, 0, _imageWidth, _imageHeight);

            SolidBrush blkBrush = new SolidBrush(Color.Black);
            SolidBrush whtBrush = new SolidBrush(Color.White);

            int ipxlOffset = 0;

            g.DrawRectangle(new Pen(Color.Yellow), rect);

            g.FillRectangle(blkBrush, rect);

            for (int i = 0; i < code.Length; i++)
            {
                g.DrawString(code[i].ToString(),
                           ImageFont,
                            whtBrush, new PointF(10 + ipxlOffset, 10));

                ipxlOffset += 20;
            }

            DrawRandomLines(g);
        }

        /// <summary>
        /// Generate an Image and return Bitmap
        /// </summary>
        /// <returns></returns>
        private Bitmap CreateImageBitmap()
        {
            string code = GenerateRandomText();
            this.GeneratedCode = code;
            if (_bitmap != null)
                _bitmap.Dispose();


            _bitmap = new Bitmap(_imageWidth, _imageHeight, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(_bitmap);

            Rectangle rect = new Rectangle(0, 0, _imageWidth, _imageHeight);


            int ipxlOffset = 0;

            g.DrawRectangle(new Pen(Color.Yellow), rect);

            g.FillRectangle(BackgroundColor, rect);

            for (int i = 0; i < code.Length; i++)
            {
                g.DrawString(code[i].ToString(),
                           ImageFont,
                            FontColor,
                            new PointF(_imageWidth / 20 + ipxlOffset, _imageWidth / 20));

                ipxlOffset += _imageWidth / 10;
            }

            DrawRandomLines(g);

            return _bitmap;
        }


        private Bitmap CreateImageBitmap_v2()
        {
            string code = GenerateRandomText();
            GeneratedCode = code;
            if (_bitmap != null)
                _bitmap.Dispose();

            Rectangle rectangle = CalcTextRectangle(GeneratedCode, ImageFont);
            _imageWidth = rectangle.Width;
            _imageHeight = rectangle.Height+20;
            int charWidth = _imageWidth / code.Length;
            //_imageWidth += code.Length * charWidth;

            _bitmap = new Bitmap(_imageWidth, _imageHeight, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(_bitmap);

            Rectangle rect = new Rectangle(0, 0, _imageWidth, _imageHeight);

            int ipxlOffset = 0;

            g.DrawRectangle(new Pen(Color.Yellow), rect);

            g.FillRectangle(BackgroundColor, rect);

            for (int i = 0; i < code.Length; i++)
            {
               
                g.DrawString(code[i].ToString(),
                       ImageFont,
                       FontColor,
                       new PointF(ipxlOffset, charWidth));
                
                ipxlOffset += (charWidth);
            }

         //   DrawRandomLines(g);

            return _bitmap;
        }
        /// <summary>
        /// Draw lines.
        /// </summary>
        /// <param name="g"></param>
        private void DrawRandomLines(Graphics g)
        {
            for (int i = 0; i < NumOfLines; i++)
            {
                g.DrawLines(new Pen(LinesColor, 2), GetRandomPoints());
            }
        }

        /// <summary>
        /// Generate Image File Name with Extension.
        /// </summary>
        /// <returns></returns>
        private String GenerateFileName()
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
        /// Save Bitmap to File
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public Boolean SaveBitmapToFile(String strFileName)
        {
            
            if(String.IsNullOrEmpty(DirectoryPath))
            {
                DirectoryPath = System.IO.Directory.GetCurrentDirectory();
            }
            else if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }


            String strFullFilePath = DirectoryPath + "\\" + strFileName;

            if (File.Exists(strFullFilePath))
            {
                try
                {
                    File.Delete(strFullFilePath);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error creating file." + "\n" + ex.Message);
                }
            }

            bool bSuccessful = false;
            try
            {
        
                _bitmap.Save(strFullFilePath,ImageFormat.Bmp);
                bSuccessful = true;
                ImageFilePath = strFullFilePath;
                _bitmap.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            return bSuccessful;
        }

        /// <summary>
        /// Generate Captcha
        /// </summary>
        /// <returns></returns>
        public bool GenerateCaptcha()
        {
            // Bitmap bitmap = CreateImageBitmap();
            Bitmap bitmap = CreateImageBitmap_v2();
            String strFileName = GenerateFileName();
            return SaveBitmapToFile(strFileName);
        }

        /// <summary>
        /// calculate the proper width and height of a text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize"></param>
        /// <param name="style"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public Rectangle CalcTextRectangle(string text, string fontFamily, float fontSize, FontStyle style)
        {
            Font font = new Font(fontFamily, fontSize, style);
            Bitmap bitmap = new Bitmap(1024, 1024, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            SizeF textSizeF = g.MeasureString(text, font);
            double width = Math.Ceiling(textSizeF.Width);
            double height = Math.Ceiling(textSizeF.Height);


            return new Rectangle(0, 0, (int)width, (int)height);
        }
        public Rectangle CalcTextRectangle(string text, Font font)
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
