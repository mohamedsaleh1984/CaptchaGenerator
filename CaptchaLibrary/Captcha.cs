using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptchaLibrary
{
    /// <summary>
    /// Captcha using Builder Pattern.
    /// </summary>
    public class Captcha
    {
        private readonly Random _Rand;
        private Bitmap _bitmap;
        private int _imageWidth;
        private int _imageHeight;
        public SolidBrush LinesColor
        { get; set; }
        public string ImageFilePath { get; private set; }
        public SolidBrush BackgroundColor { get; set; }
        public SolidBrush FontColor { get; set; }
        public string DirectoryPath { get; set; }
        public int CodeLength { get; set; }
        public int NumOfLines { get; set; }

        /// <summary>
        /// Get/Set Image Font
        /// </summary>
        public Font ImageFont { get; set; }

        /// <summary>
        /// Get Generate Text
        /// </summary>
        public string GeneratedCode { get; private set; }
        /// <summary>
        /// Instance of Captcha class
        /// </summary>
        private Captcha _instance  = new Captcha();
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Captcha() {
            _Rand = new Random();
        }

        public Captcha Build() {
            return _instance;
        }

        /// <summary>
        /// Adding Generated Text Length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public void WithTextLength(int length) {
            _instance.CodeLength = length;
        }

        /// <summary>
        /// Adding Background Color of the Bitmap
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public void WithBackgroundColor(Color color)
        {
            SolidBrush solid = new SolidBrush(color);
            _instance.BackgroundColor = solid;
        }

        /// <summary>
        /// Adding Number of Strips on the Bitmap
        /// </summary>
        /// <param name="numOfStrips"></param>
        /// <returns></returns>
        public void WithNumberOfStrips(int numOfStrips)
        {
            _instance.NumOfLines = numOfStrips;
        }

        /// <summary>
        /// Adding Strips Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public void WithStripsColor(Color color) {
            _instance.LinesColor = new SolidBrush(color);
        }


        /// <summary>
        /// Add Font name, size to Captcha.
        /// </summary>
        /// <param name="fontName">Font Name</param>
        /// <param name="fontSize">Font Size</param>
        /// <returns></returns>
        public void WithFontNameAndSize(string fontName, float fontSize)
        {
            Font font = new Font(fontName, fontSize);
            if (font != null)
                _instance.ImageFont = font;
            else
                _instance.ImageFont = new Font("Tahoma", 16);//default font.
        }


        /// <summary>
        /// Reset all values for the class.
        /// </summary>
        public void Reset()
        {
            _instance = new Captcha();
        }

        /// <summary>
        /// Add width.
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public void WithWidth(int width)
        {
            _instance._imageWidth = width;
        }

        /// <summary>
        /// Add height.
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public void WithHeight(int height) {
            _instance._imageHeight = height;
        }

        /// <summary>
        /// Check if the given string matched with generated code, for Windows apo version
        /// </summary>
        /// <param name="InputFromUser">User's input</param>
        /// <returns></returns>
        public bool IsValidCode(string InputFromUser)
        {
            if (String.IsNullOrEmpty(GeneratedCode))
            {
                throw new Exception("");
            }

            return GeneratedCode.Equals(InputFromUser) ? true : false;
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
            _imageHeight = rectangle.Height + 20;

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
                g.DrawLines(new Pen(LinesColor, 2), Helper.GetRandomPoints(_imageWidth, _imageHeight));
            }
        }

        /// <summary>
        /// Save Bitmap to File
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public Boolean SaveBitmapToFile(String strFileName)
        {
            if (String.IsNullOrEmpty(DirectoryPath))
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

                _bitmap.Save(strFullFilePath, ImageFormat.Bmp);
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
