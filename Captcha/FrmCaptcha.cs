using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Captcha
{
    public partial class FrmCaptcha : Form
    {
        private Random rand = new Random();
        private string code;

        public FrmCaptcha()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateImage();
        }

        private void CreateImage()
        {
            string code = GenerateRandomText();

            Bitmap bitmap = new Bitmap(200, 50, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
           
            Rectangle rect = new Rectangle(0, 0, 200, 50);

            SolidBrush blkBrush = new SolidBrush(Color.Black);
            SolidBrush whtBrush = new SolidBrush(Color.White);

            int ipxlOffset = 0;

            g.DrawRectangle(new Pen(Color.Yellow), rect);

            g.FillRectangle(blkBrush, rect);

            for (int i = 0; i < code.Length; i++)
            {
                g.DrawString(code[i].ToString(),
                            new Font("Tahoma", 10 + rand.Next(14, 18)),
                            whtBrush, new PointF(10 + ipxlOffset, 10));

                ipxlOffset += 20;
            }

            DrawRandomLines(g);

            if (File.Exists("tempimage.bmp"))
            {
                try
                {
                    File.Delete("tempimage.bmp");
                    bitmap.Save("tempimage.bmp");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                bitmap.Save("tempimage.bmp");
            }

            g.Dispose();
            
            bitmap.Dispose();
            
            pictureBox1.Image = Image.FromFile("tempimage.bmp");
        }

        private void DrawRandomLines(Graphics g)
        {
            SolidBrush green = new SolidBrush(Color.Gray);
            for (int i = 0; i < 10 ; i++)
            {
                g.DrawLines(new Pen(green, 2), GetRandomPoints());
            }
        }

        private Point[] GetRandomPoints()
        {
            Point[] points = { new Point(rand.Next(10, 150), rand.Next(10, 150)), 
                               new Point(rand.Next(10, 100), rand.Next(10, 100)) };
            return points;
        }
       
        private string GenerateRandomText()
        {
            string randomText = "";

            if (String.IsNullOrEmpty(code))
            {
                string alphabets = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                
                Random r = new Random();

                for (int j = 0; j <= 5 ; j++)
                {
                    randomText = randomText + alphabets[r.Next(alphabets.Length)];
                   
                }
                code = randomText.ToString();
            }
            return code;
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Equals(code))
                MessageBox.Show("Correct","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
                MessageBox.Show("Not Correct", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            code = "";
            CreateImage();
        }

        
    }

    public class CaptchaGenerator
    {
        private Random _Rand;
        private  string _strCode;
        private int _iCodeLength;
        private  Image _BitMapImage;
        public String _DirectoryPath { get; set; }
        private string _GeneratedFileName { get; set; }
        public string GetGeneratedFilePath()
        {
            return _DirectoryPath + "\\" + _GeneratedFileName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CodeLength">Length of Generated Code</param>
        /// <param name="DirectoryPath">Location where BMP files Get generated.</param>
        public CaptchaGenerator(int CodeLength=5,
                                string DirectoryPath="C:\\Images")
        {
            _Rand = new Random();
            _iCodeLength = CodeLength;
            _BitMapImage = null;
            _DirectoryPath = DirectoryPath;
            _GeneratedFileName = "";
            _strCode = "";

        }

        /// <summary>
        /// Reset all values for the class.
        /// </summary>
        public void Reset()
        {
            _Rand = new Random();
            _iCodeLength = 5;
            _DirectoryPath = "C:\\Images";
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
            if(String.IsNullOrEmpty(_strCode))
            {
                throw new Exception("");
            }

            return _strCode.Equals(Convert.ToString(strInputFromUser)) ? true : false;
        }

        /// <summary>
        /// Generate an Image.
        /// </summary>
        /// <param name="strPath"></param>
        private void CreateImage()
        {
            string code = GenerateRandomText();

            Bitmap bitmap = new Bitmap(200, 50, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);

            Rectangle rect = new Rectangle(0, 0, 200, 50);

            SolidBrush blkBrush = new SolidBrush(Color.Black);
            SolidBrush whtBrush = new SolidBrush(Color.White);

            int ipxlOffset = 0;

            g.DrawRectangle(new Pen(Color.Yellow), rect);

            g.FillRectangle(blkBrush, rect);

            for (int i = 0; i < code.Length; i++)
            {
                g.DrawString(code[i].ToString(),
                            new Font("Tahoma", 10 + _Rand.Next(14, 18)),
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

            if(!Directory.Exists(_DirectoryPath))
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
            SolidBrush green = new SolidBrush(Color.Gray);
            for (int i = 0; i < 10; i++)
            {
                g.DrawLines(new Pen(green, 2), GetRandomPoints());
            }
        }

        private void Check()
        {
            if(String.IsNullOrEmpty(this._DirectoryPath))
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

            if(_BitMapImage == null)
            {
                throw new Exception("No Image generated.");
            }

        }

    }
}