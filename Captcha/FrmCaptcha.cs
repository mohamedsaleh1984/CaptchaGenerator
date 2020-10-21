using System;
using System.Drawing;
using System.Windows.Forms;
using Captcha;
using CaptchaLibrary;

namespace Captcha
{
    public partial class FrmCaptcha : Form
    {
        private string code;

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
        }

        private void GenerateImage()
        {
            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();

            CaptchaLibrary.Captcha captcha;
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