using CaptchaLib;
using CaptchaLib.Builder;
using CaptchaWebAPI.Models;
using System.Drawing;
using System.Net.Http;
using System.Web.Http;

namespace CaptchaWebAPI.Controllers
{
    /// <summary>
    /// Captcha WebAPI Controller
    /// </summary>
    public class CaptchaController : ApiController
    {
        private readonly CaptchaBuilder captchaBuilder = new CaptchaBuilder();

        /// <summary>
        /// Controller Constructor
        /// </summary>
        public CaptchaController()
        {
            captchaBuilder.WithTextLength(5);
            captchaBuilder.WithBackgroundColor(Color.Black);
        }

        /// <summary>
        /// Get request to generate new Captcha Image, return Image Path and Code
        /// GET: api/Captcha
        /// </summary>
        /// <returns></returns>
        public CaptchaModel Get()
        {
            CaptchaModel captchaModel = new CaptchaModel();
            Captcha captcha;
            captcha = captchaBuilder.Build();

            try
            {
                if (captcha.GenerateCaptcha())
                {
                    captchaModel.CaptchaImagePath = captcha.ImageFilePath;
                    captchaModel.CaptchaCode = captcha.GeneratedCode;

                    return captchaModel;
                }
            }
            catch (System.Exception)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.ExpectationFailed);
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
        public CaptchaModel Generate(int codeLength = 5, string backgroundColor = "Black")
        {
            CaptchaModel captchaModel = new CaptchaModel();
            Captcha captcha;

            captchaBuilder.WithTextLength(codeLength);
            captchaBuilder.WithBackgroundColor(backgroundColor);

            captcha = captchaBuilder.Build();
            if (captcha.GenerateCaptcha())
            {
                captchaModel.CaptchaImagePath = captcha.ImageFilePath;
                captchaModel.CaptchaCode = captcha.GeneratedCode;

                return captchaModel;
            }
            return null;
        }

        /// <summary>
        /// Generate Captcha code with Specific background color, length and Number of Strips.
        /// </summary>
        /// <param name="codeLength">Length of the Code</param>
        /// <param name="backgroundColor">Background Color i.e Black,Red..etc</param>
        /// <param name="numOfLines">Number of lines across the Captcha</param>
        /// <returns></returns>
        [Route("api/Captcha/Generate/{codeLength:int}/{backgroundColor}/{numOfLines:int}")]
        [HttpGet]
        public CaptchaModel GenerateWithLines(int codeLength = 5, string backgroundColor = "Black", int numOfLines = 5)
        {
            CaptchaModel captchaModel = new CaptchaModel();
            Captcha captcha;

            captchaBuilder.WithTextLength(codeLength);
            captchaBuilder.WithBackgroundColor(backgroundColor);
            captchaBuilder.WithNumberOfStrips(numOfLines);
            try
            {
                captcha = captchaBuilder.Build();
                if (captcha.GenerateCaptcha())
                {
                    captchaModel.CaptchaImagePath = captcha.ImageFilePath;
                    captchaModel.CaptchaCode = captcha.GeneratedCode;

                    return captchaModel;
                }
            }
            catch (System.Exception)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.ExpectationFailed);
            }

            return null;
        }

        /// <summary>
        /// Generate Captcha code with Specific background color, length, number of strips and color of strips.
        /// </summary>
        /// <param name="codeLength">Length of the Code</param>
        /// <param name="backgroundColor">Background Color i.e Black,Red..etc</param>
        /// <param name="numOfLines">Number of lines across the Captcha</param>
        /// <param name="linesColor">Strips Color</param>
        /// <returns></returns>
        [Route("api/Captcha/Generate/{codeLength:int}/{backgroundColor}/{numOfLines:int}/{linesColor}")]
        [HttpGet]
        public CaptchaModel GenerateWithColoredLines(int codeLength = 5,
                                        string backgroundColor = "Black",
                                        int numOfLines = 5,
                                        string linesColor = "Gray")
        {
            CaptchaModel captchaModel = new CaptchaModel();
            Captcha captcha;

            captchaBuilder.WithTextLength(codeLength);
            captchaBuilder.WithBackgroundColor(backgroundColor);
            captchaBuilder.WithNumberOfStrips(numOfLines);
            captchaBuilder.WithStripsColor(linesColor);

            captcha = captchaBuilder.Build();
            try
            {
                if (captcha.GenerateCaptcha())
                {
                    captchaModel.CaptchaImagePath = captcha.ImageFilePath;
                    captchaModel.CaptchaCode = captcha.GeneratedCode;

                    return captchaModel;
                }
            }
            catch (System.Exception)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.ExpectationFailed);
            }
            return null;
        }
        /// <summary>
        /// Generate Captcha with Five Gray Strips. 
        /// </summary>
        /// <param name="codeLength">Length of Captcha</param>
        /// <param name="backgroundColor">Background Color</param>
        /// <returns></returns>
        [Route("api/Captcha/GenGray5Strips/{codeLength:int}/{backgroundColor}")]
        [HttpGet]
        public CaptchaModel GenerateWith5GrayStrips(int codeLength = 5,
                                        string backgroundColor = "Black")
        {
            CaptchaModel captchaModel = new CaptchaModel();
            Captcha captcha;

            captchaBuilder.WithTextLength(codeLength);
            captchaBuilder.WithBackgroundColor(backgroundColor);
            captchaBuilder.WithNumberOfStrips(5);
            captchaBuilder.WithStripsColor(Color.Gray);

            captcha = captchaBuilder.Build();
            try
            {
                if (captcha.GenerateCaptcha())
                {
                    captchaModel.CaptchaImagePath = captcha.ImageFilePath;
                    captchaModel.CaptchaCode = captcha.GeneratedCode;

                    return captchaModel;
                }
            }
            catch (System.Exception)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.ExpectationFailed);
            }
            return null;
        }

    }
}
