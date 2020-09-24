using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CaptchaLibrary
{

    public  class CaptchaLibraryClass
    {
        private  Random _Rand;
        private  string _strCode;
        private  int _iCodeLength;
        private  Image _BitMapImage;
        private int _imageWidth;
        private int _imageHeight;
        private Font _imageFont;
        

        public  String _DirectoryPath { get; set; }
        private  string _GeneratedFileName { get; set; }
        public  string GetGeneratedFilePath()
        {
            return _DirectoryPath + "\\" + _GeneratedFileName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CodeLength">Length of Generated Code</param>
        /// <param name="DirectoryPath">Location where BMP files Get generated.</param>
        public CaptchaLibraryClass(int CodeLength = 5,
                                string DirectoryPath = "C:\\Images",
                                int ImageWidth = 200,
                                int ImageHeight = 50)
        {
            _Rand = new Random();
            _iCodeLength = CodeLength;
            _BitMapImage = null;
            _DirectoryPath = DirectoryPath;
            _GeneratedFileName = "";
            _strCode = "";
            _imageWidth = ImageWidth;
            _imageHeight = ImageHeight;
            _imageFont = new Font("Tahoma", 10 + _Rand.Next(14, 18));

        }
        public Font ImageFont
        {
            get { return _imageFont; }
            set { _imageFont = value; }
        }


        /// <summary>
        /// Reset all values for the class.
        /// </summary>
        public  void Reset()
        {
            _Rand = new Random();
            _iCodeLength = 5;
            _DirectoryPath = "";
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
        public string GenerateRandomText()
        {
            string randomText = "";

            if (String.IsNullOrEmpty(_strCode))
            {
                string alphabets = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                Random r = new Random();

                for (int j = 0; j <= _iCodeLength; j++)
                {
                    randomText = randomText + alphabets[r.Next(alphabets.Length)];

                }
                _strCode = randomText.ToString();
            }
            return _strCode;
        }

        /// <summary>
        /// Check if the given string matched with generated code.
        /// </summary>
        /// <param name="strInputFromUser"></param>
        /// <returns></returns>
        public bool IsValidCode(object strInputFromUser)
        {
            if (String.IsNullOrEmpty(_strCode))
            {
                throw new Exception("");
            }

            return _strCode.Equals(Convert.ToString(strInputFromUser)) ? true : false;
        }

        /// <summary>
        /// Generate an Image.
        /// </summary>
        /// <param name="strPath"></param>
        public void CreateImage()
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
                           _imageFont,
                            whtBrush, new PointF(10 + ipxlOffset, 10));

                ipxlOffset += 20;
            }

            DrawRandomLines(g);

            string strFileName = DateTime.Now.Year.ToString() +
                                DateTime.Now.Month.ToString() +
                                DateTime.Now.Day.ToString() +
                                DateTime.Now.Hour.ToString() +
                                DateTime.Now.Minute.ToString() +
                                DateTime.Now.Second.ToString();

            strFileName += ".bmp";
            _GeneratedFileName = strFileName;

            if (!Directory.Exists(_DirectoryPath))
            {
                Directory.CreateDirectory(_DirectoryPath);
            }


            if (File.Exists(_DirectoryPath + "\\" + _GeneratedFileName))
            {
                try
                {
                    File.Delete(_DirectoryPath + "\\" + _GeneratedFileName);
                    bitmap.Save(_DirectoryPath + "\\" + _GeneratedFileName);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error creating file." + "\n" + ex.Message);
                }
            }
            else
            {
                bitmap.Save(_DirectoryPath + "\\" + _GeneratedFileName);
            }

            g.Dispose();

            bitmap.Dispose();

            _BitMapImage = Image.FromFile(_DirectoryPath + "\\" + _GeneratedFileName);
        }

        /// <summary>
        /// Draw lines.
        /// </summary>
        /// <param name="g"></param>
        private void DrawRandomLines(Graphics g)
        {
            SolidBrush gray = new SolidBrush(Color.Gray);
            for (int i = 0; i < 10; i++)
            {
                g.DrawLines(new Pen(gray, 2), GetRandomPoints());
            }
        }

        private void Check()
        {
            if (String.IsNullOrEmpty(this._DirectoryPath))
            {
                throw new Exception("You should set Directory first.");
            }
            if (String.IsNullOrEmpty(this._GeneratedFileName))
            {
                throw new Exception("No generated file");
            }

            if (String.IsNullOrEmpty(this._strCode))
            {
                throw new Exception("No generated code");
            }

            if (_BitMapImage == null)
            {
                throw new Exception("No Image generated.");
            }

        }
    }
}
