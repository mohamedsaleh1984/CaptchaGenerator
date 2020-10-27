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
        /// Create dynamic bitmap based on generated text.
        /// </summary>
        /// <returns></returns>
        private Bitmap CreateImageBitmap()
        {
            //Generate Random Code
            string code = Helper.GenerateRandomText(CodeLength);

            //Set it to currrent property to use it for verify
            GeneratedCode = code;

            //Destroy the bitmap.
            if (_bitmap != null)
                _bitmap.Dispose();

            //Create a rectangle to hold the text.
            Rectangle rectangle = Helper.CalcTextRectangle(GeneratedCode, ImageFont);
            _imageWidth = rectangle.Width;
            _imageHeight = rectangle.Height+20;

            //Caculate roughly the width of a Character. 
            int charWidth = _imageWidth / code.Length;

            //Create new Bitmap
            _bitmap = new Bitmap(_imageWidth, _imageHeight, PixelFormat.Format32bppArgb);

            //Created graphics GDI handler to paint 
            Graphics g = Graphics.FromImage(_bitmap);

            //Create rectangle 
            Rectangle rect = new Rectangle(0, 0, _imageWidth, _imageHeight);

            int ipxlOffset = 0;

            //Draw dummy rectangle with the Bitmap.
            g.DrawRectangle(new Pen(Color.Yellow), rect);

            //Fill the rectangle with the choosen color.
            g.FillRectangle(BackgroundColor, rect);

            //Draw Code text...
            for (int i = 0; i < code.Length; i++)
            {
               
                g.DrawString(code[i].ToString(),
                       ImageFont,
                       FontColor,
                       new PointF(ipxlOffset, charWidth));
                
                ipxlOffset += (charWidth);
            }

            //Adding Strips to the Bitmap
            DrawRandomLines(g);

            return _bitmap;
        }

        /// <summary>
        /// Draw lines to bitmap.
        /// </summary>
        /// <param name="g"></param>
        private void DrawRandomLines(Graphics g)
        {
           
            for (int i = 0; i < NumOfLines; i++)
            {
                g.DrawLines(new Pen(LinesColor, 2), Helper.GetRandomPoints(_imageWidth,_imageHeight));
            }
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
            _bitmap = CreateImageBitmap();
            String strFileName = Helper.GenerateFileName();
            return SaveBitmapToFile(strFileName);
        }
    }
}
