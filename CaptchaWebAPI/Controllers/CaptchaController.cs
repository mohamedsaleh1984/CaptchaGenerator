using CaptchaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
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
            cls = new CaptchaLibrary.CaptchaLibraryClass(5, "",200,50);
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
    }
}
