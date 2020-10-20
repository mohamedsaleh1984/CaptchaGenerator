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
            cls = new CaptchaLibrary.CaptchaLibraryClass
            {
                NumOfLines = 5,
                BackgroundColor = new System.Drawing.SolidBrush(System.Drawing.Color.Black)
            };
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

        /// <summary>
        /// Generate Captcha code with Specific background color, length and Number of Strips.
        /// </summary>
        /// <param name="codeLength">Length of the Code</param>
        /// <param name="backgroundColor">Background Color i.e Black,Red..etc</param>
        /// <param name="numOfLines">Number of lines across the Captcha</param>
        /// <returns></returns>
        [Route("api/Captcha/Generate/{codeLength:int}/{backgroundColor}/{numOfLines}")]
        [HttpGet]
        public Captcha GenerateWithLines(int codeLength = 5, string backgroundColor = "Black", int numOfLines = 5)
        {
            cls = new CaptchaLibrary.CaptchaLibraryClass(codeLength);
            Color selectedColor = Color.FromName(backgroundColor);

            cls.BackgroundColor = new System.Drawing.SolidBrush(selectedColor);
            if (cls.BackgroundColor == null)
            {
                cls.BackgroundColor = new System.Drawing.SolidBrush(Color.Black);
            }
            cls.NumOfLines = numOfLines;
            return Get();
        }

        /// <summary>
        /// Generate Captcha code with Specific background color, length, number of strips and color of strips.
        /// </summary>
        /// <param name="codeLength">Length of the Code</param>
        /// <param name="backgroundColor">Background Color i.e Black,Red..etc</param>
        /// <param name="numOfLines">Number of lines across the Captcha</param>
        /// <param name="linesColor">Strips Color</param>
        /// <returns></returns>
        [Route("api/Captcha/Generate/{codeLength:int}/{backgroundColor}/{numOfLines}/{linesColor}")]
        [HttpGet]
        public Captcha GenerateWithColoredLines(int codeLength = 5,
                                        string backgroundColor = "Black",
                                        int numOfLines = 5,
                                        string linesColor = "Gray")
        {
            cls = new CaptchaLibrary.CaptchaLibraryClass(codeLength);
            Color selectedColor = Color.FromName(backgroundColor);

            cls.BackgroundColor = new System.Drawing.SolidBrush(selectedColor);
            if (cls.BackgroundColor == null)
            {
                cls.BackgroundColor = new System.Drawing.SolidBrush(Color.Black);
            }
            cls.NumOfLines = numOfLines;

            Color clinesColor = Color.FromName(linesColor);
            cls.LinesColor = new System.Drawing.SolidBrush(clinesColor);
            if (cls.LinesColor == null)
            {
                cls.LinesColor = new System.Drawing.SolidBrush(Color.Gray);
            }

            return Get();
        }
    }
}
