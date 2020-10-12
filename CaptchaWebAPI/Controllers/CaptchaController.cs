using CaptchaWebAPI.Models;
using System.Drawing;
using System.Web.Http;


namespace CaptchaWebAPI.Controllers
{
    /// <summary>
    /// Captcha WebAPI Controller
    /// </summary>
    public class CaptchaController : ApiController
    {
        private CaptchaLibrary.CaptchaLibraryClass cls = null;

        /// <summary>
        /// Controller Constructor
        /// </summary>
        public CaptchaController()
        {
            cls = new CaptchaLibrary.CaptchaLibraryClass(5, "", 200, 50);
            cls.NumOfLines = 5;
            cls.BackgroundColor = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        }


        /// <summary>
        /// Get request to generate new Captcha Image, return Image Path and Code
        /// GET: api/Captcha
        /// </summary>
        /// <returns></returns>
        public Captcha Get()
        {

            Captcha captcha = new Captcha();
            if (cls.GenerateCaptcha())
            {
                captcha.CaptchaImagePath = cls.ImageFilePath;
                captcha.CaptchaCode = cls.GeneratedCode;

                return captcha;
            }
            return null;
        }


        /// <summary>
        /// Generate Captcha code with Specific background color and length.
        /// </summary>
        /// <param name="codeLength">Length of the Code</param>
        /// <param name="backgroundColor">Background Color i.e Black,Red..etc</param>
        /// <returns></returns>
        [Route("api/Captcha/Generate/{codeLength:int}/{backgroundColor}")]
        [HttpGet]
        public Captcha Generate(int codeLength = 5, string backgroundColor = "Black")
        {
            cls = new CaptchaLibrary.CaptchaLibraryClass(codeLength, "", 200, 50);
            Color selectedColor = Color.FromName(backgroundColor);

            cls.BackgroundColor = new System.Drawing.SolidBrush(selectedColor);
            if (cls.BackgroundColor == null)
            {
                cls.BackgroundColor = new System.Drawing.SolidBrush(Color.Black);
            }
            return Get();
        }
    }
}
