using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using CaptchaLibrary;

namespace Captcha
{
    public partial class FrmCaptcha : Form
    {
        private CaptchaLibrary.CaptchaLibraryClass cls;

        private Random rand = new Random();
        private string code;

        public FrmCaptcha()
        {
            InitializeComponent();
            cls = new CaptchaLibraryClass();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateImage();
        }

        private void CreateImage()
        {
            string code = cls.GenerateRandomText();

            Bitmap bitmap =  new Bitmap(200, 50, PixelFormat.Format32bppArgb);
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

}