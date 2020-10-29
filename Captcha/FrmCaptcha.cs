using CaptchaLib.Builder;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace Captcha
{
    public partial class FrmCaptcha : Form
    {
        private string code = String.Empty;

        public FrmCaptcha()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Equals(code))
                MessageBox.Show("Correct", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Not Correct", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GenerateImage();
            //GenerateImages(); 
        }

        private void GenerateImages() {

            CaptchaLib.Captcha captcha;
            Random r;
            int lenLines = 0;
             CaptchaBuilder captchaBuilder;
        
            for (int i = 0; i < 10; i++) {
                r = new Random();
                captchaBuilder = new CaptchaBuilder();
                captchaBuilder.WithBackgroundColorRGB(r.Next(0,255), r.Next(0, 255), r.Next(0, 255));
                captchaBuilder.WithFontColorRGB(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                 lenLines = r.Next(5, 40);
                
                captchaBuilder.WithTextLength(lenLines);
                captchaBuilder.WithFontNameAndSize("Arial", 16);
                captchaBuilder.WithNumberOfStrips(5);
                captchaBuilder.WithStripsColorRGB(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                captcha = captchaBuilder.Build();
                captcha.GenerateCaptcha();
             
                Thread.Sleep(1000);
            }
            MessageBox.Show("Generating Process has been finished.");
        }

        private void GenerateImage()
        {
            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();

            CaptchaLib.Captcha captcha;
            CaptchaBuilder captchaBuilder = new CaptchaBuilder();
            captchaBuilder.WithBackgroundColor(Color.Red);
            captchaBuilder.WithFontColor(Color.Aqua);
            captchaBuilder.WithTextLength(5);
            captchaBuilder.WithFontNameAndSize("Arial", 16);
            captchaBuilder.WithNumberOfStrips(15);
            captchaBuilder.WithStripsColor(Color.AliceBlue);
            captcha = captchaBuilder.Build();



            captcha.GenerateCaptcha();
            txtInput.Text = captcha.GeneratedCode;
            pictureBox1.Image = Image.FromFile(captcha.ImageFilePath);

        }
    }

}