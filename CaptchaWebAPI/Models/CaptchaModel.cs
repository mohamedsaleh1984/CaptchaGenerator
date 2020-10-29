using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaptchaWebAPI.Models
{
    /// <summary>
    /// Captcha Domain Class
    /// </summary>
    public class CaptchaModel
    {
        /// <summary>
        /// Captcha File Path
        /// </summary>
        public string CaptchaImagePath { get; set; } = "";

        /// <summary>
        /// Captcha Code
        /// </summary>
        public string CaptchaCode { get; set; } = "";
    }
}